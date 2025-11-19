using System.Collections.Generic;

namespace UniChat
{
    public class ClientRequest
    {
        public string Command { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Content { get; set; }
        public int? Count { get; set; }
        public int? ChatId { get; set; } // Para soporte de múltiples chats
    }

    public class ServerResponse
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public string Timestamp { get; set; }
        public List<MessageData> Messages { get; set; }
        public int? ChatId { get; set; } // Para soporte de múltiples chats
    }

    public class MessageData
    {
        public string Username { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
    }
}