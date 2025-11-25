using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using WindowsFormsApp3;
using UniChat;
using BCrypt.Net;

namespace Unichat
{
    public class Server
    {
        // Mantiene una lista de todos los "StreamWriters" de los clientes conectados.
        // Usamos StreamWriter para enviar texto fácilmente.
        private static readonly List<StreamWriter> clientWriters = new List<StreamWriter>();

        // Diccionario para asociar cada cliente con su username
        private static readonly Dictionary<StreamWriter, string> clientUsernames = new Dictionary<StreamWriter, string>();

        public static async Task Main(string[] args)
        {
            MessageBox.Show("Initializing UniChat Server...");
            TcpListener listener = new TcpListener(IPAddress.Any, 9000);
            listener.Start();

            // Obtener la IP local IPv4 para mandarla en un MessageBox.show w
            string localIP = "No encontrada";
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            MessageBox.Show($"Server ready in IP {localIP} Port 9000. Waiting for connections...");

            while (true)
            {
                // Espera a que un nuevo cliente se conecte
                TcpClient client = await listener.AcceptTcpClientAsync();

                // Inicia una nueva Tarea para manejar a este cliente.
                // Esto permite que el bucle 'while' vuelva a esperar por más clientes.
                _ = Task.Run(() => HandleClient(client));
            }
        }

        private static async Task HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

            string currentUsername = null;

            try
            {
                while (true)
                {
                    // Espera a recibir un mensaje del cliente (formato JSON)
                    string jsonMessage = await reader.ReadLineAsync();
                    if (jsonMessage == null) break; // El cliente se desconectó

                    Console.WriteLine($"Received: {jsonMessage}");

                    try
                    {
                        // Deserializar el comando del cliente
                        var request = JsonConvert.DeserializeObject<ClientRequest>(jsonMessage);

                        // Procesar el comando y generar respuesta
                        var response = await ProcessCommand(request, writer);

                        // Si es un LOGIN exitoso, guardar el username y añadir el writer
                        if (request.Command == "LOGIN" && response.Type == "LOGIN_SUCCESS")
                        {
                            currentUsername = request.Username;
                            lock (clientWriters)
                            {
                                clientWriters.Add(writer);
                                clientUsernames[writer] = currentUsername;
                            }
                        }

                        // Enviar respuesta al cliente
                        string jsonResponse = JsonConvert.SerializeObject(response);
                        await writer.WriteLineAsync(jsonResponse);

                        // Si es un mensaje de chat, hacer broadcast
                        if (request.Command == "SEND_MESSAGE" && response.Type == "MESSAGE_SUCCESS")
                        {
                            await BroadcastMessage(currentUsername, request.Content, request.ChatId ?? 0);
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error parsing JSON: {ex.Message}");
                        var errorResponse = new ServerResponse
                        {
                            Type = "ERROR",
                            Content = "Formato de mensaje inválido"
                        };
                        await writer.WriteLineAsync(JsonConvert.SerializeObject(errorResponse));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cliente desconectado: {ex.Message}");
            }
            finally
            {
                // Limpia la lista cuando el cliente se desconecta
                lock (clientWriters)
                {
                    clientWriters.Remove(writer);
                    if (clientUsernames.ContainsKey(writer))
                    {
                        clientUsernames.Remove(writer);
                    }
                }
                client.Close();
            }
        }

        private static async Task<ServerResponse> ProcessCommand(ClientRequest request, StreamWriter writer)
        {
            switch (request.Command)
            {
                case "LOGIN": 
                    return await HandleLogin(request);

                case "REGISTER":
                    return await HandleRegister(request);

                case "SEND_MESSAGE":
                    return await HandleSendMessage(request);

                case "GET_MESSAGES":
                    return await HandleGetMessages(request);

                case "CREATE_CHAT":
                    return await HandleCreateChat(request);

                case "GET_CHATS":
                    return await HandleGetChats(request);

                case "ADD_USER_TO_CHAT":
                    return await HandleAddUserToChat(request);

                case "REMOVE_USER_FROM_CHAT":
                    return await HandleRemoveUserFromChat(request);

                case "GET_CHAT_MEMBERS":
                    return await HandleGetChatMembers(request);

                case "DELETE_CHAT":
                    return await HandleDeleteChat(request);

                case "GET_ALL_USERS":
                    return await HandleGetAllUsers(request);

                default:
                    return new ServerResponse
                    {
                        Type = "ERROR",
                        Content = "Comando desconocido"
                    };
            }
        }

        private static async Task<ServerResponse> HandleLogin(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    string query = "SELECT id_user, passwd FROM users WHERE username = @username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", request.Username);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int idUser = Convert.ToInt32(reader["id_user"]);
                                string storedHashedPassword = reader["passwd"].ToString();

                                if (!storedHashedPassword.StartsWith("$"))
                                {
                                    return new ServerResponse
                                    {
                                        Type = "LOGIN_ERROR",
                                        Content = "Su contraseña debe ser actualizada. Contacte al administrador."
                                    };
                                }

                                if (PasswordManager.VerifyPassword(request.Password, storedHashedPassword))
                                {
                                    return new ServerResponse
                                    {
                                        Type = "LOGIN_SUCCESS",
                                        Content = idUser.ToString(),
                                        Username = request.Username
                                    };
                                }
                                else
                                {
                                    return new ServerResponse
                                    {
                                        Type = "LOGIN_ERROR",
                                        Content = "Contraseña incorrecta."
                                    };
                                }
                            }
                            else
                            {
                                return new ServerResponse
                                {
                                    Type = "LOGIN_ERROR",
                                    Content = "El usuario no existe."
                                };
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error de base de datos: " + ex.Message
                };
            }
        }

