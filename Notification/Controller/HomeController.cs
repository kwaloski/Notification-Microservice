using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notification.Model;
using Notification.Services;

namespace Notification.Controller
{
    
    public class HomeController : ControllerBase
    {
        IMessageConsumer _messageConsumer;
            public HomeController(IMessageConsumer messageConsumer)
        {
            _messageConsumer = messageConsumer;
        }
        
        [HttpGet("consume")]
        public List<Device> Consume()
        {
           return _messageConsumer.Consume();
        }

    }
}
