using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Model;
using server.Model.DTO;

namespace server.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private BookshelfDBContext _DBContext;
        public UserController(BookshelfDBContext dBContext)
        {
            _DBContext = dBContext;
        }


        [HttpGet]
        public IActionResult getAllUsers()
        {
            return Ok(_DBContext.Users);
        }


        [HttpPost("createUser")]
        public IActionResult createUser([FromBody] UserDTO user)
        {
            if(user == null)
            {
                return BadRequest();
            }

            User newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Score = 0,
                BookNum = 0
            };

            _DBContext.Users.Add(newUser);
            _DBContext.SaveChanges();
            return Ok(newUser);

        }

        [HttpDelete("removeUser")]
        public IActionResult removeUser(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var dbUser = _DBContext.Users.FirstOrDefault(u => u.Id == id);

            if(dbUser == null)
            {
                return NotFound();
            }

            _DBContext.Users.Remove(dbUser);

            _DBContext.SaveChanges();

            return Ok("User is Deleted");
        }

        [HttpPut("updateUser")]

        public IActionResult updateUser([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var dbUser = _DBContext.Users.Find(user);
            if (dbUser == null)
            {
                return NotFound();
            }

            User newUser = new User
            {
                Name = dbUser.Name,
                Email = dbUser.Email,
                Password = dbUser.Password,
            };

            _DBContext.Users.Update(newUser);
            _DBContext.SaveChanges();

            return Ok("User is updated");
        }
    }
}