        private static async Task<ServerResponse> HandleRegister(ClientRequest request){
            try{
                using (MySqlConnection connection = DbConfig.GetOpenConnection()){
                    // Verificar si el usuario ya existe
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@username", request.Username);
                        int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                        if (count > 0)
                        {
                            return new ServerResponse
                            {
                                Type = "REGISTER_ERROR",
                                Content = "El usuario ya existe."
                            };
                        }
                    }

                    // Hash de la contraseña
                    string hashedPassword = PasswordManager.HashPassword(request.Password);

                    // Insertar nuevo usuario
                    string insertQuery = "INSERT INTO users (username, passwd, creationDate) VALUES (@username, @passwd, @creationDate)";
                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@username", request.Username);
                        insertCommand.Parameters.AddWithValue("@passwd", hashedPassword);
                        insertCommand.Parameters.AddWithValue("@creationDate", DateTime.Now);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    return new ServerResponse
                    {
                        Type = "REGISTER_SUCCESS",
                        Content = "Usuario registrado exitosamente."
                    };
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al registrar el usuario: " + ex.Message
                };
            }
        }

        private static async Task<ServerResponse> HandleSendMessage(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    // Obtener id_user del username
                    string getUserQuery = "SELECT id_user FROM users WHERE username = @username";
                    int idUser;
                    using (MySqlCommand getUserCmd = new MySqlCommand(getUserQuery, connection))
                    {
                        getUserCmd.Parameters.AddWithValue("@username", request.Username);
                        object result = await getUserCmd.ExecuteScalarAsync();
                        if (result == null)
                        {
                            return new ServerResponse
                            {
                                Type = "ERROR",
                                Content = "Usuario no encontrado."
                            };
                        }
                        idUser = Convert.ToInt32(result);
                    }

                    // Insertar mensaje
                    string insertQuery = "INSERT INTO messages (id_chat, id_user, content, sendingDate) VALUES (@id_chat, @id_user, @content, NOW())";
                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@id_chat", request.ChatId);
                        insertCommand.Parameters.AddWithValue("@id_user", idUser);
                        insertCommand.Parameters.AddWithValue("@content", request.Content);
                        await insertCommand.ExecuteNonQueryAsync();
                    }

                    return new ServerResponse
                    {
                        Type = "MESSAGE_SUCCESS",
                        Content = "Mensaje enviado.",
                        Username = request.Username,
                        Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al enviar mensaje: " + ex.Message
                };
            }
        }

        private static async Task<ServerResponse> HandleGetMessages(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    string query = @"SELECT u.username, m.content, m.sendingDate 
                               FROM messages m 
                               INNER JOIN users u ON m.id_user = u.id_user 
                               WHERE m.id_chat = @id_chat 
                               ORDER BY m.sendingDate DESC 
                               LIMIT @count";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_chat", request.ChatId);
                        command.Parameters.AddWithValue("@count", request.Count ?? 50);

                        var messages = new List<MessageData>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                messages.Add(new MessageData
                                {
                                    Username = reader["username"].ToString(),
                                    Content = reader["content"].ToString(),
                                    Timestamp = Convert.ToDateTime(reader["sendingDate"]).ToString("yyyy-MM-dd HH:mm:ss")
                                });
                            }
                        }

                        return new ServerResponse
                        {
                            Type = "MESSAGES_RESPONSE",
                            Messages = messages
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al obtener mensajes: " + ex.Message
                };
            }
        }

        private static async Task<ServerResponse> HandleCreateChat(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    // Obtener id_user del username
                    string getUserQuery = "SELECT id_user FROM users WHERE username = @username";
                    int idUser;
                    using (MySqlCommand getUserCmd = new MySqlCommand(getUserQuery, connection))
                    {
                        getUserCmd.Parameters.AddWithValue("@username", request.Username);
                        object result = await getUserCmd.ExecuteScalarAsync();
                        if (result == null)
                        {
                            return new ServerResponse
                            {
                                Type = "ERROR",
                                Content = "Usuario no encontrado."
                            };
                        }
                        idUser = Convert.ToInt32(result);
                    }

                    // Verificar si el chat ya existe
                    string checkQuery = "SELECT id_chat FROM chats WHERE chat_name = @chat_name";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@chat_name", request.Content);
                        object existingChatId = await checkCmd.ExecuteScalarAsync();

                        if (existingChatId != null)
                        {
                            int chatId = Convert.ToInt32(existingChatId);

                            // Verificar si el usuario ya es miembro
                            string memberCheckQuery = "SELECT COUNT(*) FROM chat_members WHERE id_chat = @id_chat AND id_user = @id_user";
                            using (MySqlCommand memberCheckCmd = new MySqlCommand(memberCheckQuery, connection))
                            {
                                memberCheckCmd.Parameters.AddWithValue("@id_chat", chatId);
                                memberCheckCmd.Parameters.AddWithValue("@id_user", idUser);
                                int memberCount = Convert.ToInt32(await memberCheckCmd.ExecuteScalarAsync());

                                if (memberCount > 0)
                                {
                                    return new ServerResponse
                                    {
                                        Type = "CHAT_EXISTS",
                                        Content = chatId.ToString()
                                    };
                                }
                                else
                                {
                                    // Añadir al usuario al chat existente
                                    string joinQuery = "INSERT INTO chat_members (id_chat, id_user) VALUES (@id_chat, @id_user)";
                                    using (MySqlCommand joinCmd = new MySqlCommand(joinQuery, connection))
                                    {
                                        joinCmd.Parameters.AddWithValue("@id_chat", chatId);
                                        joinCmd.Parameters.AddWithValue("@id_user", idUser);
                                        await joinCmd.ExecuteNonQueryAsync();
                                    }

                                    return new ServerResponse
                                    {
                                        Type = "CHAT_JOINED",
                                        Content = chatId.ToString()
                                    };
                                }
                            }
                        }
                    }

                    // Crear nuevo chat
                    string insertQuery = @"INSERT INTO chats (chat_name, admin_id) VALUES (@chat_name, @admin_id);
                                     SELECT LAST_INSERT_ID();";
                    int newChatId;
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@chat_name", request.Content);
                        insertCmd.Parameters.AddWithValue("@admin_id", idUser);
                        object result = await insertCmd.ExecuteScalarAsync();
                        newChatId = Convert.ToInt32(result);
                    }

                    // Añadir al creador como miembro
                    string addMemberQuery = "INSERT INTO chat_members (id_chat, id_user) VALUES (@id_chat, @id_user)";
                    using (MySqlCommand addMemberCmd = new MySqlCommand(addMemberQuery, connection))
                    {
                        addMemberCmd.Parameters.AddWithValue("@id_chat", newChatId);
                        addMemberCmd.Parameters.AddWithValue("@id_user", idUser);
                        await addMemberCmd.ExecuteNonQueryAsync();
                    }

                    return new ServerResponse
                    {
                        Type = "CHAT_CREATED",
                        Content = newChatId.ToString()
                    };
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al crear chat: " + ex.Message
                };
            }
        }
        private static async Task<ServerResponse> HandleGetChats(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    // Obtener id_user del username
                    string getUserQuery = "SELECT id_user FROM users WHERE username = @username";
                    int idUser;
                    using (MySqlCommand getUserCmd = new MySqlCommand(getUserQuery, connection))
                    {
                        getUserCmd.Parameters.AddWithValue("@username", request.Username);
                        object result = await getUserCmd.ExecuteScalarAsync();
                        if (result == null)
                        {
                            return new ServerResponse
                            {
                                Type = "ERROR",
                                Content = "Usuario no encontrado."
                            };
                        }
                        idUser = Convert.ToInt32(result);
                    }

                    string query = @"SELECT c.id_chat, c.chat_name 
                               FROM chats c 
                               INNER JOIN chat_members cm ON c.id_chat = cm.id_chat 
                               WHERE cm.id_user = @id_user 
                               ORDER BY c.id_chat";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_user", idUser);

                        var chats = new List<MessageData>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                chats.Add(new MessageData
                                {
                                    Content = reader["id_chat"].ToString() + "|" + reader["chat_name"].ToString()
                                });
                            }
                        }

                        return new ServerResponse
                        {
                            Type = "CHATS_RESPONSE",
                            Messages = chats
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al obtener chats: " + ex.Message
                };
            }
        }

        private static async Task<ServerResponse> HandleAddUserToChat(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    // request.Username contiene el username del usuario a añadir
                    // Obtener id_user del username
                    string getUserQuery = "SELECT id_user FROM users WHERE username = @username";
                    int idUser;
                    using (MySqlCommand getUserCmd = new MySqlCommand(getUserQuery, connection))
                    {
                        getUserCmd.Parameters.AddWithValue("@username", request.Username);
                        object result = await getUserCmd.ExecuteScalarAsync();
                        if (result == null)
                        {
                            return new ServerResponse
                            {
                                Type = "ERROR",
                                Content = "Usuario no encontrado."
                            };
                        }
                        idUser = Convert.ToInt32(result);
                    }

                    string insertQuery = "INSERT INTO chat_members (id_chat, id_user) VALUES (@id_chat, @id_user)";
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@id_chat", request.ChatId);
                        insertCmd.Parameters.AddWithValue("@id_user", idUser);
                        await insertCmd.ExecuteNonQueryAsync();
                    }

                    // Obtener el nombre del chat para enviarlo en la notificación
                    string getChatNameQuery = "SELECT chat_name FROM chats WHERE id_chat = @id_chat";
                    string chatName;
                    using (MySqlCommand getChatNameCmd = new MySqlCommand(getChatNameQuery, connection))
                    {
                        getChatNameCmd.Parameters.AddWithValue("@id_chat", request.ChatId);
                        chatName = (await getChatNameCmd.ExecuteScalarAsync())?.ToString() ?? "Chat";
                    }

                    // Notificar al usuario añadido que tiene un nuevo chat
                    await BroadcastChatAdded(request.Username, request.ChatId ?? 0, chatName);

                    return new ServerResponse
                    {
                        Type = "USER_ADDED",
                        Content = "Usuario añadido al chat."
                    };
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al añadir usuario: " + ex.Message
                };
            }
        }

        private static async Task<ServerResponse> HandleRemoveUserFromChat(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    // request.Username contiene el username del usuario a eliminar
                    // Obtener id_user del username
                    string getUserQuery = "SELECT id_user FROM users WHERE username = @username";
                    int idUser;
                    using (MySqlCommand getUserCmd = new MySqlCommand(getUserQuery, connection))
                    {
                        getUserCmd.Parameters.AddWithValue("@username", request.Username);
                        object result = await getUserCmd.ExecuteScalarAsync();
                        if (result == null)
                        {
                            return new ServerResponse
                            {
                                Type = "ERROR",
                                Content = "Usuario no encontrado."
                            };
                        }
                        idUser = Convert.ToInt32(result);
                    }

                    string deleteQuery = "DELETE FROM chat_members WHERE id_chat = @id_chat AND id_user = @id_user";
                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@id_chat", request.ChatId);
                        deleteCmd.Parameters.AddWithValue("@id_user", idUser);
                        await deleteCmd.ExecuteNonQueryAsync();
                    }

                    return new ServerResponse
                    {
                        Type = "USER_REMOVED",
                        Content = "Usuario eliminado del chat."
                    };
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al eliminar usuario: " + ex.Message
                };
            }
        }

        private static async Task<ServerResponse> HandleGetChatMembers(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    string query = @"SELECT u.id_user, u.username 
                               FROM users u 
                               INNER JOIN chat_members cm ON u.id_user = cm.id_user 
                               WHERE cm.id_chat = @id_chat";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_chat", request.ChatId);

                        var members = new List<MessageData>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                members.Add(new MessageData
                                {
                                    Content = reader["id_user"].ToString() + "|" + reader["username"].ToString()
                                });
                            }
                        }

                        return new ServerResponse
                        {
                            Type = "MEMBERS_RESPONSE",
                            Messages = members
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al obtener miembros: " + ex.Message
                };
            }
        }

        private static async Task<ServerResponse> HandleDeleteChat(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    // Primero eliminar los miembros
                    string deleteMembersQuery = "DELETE FROM chat_members WHERE id_chat = @id_chat";
                    using (MySqlCommand deleteMembersCmd = new MySqlCommand(deleteMembersQuery, connection))
                    {
                        deleteMembersCmd.Parameters.AddWithValue("@id_chat", request.ChatId);
                        await deleteMembersCmd.ExecuteNonQueryAsync();
                    }

                    // Luego eliminar los mensajes
                    string deleteMessagesQuery = "DELETE FROM messages WHERE id_chat = @id_chat";
                    using (MySqlCommand deleteMessagesCmd = new MySqlCommand(deleteMessagesQuery, connection))
                    {
                        deleteMessagesCmd.Parameters.AddWithValue("@id_chat", request.ChatId);
                        await deleteMessagesCmd.ExecuteNonQueryAsync();
                    }

                    // Finalmente eliminar el chat
                    string deleteChatQuery = "DELETE FROM chats WHERE id_chat = @id_chat";
                    using (MySqlCommand deleteChatCmd = new MySqlCommand(deleteChatQuery, connection))
                    {
                        deleteChatCmd.Parameters.AddWithValue("@id_chat", request.ChatId);
                        await deleteChatCmd.ExecuteNonQueryAsync();
                    }

                    return new ServerResponse
                    {
                        Type = "CHAT_DELETED",
                        Content = "Chat eliminado exitosamente."
                    };
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al eliminar chat: " + ex.Message
                };
            }
        }

        private static async Task<ServerResponse> HandleGetAllUsers(ClientRequest request)
        {
            try
            {
                using (MySqlConnection connection = DbConfig.GetOpenConnection())
                {
                    string query = "SELECT id_user, username FROM users ORDER BY username";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        var users = new List<MessageData>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                users.Add(new MessageData
                                {
                                    Content = $"{reader["id_user"]}|{reader["username"]}"
                                });
                            }
                        }

                        return new ServerResponse
                        {
                            Type = "USERS_RESPONSE",
                            Messages = users
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServerResponse
                {
                    Type = "ERROR",
                    Content = "Error al obtener usuarios: " + ex.Message
                };
            }
        }

        private static async Task BroadcastMessage(string username, string content, int chatId)
        {
            var broadcastData = new ServerResponse
            {
                Type = "NEW_MESSAGE",
                Username = username,
                Content = content,
                ChatId = chatId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            string json = JsonConvert.SerializeObject(broadcastData);

            lock (clientWriters)
            {
                foreach (var writer in clientWriters)
                {
                    try
                    {
                        writer.WriteLineAsync(json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error broadcasting to client: {ex.Message}");
                    }
                }
            }
        }

        private static async Task BroadcastChatAdded(string targetUsername, int chatId, string chatName)
        {
            var broadcastData = new ServerResponse
            {
                Type = "CHAT_ADDED",
                ChatId = chatId,
                Content = chatName
            };

            string json = JsonConvert.SerializeObject(broadcastData);

            lock (clientWriters)
            {
                foreach (var writer in clientWriters)
                {
                    // Solo enviar al usuario específico que fue añadido
                    if (clientUsernames.TryGetValue(writer, out string username) && username == targetUsername)
                    {
                        try
                        {
                            writer.WriteLineAsync(json);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error broadcasting chat added to client: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}