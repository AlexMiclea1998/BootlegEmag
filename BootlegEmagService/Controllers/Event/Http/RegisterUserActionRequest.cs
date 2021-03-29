using BootlegEmagService.Events.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Controllers.Event.Http
{
    public class RegisterUserActionRequest
    {
        public string UserId { get; set; }

        public UserActionType Type { get; set; }
    }
}
