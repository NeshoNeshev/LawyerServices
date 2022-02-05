using LawyerServices.Services.Data.AdminServices;
using Microsoft.AspNetCore.SignalR;

namespace LawyerServices.Web.Hubs
{
    public class SubmitApplicationRequestHub : Hub
    {
        private readonly IRequestsService requestsService;

        public SubmitApplicationRequestHub(IRequestsService requestsService)
        {
            this.requestsService = requestsService;
        }

        public async Task GetSubmitApplicationRequest()
        { 
        
        }
    }
}
