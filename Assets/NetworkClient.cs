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
    private readonly Uri location = new Uri("wss://vast-lake-26314.herokuapp.com/");
    private readonly ClientWebSocket cws = new ClientWebSocket();
    private readonly int sendTimeout = 1000;
    private readonly int receiveTimeout = 60000;
    private readonly int connectionTimeout = 1000;

    private async Task Connect()
    {
        using (var cts = new CancellationTokenSource(connectionTimeout))
            await cws.ConnectAsync(location, cts.Token);
    }

    public async Task Send(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        var segment = new ArraySegment<byte>(buffer, 0, buffer.Length);
        WebSocketReceiveResult recvResult;
        using (var cts = new CancellationTokenSource(sendTimeout))
            await cws.SendAsync(segment, WebSocketMessageType.Text, true, cts.Token);
    }

    public async Task<String> Receive()
    {
        var buffer = new byte[2048];
        var segment = new ArraySegment<byte>(buffer, 0, buffer.Length);
        Debug.Log("Listening for message...");
        WebSocketReceiveResult recvResult;
        using (var cts = new CancellationTokenSource(receiveTimeout))
            recvResult = await cws.ReceiveAsync(segment, cts.Token);

        var message = Encoding.UTF8.GetString(buffer, 0, recvResult.Count);
        Debug.Log("Received message: " + message);
        return message;
    }

    async Task SetPlayerName(string name)
    {
        await Send(name);
    }

    async void Start()
    {
        Debug.Log("Connecting");
        await Connect();
        Debug.Log("Connected.");
        await SetPlayerName("Player 1");
        Debug.Log("Player name set.");
    }

    async void OnDestroy()
    {
        await cws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }
}