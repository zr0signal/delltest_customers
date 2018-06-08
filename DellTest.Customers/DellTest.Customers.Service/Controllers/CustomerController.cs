using System;
using System.Linq;
using System.Web.Http;
using DellTest.Customers.Service.Models;

namespace DellTest.Customers.Service.Controllers
{
    public class CustomerController : ApiController
    {
        [AcceptVerbs("GET")]
        [ActionName("get")]
        public IHttpActionResult GetCustomer(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            using (var ctx = new CustomerServiceContext())
            {
                var customer = ctx.Customers.FirstOrDefault(x => x.Id == id);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
        }

        [AcceptVerbs("POST", "PUT")]
        [ActionName("add")]
        public IHttpActionResult AddCustomer([FromBody] CustomerUpdate customerUpdate)
        {
            if (string.IsNullOrWhiteSpace(customerUpdate?.name) || string.IsNullOrWhiteSpace(customerUpdate.email))
            {
                throw new ArgumentException("AddCustomer_InvalidArguments");
            }

            using (var ctx = new CustomerServiceContext())
            {
                var customer = ctx.Customers.FirstOrDefault(x => x.Email == customerUpdate.email);

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

                ctx.Customers.Add(customer);
                ctx.SaveChanges();

                return Ok(customer);
            }
        }

        [AcceptVerbs("POST", "PUT")]
        [ActionName("update")]
        public IHttpActionResult UpdateCustomer([FromBody] CustomerUpdate customerUpdate)
        {
            if (string.IsNullOrWhiteSpace(customerUpdate?.name) || string.IsNullOrWhiteSpace(customerUpdate.email))
            {
                throw new ArgumentException("UpdateCustomer_InvalidArguments");
            }

            using (var ctx = new CustomerServiceContext())
            {
                var customer = ctx.Customers.FirstOrDefault(x => x.Email == customerUpdate.email);

                if (customer == null)
                {
                    return NotFound();
                }

                customer.Name = customerUpdate.name;
                customer.Email = customerUpdate.email;

                ctx.SaveChanges();

                return Ok(customer);
            }
        }

        [AcceptVerbs("DELETE")]
        [ActionName("delete")]
        public bool DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("DeleteCustomer_InvalidArguments");
            }

            using (var ctx = new CustomerServiceContext())
            {
                var customer = ctx.Customers.FirstOrDefault(x => x.Id == id);

                if (customer == null)
                {
                    throw new ArgumentException("DeleteCustomer_CustomerNotFound");
                }

                ctx.Customers.Remove(customer);
                ctx.SaveChanges();

                return true;
            }
        }
    }
}