using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;

        public OrderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var response = await httpClient.GetAsync($"/api/v1/Order/{userName}");
            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}

