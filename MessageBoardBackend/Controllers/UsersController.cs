using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBoardBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageBoardBackend.Controllers
{
    public class EditProfileData 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext apiContext;

        public UsersController(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            var user = apiContext.Users.SingleOrDefault(user => user.Id == id);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);

        }

        [Authorize]
        [HttpGet("me")]
        public ActionResult Get() 
        {            
            return Ok(GetSecureUser());
        }

        [Authorize]
        [HttpPost("me")]
        public ActionResult Post([FromBody] EditProfileData profileData)
        {
            var user = GetSecureUser();

            user.FirstName = profileData.FirstName ?? user.FirstName;
            user.LastName = profileData.LastName ?? user.LastName;

            apiContext.SaveChanges();
            
            return Ok(user);
        }

        private User GetSecureUser()
        {
            var id = HttpContext.User.Claims.First().Value;

            return apiContext.Users.SingleOrDefault(u => u.Id == id);
        }
    }
}