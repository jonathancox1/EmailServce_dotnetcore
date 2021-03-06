using EmailAPI.EmailServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace EmailAPI.Controllers
{
    [ApiController]
    public class OrderConfirmationController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IConfiguration _config;

        public OrderConfirmationController(IMessageService messageService, IConfiguration config)
        {
            _messageService = messageService;
            _config = config;
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("/order/success")]
        public IActionResult OrderSuccess([FromBody]MessageService requestBody)
        {
            _messageService.EmailBuilderAsync(requestBody, "We've got your order!", "success", _config);
            return Ok();
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [Route("/order/shipped")]
        public IActionResult OrderShipped([FromBody] MessageService requestBody)
        {
            _messageService.EmailBuilderAsync(requestBody, "Your Order Has Shipped!", "shipped", _config);
            return new EmptyResult();
        }
    }
}
