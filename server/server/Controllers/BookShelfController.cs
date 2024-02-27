using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Model.DTO;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookShelfController : ControllerBase
    {
        [HttpGet]
        public ActionResult getAllBooks()
        {
            //Receive Token and return all books based on that. 
            return Ok();
        }
        [HttpGet("getBook")]
        public ActionResult getBook(int id)
        {
            //Receive Token and id and search for id in database
            if(id == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("storeBook")]
        public ActionResult storeBook(BookDTO bookDTO)
        {

            //Receive Token and bookDTO and store it in database
            if (bookDTO == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("removeBook")]
        public ActionResult removeBook(int id)
        {
            //Receive Token and id and receive book based on that. 
            if(id == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
