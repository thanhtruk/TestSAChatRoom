using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Model
{
    public class Message
    {
        public DateTime Time { get; }
        public string Username { get; }
        public string Content { get; }

        public Message(string username, string content)
        {
            Time = DateTime.Now;
            Username = username;
            Content = content;
        }

        public override string ToString()
        {
            return $"{{\"time\": \"{Time:HH:mm}\", \"username\": \"{Username}\", \"message\": \"{Content}\"}}";
        }
    }
}
