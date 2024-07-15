using Application.Contracts.UserContracts;
using Microsoft.AspNetCore.Mvc;

namespace Recipe.Api.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }


        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _userApplication.SelectAllUsers();
            return Ok(result);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userApplication.FindUser(u=>u.Id==id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserCommand userCmd)
        {
            int? userId;
            var isSuccess =_userApplication.AddUser(userCmd,out userId);
            if (userId.HasValue&&isSuccess)
            {
                var url = Url.Action(nameof(Get),"User",new {id = userId},Request.Scheme);
                return Created(url,userId);
            }
            return BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
