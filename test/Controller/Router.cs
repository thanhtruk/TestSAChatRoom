using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test.Controller
{
    // Router.cs
    public class Router
    {
        private readonly ChatController _chatController = new ChatController();
        private readonly AuthController _authController = new AuthController();
        private readonly StudentController _studentController = new StudentController();

        public string RouteRequest(string request)
        {
            if (request.StartsWith("GET / "))
            {
                return _studentController.GetStudentInfo();
            }
            else if (request.StartsWith("GET /login"))
            {
                return _authController.ShowLoginPage();
            }
            else if (request.StartsWith("POST /login"))
            {
                return _authController.HandleLogin(request);
            }
            else if (request.StartsWith("GET /logout"))
            {
                return _authController.Logout(request);
            }
            else if (request.StartsWith("GET /chat "))
            {
                return _chatController.GetChatRooms();
            }
            else if (Regex.IsMatch(request, @"^POST /chat/\d+"))
            {
                return _chatController.SendMessage(request);
            }
            else if (Regex.IsMatch(request, @"^GET /chat/\d+"))
            {
                return _chatController.GetChatMessages(request);
            }
            else
            {
                return NotFound();
            }
        }

        private string NotFound()
        {
            return "HTTP/1.1 404 Not Found\n\nPage not found.";
        }
    }

}
