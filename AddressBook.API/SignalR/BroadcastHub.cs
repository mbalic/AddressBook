using Microsoft.AspNetCore.SignalR;

namespace AddressBook.API.SignalR
{
    public class BroadcastHub : Hub<IHubClient>
    {
    }
}