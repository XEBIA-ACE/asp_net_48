using MicroProjectApplication.Models;
using MicroProjectApplication.Services.Interfaces;
using System.Net;
using System.Web.Http;

namespace MicroProjectApplication.Controllers
{
    // US-001 / SVC-002: customer CRUD under /api/v1/customers
    [RoutePrefix("api/v1/customers")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_customerService.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
                return CustomerNotFound(id);

            return Ok(customer);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(Customer customer)
        {
            var created = _customerService.Create(customer);
            if (created == null)
                return BadRequest("Invalid customer.");

            return Ok(created);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, Customer customer)
        {
            if (customer == null)
                return BadRequest("Invalid customer.");

            var updated = _customerService.Update(id, customer);
            if (updated == null)
                return CustomerNotFound(id);

            return Ok(updated);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public IHttpActionResult Patch(int id, Customer customer)
        {
            if (customer == null)
                return BadRequest("Invalid customer.");

            var patched = _customerService.Patch(id, customer);
            if (patched == null)
                return CustomerNotFound(id);

            return Ok(patched);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var deleted = _customerService.Delete(id);
            if (!deleted)
                return CustomerNotFound(id);

            return Ok("Customer deleted successfully.");
        }

        // Standardized Not Found payload for /api/v1/customers/{id} (ErrorResponse placeholder, GAP-02).
        private IHttpActionResult CustomerNotFound(int id)
        {
            return Content(HttpStatusCode.NotFound, new ErrorResponse
            {
                Code = "customer_not_found",
                Message = "Customer with id " + id + " was not found."
            });
        }
    }
}