using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class ChatServer
{
    // Mantiene una lista de todos los "StreamWriters" de los clientes conectados.
    // Usamos StreamWriter para enviar texto fácilmente.
    private static readonly List<StreamWriter> clientWriters = new List<StreamWriter>();

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Server Initialized on port 9000...");
        TcpListener listener = new TcpListener(IPAddress.Any, 9000);
        listener.Start();

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

        // Añade el "escritor" de este cliente a la lista.
        lock (clientWriters)
        {
            clientWriters.Add(writer);
        }

        try
        {
            while (true)
            {
                // Espera a recibir un mensaje del cliente
                string message = await reader.ReadLineAsync();
                if (message == null) break; // El cliente se desconectó

                Console.WriteLine($"{message}");

                // Retransmite el mensaje a todos los demás clientes
                await BroadcastMessage(message);
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
            }
            client.Close();
        }
    }

    private static async Task BroadcastMessage(string message)
    {
        // 'lock' para asegurar que la lista no cambie mientras la recorremos
        lock (clientWriters)
        {
            foreach (var writer in clientWriters)
            {
                // Envía el mensaje a cada cliente
                writer.WriteLineAsync(message);
            }
        }
    }
}