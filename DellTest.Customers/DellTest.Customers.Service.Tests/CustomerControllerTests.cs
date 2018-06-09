using System;
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
            _context = null;
            _controller = null;
        }

        #endregion

        #region /GetAll

        // TODO

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
            _context.Customers.Add(new Customer {Id = 1, Name = "Name", Email = "Email"});

            var result = _controller.GetCustomer(1) as OkNegotiatedContentResult<Customer>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

            Assert.IsTrue(result.Content.Id == 1);
            Assert.IsTrue(result.Content.Name == "Name");
            Assert.IsTrue(result.Content.Email == "Email");
        }

        #endregion

        #region /AddCustomer

        [TestMethod]
        public void AddCustomer_WithInvalidCustomerDetails_ShouldThrowException()
        {
            Assert.ThrowsException<ArgumentException>(() => _controller.AddCustomer(null),
                "AddCustomer_InvalidArguments");
            Assert.ThrowsException<ArgumentException>(() => _controller.AddCustomer(new CustomerDetails()),
                "AddCustomer_InvalidArguments");
            Assert.ThrowsException<ArgumentException>(() => _controller.AddCustomer(new CustomerDetails {email = ""}),
                "AddCustomer_InvalidArguments");
            Assert.ThrowsException<ArgumentException>(() => _controller.AddCustomer(new CustomerDetails { email = "invalidemail" }),
                "AddCustomer_InvalidEmail");
            Assert.ThrowsException<ArgumentException>(() => _controller.AddCustomer(new CustomerDetails { email = "invalidemail@nothing" }),
                "AddCustomer_InvalidEmail");
        }

        [TestMethod]
        public void AddCustomer_WithNewCustomerDetails_ShouldReturnCustomer()
        {
            _context.Customers.Add(new Customer
            {
                Id = 1,
                Name = "Existing Customer",
                Email = "existing.customer@mail.org"
            });

            var customerDetails = new CustomerDetails
            {
                name = "New Customer",
                email = "new.customer@mail.org"
            };

            var result = _controller.AddCustomer(customerDetails) as OkNegotiatedContentResult<CustomerResult>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsNotNull(result.Content.Data);

            Assert.IsTrue(result.Content.Data.Name == customerDetails.name);
            Assert.IsTrue(result.Content.Data.Email == customerDetails.email);
            Assert.IsTrue(result.Content.Data.IsActive);
            Assert.IsTrue(result.Content.Data.State == CustomerState.Active);

            // TODO: Moq Verify for Add and SaveChanges
        }

        [TestMethod]
        public void AddCustomer_WithExistingCustomerDetails_ShouldReturnCustomer()
        {
            _context.Customers.Add(new Customer
            {
                Id = 1,
                Name = "Existing Customer",
                Email = "existing.customer@mail.org"
            });

            var customerDetails = new CustomerDetails
            {
                name = "New Customer",
                email = "existing.customer@mail.org"
            };

            var result = _controller.AddCustomer(customerDetails) as OkNegotiatedContentResult<CustomerResult>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsNotNull(result.Content.Data);

            Assert.IsTrue(result.Content.Data.Name == customerDetails.name);
            Assert.IsTrue(result.Content.Data.Email == customerDetails.email);

            // TODO: Moq Verify for Add and SaveChanges
        }

        #endregion

        #region /UpdateCustomer

        [TestMethod]
        public void UpdateCustomer_WithInvalidCustomerDetails_ShouldThrowException()
        {
            Assert.ThrowsException<ArgumentException>(() => _controller.UpdateCustomer(null),
                "UpdateCustomer_InvalidArguments");
            Assert.ThrowsException<ArgumentException>(() => _controller.UpdateCustomer(new CustomerDetails()),
                "UpdateCustomer_InvalidArguments");
            Assert.ThrowsException<ArgumentException>(
                () => _controller.UpdateCustomer(new CustomerDetails {email = ""}), "UpdateCustomer_InvalidArguments");
            Assert.ThrowsException<ArgumentException>(() => _controller.AddCustomer(new CustomerDetails { email = "invalidemail" }),
                "UpdateCustomer_InvalidEmail");
            Assert.ThrowsException<ArgumentException>(() => _controller.AddCustomer(new CustomerDetails { email = "invalidemail@nothing" }),
                "UpdateCustomer_InvalidEmail");
        }

        [TestMethod]
        public void UpdateCustomer_WithInvalidEmail_ShouldReturnNotFound()
        {
            _context.Customers.Add(new Customer
            {
                Id = 1,
                Name = "Existing Customer",
                Email = "existing.customer@mail.org"
            });

            var customerDetails = new CustomerDetails
            {
                name = "Existing Customer",
                email = "existing.customer2@mail.org"
            };

            var result = _controller.UpdateCustomer(customerDetails);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void AddCustomer_WithNewCustomerUpdate_ShouldReturnCustomer()
        {
            _context.Customers.Add(new Customer
            {
                Id = 1,
                Name = "Existing Customer",
                Email = "existing.customer@mail.org"
            });

            var customerDetails = new CustomerDetails
            {
                name = "Existing Customer",
                email = "existing.customer@mail.org"
            };

            var result = _controller.UpdateCustomer(customerDetails) as OkNegotiatedContentResult<CustomerResult>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsNotNull(result.Content.Data);

            Assert.IsTrue(result.Content.Data.Name == customerDetails.name);
            Assert.IsTrue(result.Content.Data.Email == customerDetails.email);

            // TODO: Moq Verify for SaveChanges
        }

        #endregion

        #region /DeleteCustomer

        // TODO

        #endregion
    }
}