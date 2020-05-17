using IT.Employer.Services.Models.Chat;
using Microsoft.AspNetCore.SignalR;

namespace IT.Employer.Services.HubN
{
    public class ChatHub : Hub
    {
        public void SendMessage(Message message)
        {
            Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
