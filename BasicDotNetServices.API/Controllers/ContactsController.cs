using BasicDotNetServices.Core.Model;
using BasicDotNetServices.Core.Validator;
using BasicDotNetServices.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
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
            /*
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact()
            {
                id = new Guid("74901648-8341-4784-ad5b-ad649ffc7ff4"),
                Name = "Test1",
                Email = "sasas111@email.com",
                Phone = 9999999991,
                Address = "AddressAddress1"
            }
            );
            contacts.Add(new Contact()
            {
                id = new Guid("0ef01d7e-42ba-4f23-8a1f-1dff394440be"),
                Name = "Test2",
                Email = "sasas2222@email.com",
                Phone = 9999999992,
                Address = "AddressAddress2"
            }
            );
            return Ok(contacts);
            */

        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DBContact))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            /*
            DBContact dBcontact = await dbContxt.Contacts.FindAsync(id);
            if (dBcontact != null)
            {
                return Ok(dBcontact);
            }
            */
            /* Latest */
            
            var contact = _unitOfWork.Contact.GetFirstOrDefault(e => e.id == id);
            if(contact == null)
            {
                return NoContent();
            }
            return Ok(contact);
            /*
            
            return Ok(new Contact()
            {
                id = new Guid("74901648-8341-4784-ad5b-ad649ffc7ff4"),
                Name = "Test1",
                Email = "sasas111@email.com",
                Phone = 9999999991,
                Address = "AddressAddress1"
            });
            */
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FluentValidation.Results.ValidationResult))]
        //Return type of async method is either void or , Task<t>
        public async Task<IActionResult> AddContact([FromBody] Contact data)
        {
            /*
            var contactValidator = new ContactValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = contactValidator.Validate(data);

            if (result.IsValid)
            {
                DBContact contact = new DBContact()
                {
                    Id = Guid.NewGuid(),
                    Name = data.Name,
                    Email = data.Email,
                    Phone = data.Phone,
                    Address = data.Address
                };

                await dbContxt.Contacts.AddAsync(contact);
                await dbContxt.SaveChangesAsync();
                return Ok(contact);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
            */
            /*  Latest */
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
            /*
            var dBcontact = await dbContxt.Contacts.FindAsync(id);

            if (dBcontact != null)
            {
                dBcontact.Name = data.Name;
                dBcontact.Email = data.Email;
                dBcontact.Phone = data.Phone;
                dBcontact.Address = data.Address;

                await dbContxt.SaveChangesAsync();
                return Ok(dBcontact);
            }
            */
            /* Latest */

            _unitOfWork.Contact.Update(data);
            _unitOfWork.Save();
            return Ok(data);
            //return NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            /*
            var dBcontact = await dbContxt.Contacts.FindAsync(id);
            if (dBcontact != null)
            {
                dbContxt.Remove(dBcontact);
                await dbContxt.SaveChangesAsync();
                return Ok(dBcontact);
            }
            */
            /* Latest */
            
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
