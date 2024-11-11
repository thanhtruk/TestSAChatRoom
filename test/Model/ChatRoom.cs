using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Model
{
    public class ChatRoom
    {
        public int Id { get; }
        public bool IsActive { get; private set; }
        public DateTime LastActiveTime { get; private set; }
        public List<Message> Messages { get; }

        public ChatRoom(int id)
        {
            Id = id;
            Messages = new List<Message>();
            IsActive = false;
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
            Activate();
        }

        public void Activate()
        {
            IsActive = true;
            LastActiveTime = DateTime.Now;
        }

        public void DeactivateIfInactive(TimeSpan timeout)
        {
            if ((DateTime.Now - LastActiveTime) > timeout)
            {
                IsActive = false;
                Messages.Clear(); // Clear messages if the room becomes inactive
            }
        }
    }

    public class ChatRoomFactory
    {
        public ChatRoom CreateChatRoom(int roomId)
        {
            return new ChatRoom(roomId);
        }
    }
}
