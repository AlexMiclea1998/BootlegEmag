using BootlegEmagService.Events.Model;
using BootlegEmagService.Events.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Events
{
    public class EventFacade
    {
        private EventRepository Repository { get; set; }

        public EventFacade()
        {
            Repository = new EventRepository();
        }

        public void RegisterUserAction(UserActionEvent userAction)
        {
            Repository.RegisterUserAction(userAction);
        }

        public void RegisterProductAction(ProductActionEvent productAction)
        {
            Repository.RegisterProductAction(productAction);
        }
    }
}
