using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

public static class WebSocketManager
{
    private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

    public static void AddSocket(WebSocket socket)
    {
        string id = Guid.NewGuid().ToString();
        _sockets.TryAdd(id, socket);
    }

    public static async Task BroadcastMessageAsync(string message)
    {
        var tasks = _sockets.Values.Select(async socket =>
        {
            if (socket.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        });

        await Task.WhenAll(tasks);
    }

    public static async Task RemoveSocketAsync(WebSocket socket)
    {
        var id = _sockets.FirstOrDefault(p => p.Value == socket).Key;
        _sockets.TryRemove(id, out _);

        if (socket != null)
        {
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by server", CancellationToken.None);
            socket.Dispose();
        }
    }
}
