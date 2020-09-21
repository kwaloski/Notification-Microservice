
using Notification.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Services
{
    public interface IMessageConsumer
    {
        List<Device> Consume();
    }
}
