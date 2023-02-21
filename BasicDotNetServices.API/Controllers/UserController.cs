using BasicDotNetServices.Core.Model;
using BasicDotNetServices.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BasicDotNetServices.API.Controllers
{
    [ApiController] // Telling its API Controller not MVC Controller
    [Route("api/[controller]")] // It will take name from the controller name
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork) // Inject DbContext
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DBContact>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]

        public IActionResult GetUser()
        {
            IEnumerable<User> usersList = _unitOfWork.User.GetAll();
            return Ok(usersList);
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DBContact))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("{id:long}")]
        public async Task<IActionResult> GetUser([FromRoute] long id)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Id == id);
            if (user == null)
            {
                return NoContent();
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FluentValidation.Results.ValidationResult))]
        //Return type of async method is either void or , Task<t>
        public async Task<IActionResult> AddUser([FromBody] User data)
        {
            //data.id = Guid.NewGuid();
            _unitOfWork.User.Add(data);
            _unitOfWork.Save();
            return Ok(data);

        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FluentValidation.Results.ValidationResult))]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, User data)
        {
            _unitOfWork.User.Update(data);
            _unitOfWork.Save();
            return Ok(data);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(e => e.Id == id);
            if (user != null)
            {
                _unitOfWork.User.Remove(user);
                _unitOfWork.Save();
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FluentValidation.Results.ValidationResult))]
        //Return type of async method is either void or , Task<t>
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthorizeModel data)
        {
            var user =_unitOfWork.User.Authenticate(data);
            if (user == null)
            {
                return NoContent();
            }
            return Ok(user);
        }
    }
}
