using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace test.Model
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class LoginResult
    {
        public string UserName { get; set; }
        public string SessionToken { get; set; }
    }

    public static class UserStore
    {
        private static List<User> users;
        private static Dictionary<string, string> loggedInUsers = new Dictionary<string, string>();

        public static void LoadUsers(string filePath)
        {
            string json = File.ReadAllText(filePath);
            users = JsonSerializer.Deserialize<List<User>>(json);
        }

        public static LoginResult ValidateUser(string username, string password)
        {
            var user = users?.FirstOrDefault(u => u.username == username && u.password == password);
            if (user != null)
            {
                string sessionToken = Guid.NewGuid().ToString();
                Console.WriteLine("Cookie vua tao: " + sessionToken);
                loggedInUsers[sessionToken] = username;
                Console.WriteLine(loggedInUsers[sessionToken]);
                return new LoginResult
                {
                    UserName = loggedInUsers[sessionToken],
                    SessionToken = sessionToken
                };
            }
            return null;
        }
        public static void PrintAllUsers()
        {
            if (users == null || users.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }

            Console.WriteLine("List of Users:");
            foreach (var user in users)
            {
                Console.WriteLine($"Username: {user.username}, Password: {user.password}");
            }
        }

        public static bool LogoutUser(string sessionToken)
        {
            return loggedInUsers.Remove(sessionToken);
        }

        public static string GetLoggedInUsername(string sessionToken)
        {
            sessionToken = sessionToken.Trim();
            Console.WriteLine("hiZ:"+loggedInUsers[sessionToken]);
            if (loggedInUsers[sessionToken] != null)
            {
                return loggedInUsers[sessionToken];
            }
            return null;
        }

        public static void PrintLoggedInUsers()
        {
            foreach (var kvp in loggedInUsers)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }
        }

    }
}
