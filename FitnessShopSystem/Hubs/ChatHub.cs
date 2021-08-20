namespace FitnessShopSystem.Hubs
{
    using System.Threading.Tasks;

    using FitnessShopSystem.Models.Chat;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("NewMessage", new Message { User = this.Context.User.Identity.Name, Text = message });
        
        }
    }
}
