using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Model;

namespace test
{
    public class  ChatFacade
    {
        private readonly Model.ChatRoomFactory _chatRoomFactory;
        private readonly Dictionary<int, ChatRoom> _chatRooms;

        public ChatFacade()
        {
            _chatRoomFactory = new ChatRoomFactory();
            _chatRooms = new Dictionary<int, ChatRoom>();
            InitializeChatRooms();
        }

        private void InitializeChatRooms()
        {
            // Initialize two chat rooms for demonstration purposes
            _chatRooms[1] = _chatRoomFactory.CreateChatRoom(1);
            _chatRooms[2] = _chatRoomFactory.CreateChatRoom(2);
        }

        public string GetChatRoomsStatus()
        {
            // Returns JSON-like status of chat rooms
            List<string> roomStatuses = new List<string>();
            foreach (var room in _chatRooms.Values)
            {
                roomStatuses.Add($"{{\"id\": {room.Id}, \"status\": \"{(room.IsActive ? "online" : "offline")}\"}}");
            }
            return $"[{string.Join(", ", roomStatuses)}]";
        }

        public void SendMessageToRoom(int roomId, Message message)
        {
            if (_chatRooms.ContainsKey(roomId))
            {
                _chatRooms[roomId].AddMessage(message);
                Console.WriteLine(GetMessagesFromRoom(roomId).Count);
                _chatRooms[roomId].Activate();          
            }
        }

        public List<Message> GetMessagesFromRoom(int roomId)
        {
            return _chatRooms.ContainsKey(roomId) ? _chatRooms[roomId].Messages : new List<Message>();
        }
    }
}
