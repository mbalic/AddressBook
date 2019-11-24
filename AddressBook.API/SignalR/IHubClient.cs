using System.Threading.Tasks;

namespace AddressBook.API.SignalR
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}