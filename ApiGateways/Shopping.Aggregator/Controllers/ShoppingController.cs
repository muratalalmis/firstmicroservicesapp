using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService catalogService;
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;

        public ShoppingController(
            ICatalogService catalogService,
            IBasketService basketService,
            IOrderService orderService)
        {
            this.catalogService = catalogService;
            this.basketService = basketService;
            this.orderService = orderService;
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            // get basket with username
            // itarete basket items and consme products with basket item productId member
            // map product related members into basketitem dto with extended columns
            // consume ordering microservices in order to retrieve order list
            // return root ShoppingModel dto class which including all responses

            var basket = await basketService.GetBasket(userName);

            foreach (var item in basket.Items)
            {
                var product = await catalogService.GetCatalog(item.ProductId);

                // set addtional product fields onto basketitem
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await orderService.GetOrdersByUserName(userName);
            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return shoppingModel;
        }
    }
}

