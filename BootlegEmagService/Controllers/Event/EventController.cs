using BootlegEmagService.Controllers.Event.Http;
using BootlegEmagService.Events;
using BootlegEmagService.Events.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private EventFacade Facade { get; set; }

        public EventController()
        {
            Facade = new EventFacade();
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
