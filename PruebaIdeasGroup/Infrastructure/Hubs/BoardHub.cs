namespace PruebaIdeasGroup.Infrastructure.Hubs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class BoardHub : Hub
{
    public async Task JoinProjectBoard(string projectId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Project_{projectId}");
    }

    public async Task LeaveProjectBoard(string projectId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Project_{projectId}");
    }
}