using BasicDotNetServices.Core.Model;
using BasicDotNetServices.Core.Validator;
using BasicDotNetServices.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Xml.Linq;

namespace BasicDotNetServices.API.Controllers
{
    [ApiController] // Telling its API Controller not MVC Controller
    [Route("api/[controller]")] // It will take name from the controller name
    public class ContactsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ContactsController(IUnitOfWork unitOfWork) // Inject DbContext
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DBContact>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Contact>))]

        public IActionResult GetContacts()
        {
             IEnumerable<Contact> contactsList = _unitOfWork.Contact.GetAll();
             return Ok(contactsList);
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DBContact))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = _unitOfWork.Contact.GetFirstOrDefault(e => e.id == id);
            if(contact == null)
            {
                return NoContent();
            }
            return Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FluentValidation.Results.ValidationResult))]
        //Return type of async method is either void or , Task<t>
        public async Task<IActionResult> AddContact([FromBody] Contact data)
        {
            data.id = Guid.NewGuid();
            _unitOfWork.Contact.Add(data);
            _unitOfWork.Save();
            return Ok(data);

        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FluentValidation.Results.ValidationResult))]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, Contact data)
        {
            _unitOfWork.Contact.Update(data);
            _unitOfWork.Save();
            return Ok(data);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var category = _unitOfWork.Contact.GetFirstOrDefault(e => e.id == id);
            if (category != null)
            {
                _unitOfWork.Contact.Remove(category);
                _unitOfWork.Save();
                return Ok(category);
            }
            return NotFound();
        }
    }
}
