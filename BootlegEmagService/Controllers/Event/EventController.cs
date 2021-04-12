using BootlegEmagService.Controllers.Event.Http;
using BootlegEmagService.Events;
using BootlegEmagService.Events.Model;
using Microsoft.AspNetCore.Mvc;

namespace BootlegEmagService.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private EventFacade Facade;

        public EventController(EventFacade facade)
        {
            Facade = facade;
        }

        [HttpGet("{userId}")]
        public string Get(string userId)
        {
            return userId;
        }

        [Route("useraction")]
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserActionRequest userActionRequest)
        {
            var userAction = new UserActionEvent { Id = "0", UserId = userActionRequest.UserId, Type = userActionRequest.Type };
            var result = Facade.RegisterUserAction(userAction);
            var response = new RegisterUserActionResponse { HasDiscount = result };
            return Ok(response);
        }

        [Route("productaction")]
        [HttpPost]
        public IActionResult Post([FromBody] ProductActionEvent productAction)
        {
            Facade.RegisterProductAction(productAction);
            return Ok();
        }
    }
}
