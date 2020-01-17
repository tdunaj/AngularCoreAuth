using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoardBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageBoardBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApiContext apiContext;

        public MessagesController(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }
        //static List<Message> messages = new List<Message>{
        //        new Message
        //        {
        //            Owner = "John",
        //            Text = "hello"
        //        },
        //        new Message
        //        {
        //            Owner = "Tim",
        //            Text = "Hi"
        //        }
        //    };

        public IEnumerable<Message> Get()
        {
            return apiContext.Messages;
        }

        [HttpGet("{Name}")]
        public IEnumerable<Message> Get(string name)
        {
            return apiContext.Messages.Where(message => message.Owner == name);
        }

        [HttpPost]
        public Message Post([FromBody] Message message)
        {
            var dbMessage = apiContext.Messages.Add(message).Entity;
            apiContext.SaveChanges();

            return dbMessage;
        }
    }
}