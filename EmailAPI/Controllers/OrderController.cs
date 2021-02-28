using EmailAPI.EmailServices;
using Microsoft.AspNetCore.Mvc;

namespace EmailAPI.Controllers
{
    [ApiController]
    public class OrderConfirmationController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public OrderConfirmationController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [Route("/order/success")]
        public IActionResult OrderSuccess([FromBody] MessageService requestBody)
        {
            _messageService.EmailBuilderAsync(requestBody, "We've got your order!", "success");
            return new EmptyResult();
        }

        [HttpPost]
        [Route("/order/shipped")]
        public IActionResult OrderShipped([FromBody] MessageService requestBody)
        {
            _messageService.EmailBuilderAsync(requestBody, "Your Order Has Shipped!", "shipped");
            return new EmptyResult();
        }
    }
}
