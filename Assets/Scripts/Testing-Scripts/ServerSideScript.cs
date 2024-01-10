using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServerSideScript : MonoBehaviour
{
    static async Task Main()
    {
        await StartServerAsync();
    }

    static async Task StartServerAsync()
    {
        TcpListener server = new(IPAddress.Any, 5000); // Use the desired port
        server.Start();

        Console.WriteLine($"Server is listening on port {5000}...");

        while (true)
        {
            Console.WriteLine($"A client connected");
            TcpClient client = await server.AcceptTcpClientAsync();
            _ = Task.Run(() => HandleClientAsync(client));
        }
    }

    static async Task HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = await stream.ReadAsync(buffer)) != 0)
        {
            Console.WriteLine($"Sending Data to Client");
            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received data: {receivedData}");

            //string responseData = "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\nHello from the server!";
            string responseData = $"HTTP/1.1 200 OK\r\n" +
                $"Content-Type: text/plain\r\n" +
                $"Modified data from server: {receivedData} - Appended";
            byte[] responseBuffer = Encoding.UTF8.GetBytes(responseData);

            // Send the response
            await stream.WriteAsync(responseBuffer);
        }

        stream.Close();
        client.Close();
    }
}
