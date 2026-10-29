using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using MicroProjectApplication.Controllers;
using MicroProjectApplication.Models;
using MicroProjectApplication.Repositories;
using MicroProjectApplication.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroProjectApplication.Tests
{
    // US-001 / SVC-002: customer endpoints under /api/v1/customers.
    [TestClass]
    public class CustomersControllerTests
    {
        private CustomersController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new CustomersController(new CustomerService(new CustomerRepository()));
        }

        [TestMethod]
        public void RoutePrefix_IsApiV1Customers()
        {
            var attr = (RoutePrefixAttribute)System.Attribute.GetCustomAttribute(
                typeof(CustomersController), typeof(RoutePrefixAttribute));
            Assert.AreEqual("api/v1/customers", attr.Prefix);
        }

        [TestMethod]
        public void GetById_Unknown_ReturnsStandardizedNotFound()
        {
            var result = _controller.GetById(int.MaxValue) as NegotiatedContentResult<ErrorResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("customer_not_found", result.Content.Code);
        }

        [TestMethod]
        public void Update_Patch_Delete_Unknown_ReturnStandardizedNotFound()
        {
            var body = new Customer { Name = "x", City = "y" };

            Assert.AreEqual(HttpStatusCode.NotFound, ((NegotiatedContentResult<ErrorResponse>)_controller.Update(int.MaxValue, body)).StatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, ((NegotiatedContentResult<ErrorResponse>)_controller.Patch(int.MaxValue, body)).StatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, ((NegotiatedContentResult<ErrorResponse>)_controller.Delete(int.MaxValue)).StatusCode);
        }

        [TestMethod]
        public void Create_Patch_Delete_Lifecycle_SetsTimestampsAndAppliesPartialUpdate()
        {
            var created = ((OkNegotiatedContentResult<Customer>)_controller.Create(new Customer { Name = "Ann", City = "Pune" })).Content;
            Assert.AreNotEqual(default(System.DateTime), created.CreatedAt);

            var patched = ((OkNegotiatedContentResult<Customer>)_controller.Patch(created.Id, new Customer { City = "Goa" })).Content;
            Assert.AreEqual("Ann", patched.Name, "PATCH must leave omitted fields untouched");
            Assert.AreEqual("Goa", patched.City);
            Assert.IsTrue(patched.UpdatedAt >= patched.CreatedAt);

            Assert.IsInstanceOfType(_controller.Delete(created.Id), typeof(OkNegotiatedContentResult<string>));
            Assert.IsInstanceOfType(_controller.GetById(created.Id), typeof(NegotiatedContentResult<ErrorResponse>));
        }
    }
}
