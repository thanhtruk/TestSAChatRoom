using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using test.Model;
using test.Utility;

namespace test.Controller
{
    // Controllers/ChatController.cs
    public class ChatController
    {
        private static readonly ChatFacade _chatFacade = new ChatFacade();

        public string GetChatRooms()
        {
            // Use ChatFacade to get chat room statuses
            string chatRoomStatuses = _chatFacade.GetChatRoomsStatus();
            return $"HTTP/1.1 200 OK\n\n{chatRoomStatuses}";
        }

        public string SendMessage(string request)
        {
            int chatRoomId = ExtractChatRoomId(request);
            string sessionToken = RequestHelper.ExtractCookie(request); // Lấy sessionToken từ cookie
            string username = UserStore.GetLoggedInUsername(sessionToken); // Xác định username qua sessionToken
            string messageContent = RequestHelper.ExtractFormData(request, "message");

            // Giải mã nội dung tin nhắn để xử lý ký tự đặc biệt
            messageContent = HttpUtility.UrlDecode(messageContent);

            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (string.IsNullOrEmpty(username))
            {
                return "HTTP/1.1 401 Unauthorized\n\nPlease log in to send a message.";
            }

            // Kiểm tra nội dung tin nhắn
            if (!string.IsNullOrEmpty(messageContent))
            {
                Message message = new Message(username, messageContent);
                _chatFacade.SendMessageToRoom(chatRoomId, message);
                return $"HTTP/1.1 302 Found\nLocation: /chat/{chatRoomId}\n\n";
            }

            // Trả về lỗi nếu thiếu nội dung tin nhắn
            return "HTTP/1.1 400 Bad Request\n\nMessage content missing.";
        }


        public string GetChatMessages(string request)
        {
            int chatRoomId = ExtractChatRoomId(request);
            var messages = _chatFacade.GetMessagesFromRoom(chatRoomId);
            Console.WriteLine(messages.Count);
            string chatMessagesHtml = "";

            // Generate HTML for each message without adding extra <html> or <body> tags
            foreach (var message in messages)
            {
                chatMessagesHtml += $"<p><strong>{message.Username}:</strong> {message.Content}</p>";
            }

            // Return the complete HTML response with only one \n\n after headers
            return $@"
HTTP/1.1 200 OK
Content-Type: text/html


<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Chat Room {chatRoomId}</title>
</head>
<body>
    <h1>Chat Room {chatRoomId}</h1>
    <div id='chat-messages'>
        {chatMessagesHtml}
    </div>
    <form action='/chat/{chatRoomId}' method='POST'>
        <label for='message'>Enter your message:</label>
        <input type='text' id='message' name='message' required>
        <button type='submit'>Send</button>
    </form>
 <a href='/logout'>Logout</a>
</body>
</html>";
        }

        private int ExtractChatRoomId(string request)
        {
            // Example: "POST /chat/1" or "GET /chat/1"
            var parts = request.Split(' ');
            var idPart = parts[1].Split('/');
            return int.TryParse(idPart.Last(), out int roomId) ? roomId : -1;
        }
    }

}
