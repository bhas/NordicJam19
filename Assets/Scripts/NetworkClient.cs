using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Text;
using UnityEngine;

public class NetworkClient : MonoBehaviour
{
    private static readonly Uri location = new Uri("wss://vast-lake-26314.herokuapp.com/");
    private static readonly ClientWebSocket cws = new ClientWebSocket();
    private static readonly int sendTimeout = 1000;
    private static readonly int connectionTimeout = 1000;
    private static readonly Dictionary<string, MessageHandler> _handlers = new Dictionary<string, MessageHandler>();

    private static Task<String> message;

    public delegate void MessageHandler(int x, int y);

    private void Log(string message)
    {
        // Debug.Log(message);
    }

    private async Task Connect()
    {
        using (var cts = new CancellationTokenSource(connectionTimeout))
            await cws.ConnectAsync(location, cts.Token);
    }

    public static async Task Send(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        var segment = new ArraySegment<byte>(buffer, 0, buffer.Length);
        WebSocketReceiveResult recvResult;
        using (var cts = new CancellationTokenSource(sendTimeout))
            await cws.SendAsync(segment, WebSocketMessageType.Text, true, cts.Token);
    }

    private async Task<String> Receive()
    {
        var buffer = new byte[2048];
        var segment = new ArraySegment<byte>(buffer, 0, buffer.Length);
        Log("Listening for message...");
        WebSocketReceiveResult recvResult;
        recvResult = await cws.ReceiveAsync(segment, CancellationToken.None);

        var message = Encoding.UTF8.GetString(buffer, 0, recvResult.Count);
        Log("Received message: " + message);
        return message;
    }

    async Task SetPlayerName(string name)
    {
        await Send(name);
    }

    private void PollMessage()
    {
        if (message != null && message.IsCompleted)
        {
            var messageText = message.Result;
            var tokens = messageText.Split(' ');
            if (tokens.Length != 3)
                throw new Exception("Invalid command received!");
            var command = tokens[0];
            var x = int.Parse(tokens[1]);
            var y = int.Parse(tokens[2]);
            Log("Message received: " + messageText);
            if (_handlers.ContainsKey(command))
                _handlers[command](x, y);
            message = Receive();
        }
    }

    public static void RegisterHandler(string message, MessageHandler handler)
    {
        _handlers.Add(message, handler);
    }

    async void Start()
    {
        Log("Connecting");
        await Connect();
        Log("Connected.");
        await SetPlayerName("Player 1");
        Log("Player name set.");
        message = Receive();
    }

    void FixedUpdate()
    {
        PollMessage();
    }

    async void OnDestroy()
    {
        await cws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }
}