using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Model;
using test.Utility;

namespace test.Controller
{
    // Controllers/AuthController.cs
    public class AuthController
    {
        public string ShowLoginPage()
        {
            // Return HTML content or login page message
            return "HTTP/1.1 200 OK\n\n<html><body><form method='POST' action='/login'>Username: <input name='username' />Password: <input name='password' /><input type='submit' /></form></body></html>";
        }

        public string HandleLogin(string request)
        {
            // Lấy username và password từ POST request
            string username = RequestHelper.ExtractFormData(request, "username");
            string password = RequestHelper.ExtractFormData(request, "password");

            // Gọi ValidateUser để kiểm tra thông tin đăng nhập
            LoginResult result = UserStore.ValidateUser(username, password);
            if (result != null && result.UserName != null)
            {
                // Trả về phản hồi HTTP với Set-Cookie chứa sessionToken
                return $"HTTP/1.1 302 Found\n" +
                       "Location: /chat\n" +
                       $"Set-Cookie: sessionToken={result.SessionToken}; Path=/; HttpOnly\n\n";
            }
            else
            {
                // Thông báo đăng nhập thất bại
                return "HTTP/1.1 200 OK\n\nLogin failed. Please try again.";
            }
        }

        public string Logout(string request)
        {
            // Lấy sessionToken từ cookie trong request
            string sessionToken = RequestHelper.ExtractCookie(request);

            // Kiểm tra nếu sessionToken tồn tại
            if (!string.IsNullOrEmpty(sessionToken))
            {
                // Xóa người dùng khỏi danh sách đăng nhập dựa trên sessionToken
                UserStore.LogoutUser(sessionToken);
            }

            // Trả về phản hồi chuyển hướng người dùng đến trang đăng nhập
            return "HTTP/1.1 302 Found\nLocation: /login\nSet-Cookie: sessionToken=; Expires=Thu, 01 Jan 1970 00:00:00 GMT; Path=/; HttpOnly\n\n";
        }

    }

}
