using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.Data;
using server.Model;
using server.Model.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace server.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        private BookshelfDBContext _DBContext;
        public UserController(BookshelfDBContext dBContext, IConfiguration config)
        {
            _DBContext = dBContext;
            _config = config;
        }


        [HttpGet]
        public IActionResult getAllUsers()
        {
            return Ok(_DBContext.Users);
        }



        [HttpGet("getUser")]
        public IActionResult getUser(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var user = _DBContext.Users.Find(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost("createUser")]
        public IActionResult createUser([FromBody] UserDTO user)
        {
            if (user == null)
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
            if (id == 0)
            {
                return BadRequest();
            }

            var dbUser = _DBContext.Users.FirstOrDefault(u => u.Id == id);

            if (dbUser == null)
            {
                return NotFound();
            }

            _DBContext.Users.Remove(dbUser);

            _DBContext.SaveChanges();

            return Ok("User is Deleted");
        }


        [HttpPut("updateUser")]
        public IActionResult updateUser([FromBody] UserDTO user, int id)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var dbUser = _DBContext.Users.Find(id);
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


        [HttpPut("updateBookNumber")]
        public IActionResult updateBookNumber(int id, bool mode)
        {
            // mode = true => increase the bookNumber
            // mode = false => decrease the bookNumber
            if(id == 0)
            {
                return BadRequest();
            }

            var user = _DBContext.Users.Find(id);
            if(user == null)
            {
                return NotFound();
            }
            if(mode)
            {
                user.BookNum += 1;

            }
            else
            {
                user.BookNum -= 1;
            }


            _DBContext.Users.Update(user);

            _DBContext.SaveChanges();

            return Ok("The number of Books is Updated!");
        }

        [HttpPost("userLogin")]
        public IActionResult userLogin(string Username, string Password)
        {
            if(Username.IsNullOrEmpty() || Password.IsNullOrEmpty())
            {
                return BadRequest();
            }
            var user = _DBContext.Users.FirstOrDefault(x=>x.Name == Username && x.Password == Password);
            if(user == null)
            {
                return NotFound();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_config["JwtSettings:Issuer"],
              _config["JwtSettings:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            return Ok(token);
        }

    }
    
}
