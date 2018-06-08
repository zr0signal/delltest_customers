using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DellTest.Customers.Service.Models;

namespace DellTest.Customers.Service.Controllers
{
    public class CustomerController : ApiController
    {
        #region Constuctors

        private readonly ICustomerServiceContext _ctx;

        public CustomerController()
        {
            _ctx = new CustomerServiceContext();
        }

        public CustomerController(ICustomerServiceContext ctx)
        {
            _ctx = ctx;
        }

        ~CustomerController()
        {
            _ctx.Dispose();
        }

        #endregion

        #region Actions

        [AcceptVerbs("GET")]
        [ActionName("get")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var customer = _ctx.Customers.FirstOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [AcceptVerbs("POST", "PUT")]
        [ActionName("add")]
        [ResponseType(typeof(CustomerResult))]
        public IHttpActionResult AddCustomer([FromBody] CustomerDetails customerDetails)
        {
            if (string.IsNullOrWhiteSpace(customerDetails?.name) || string.IsNullOrWhiteSpace(customerDetails.email))
            {
                throw new ArgumentException("AddCustomer_InvalidArguments");
            }

            var customer = _ctx.Customers.FirstOrDefault(x => x.Email == customerDetails.email);

            if (customer != null)
            {
                return UpdateCustomer(customerDetails);
            }

            customer = new Customer
            {
                Name = customerDetails.name,
                Email = customerDetails.email,
                IsActive = true,
                State = CustomerState.Active,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();

            return Ok(new CustomerResult{Id = customer.Id, Data = customer});
        }

        [AcceptVerbs("POST", "PUT")]
        [ActionName("update")]
        [ResponseType(typeof(CustomerResult))]
        public IHttpActionResult UpdateCustomer([FromBody] CustomerDetails customerDetails)
        {
            if (string.IsNullOrWhiteSpace(customerDetails?.name) || string.IsNullOrWhiteSpace(customerDetails.email))
            {
                throw new ArgumentException("UpdateCustomer_InvalidArguments");
            }

            var customer = _ctx.Customers.FirstOrDefault(x => x.Email == customerDetails.email);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = customerDetails.name;
            customer.Email = customerDetails.email;
            customer.DateUpdated = DateTime.UtcNow;

            _ctx.SaveChanges();

            return Ok(new CustomerResult { Id = customer.Id, Data = customer });
        }

        [AcceptVerbs("DELETE")]
        [ActionName("delete")]
        [ResponseType(typeof(bool))]
        public bool DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("DeleteCustomer_InvalidArguments");
            }

            var customer = _ctx.Customers.FirstOrDefault(x => x.Id == id);

            if (customer == null)
            {
                throw new ArgumentException("DeleteCustomer_CustomerNotFound");
            }

            _ctx.Customers.Remove(customer);
            _ctx.SaveChanges();

            return true;
        }

        #endregion
    }
}