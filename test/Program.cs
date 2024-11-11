// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;
using System.Text;
using test.Model;
using System.Web;
using test.Controller;

 TcpListener listener;

        UserStore.LoadUsers("E:\\baitap\\SA\\test\\test\\user.json");
        UserStore.PrintAllUsers();
        listener = new TcpListener(IPAddress.Any, 8080);
        listener.Start();
        Console.WriteLine("Server started on port 8080...");

while (true)
{
    TcpClient client = listener.AcceptTcpClient();
    HandleClient(client);

    static void HandleClient(TcpClient client)
    {
        Router router = new Router();
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine(request);

        // Process request and generate response
        string response = router.RouteRequest(request);

        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
        stream.Write(responseBytes, 0, responseBytes.Length);
        client.Close();
    }
}

   

