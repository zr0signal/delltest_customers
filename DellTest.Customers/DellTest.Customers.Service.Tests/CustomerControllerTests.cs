using System.Web.Http.Results;
using DellTest.Customers.Service.Controllers;
using DellTest.Customers.Service.Models;
using DellTest.Customers.Service.Tests.Mock.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DellTest.Customers.Service.Tests
{
    [TestClass]
    public class CustomerControllerTests
    {
        #region Setup

        private TestCustomerServiceContext _context;
        private CustomerController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _context = new TestCustomerServiceContext();
            _controller = new CustomerController(_context);
        }

        [TestCleanup]
        public void TeadDown()
        {
            _controller = null;
        }

        #endregion

        #region /GetCustomer

        [TestMethod]
        public void GetCustomer_WithInvalidId_ShouldReturnNotFound()
        {
            var result = _controller.GetCustomer(-1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCustomer_WithNoCustomer_ShouldReturnNotFound()
        {
            var result = _controller.GetCustomer(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCustomer_WithCustomer_ShouldReturnCustomer()
        {
            _context.Customers.Add(new Customer { Id = 1, Name = "Name", Email = "Email" });

            var result = _controller.GetCustomer(1) as OkNegotiatedContentResult<Customer>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Id == 1);
            Assert.IsTrue(result.Content.Name == "Name");
            Assert.IsTrue(result.Content.Email == "Email");
        }

        #endregion
    }
}
