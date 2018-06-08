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
        [ResponseType(typeof(Customer))]
        public IHttpActionResult AddCustomer([FromBody] CustomerUpdate customerUpdate)
        {
            if (string.IsNullOrWhiteSpace(customerUpdate?.name) || string.IsNullOrWhiteSpace(customerUpdate.email))
            {
                throw new ArgumentException("AddCustomer_InvalidArguments");
            }

            var customer = _ctx.Customers.FirstOrDefault(x => x.Email == customerUpdate.email);

            if (customer != null)
            {
                return UpdateCustomer(customerUpdate);
            }

            customer = new Customer
            {
                Name = customerUpdate.name,
                Email = customerUpdate.email,
                IsActive = true,
                State = CustomerState.Active
            };

            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();

            return Ok(customer);
        }

        [AcceptVerbs("POST", "PUT")]
        [ActionName("update")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult UpdateCustomer([FromBody] CustomerUpdate customerUpdate)
        {
            if (string.IsNullOrWhiteSpace(customerUpdate?.name) || string.IsNullOrWhiteSpace(customerUpdate.email))
            {
                throw new ArgumentException("UpdateCustomer_InvalidArguments");
            }

            var customer = _ctx.Customers.FirstOrDefault(x => x.Email == customerUpdate.email);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = customerUpdate.name;
            customer.Email = customerUpdate.email;

            _ctx.SaveChanges();

            return Ok(customer);
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