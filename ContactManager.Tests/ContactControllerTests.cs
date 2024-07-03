using ContactManager.Common.Helper;
using ContactManager.Common.Model;
using ContactManager.Controllers;
using ContactManager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ContactManager.Common.Helper;

namespace ContactManager.Tests
{
    public class ContactControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IContactService> mockService;
        private readonly ContactController controller;
        public ContactControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            mockService = new Mock<IContactService>();
            controller = new ContactController(mockService.Object, _mockConfiguration.Object);

        }

        [Fact]
        public async Task Create_ValidContact_ReturnsSuccess()
        {
            // Arrange
            //var mockService = new Mock<IContactService>();

            var contactDto = new ContactDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                Address = "123 Test St"
            };

            mockService.Setup(service => service.AddOrUpdateService(contactDto))
                       .ReturnsAsync(contactDto);

            controller.ModelState.Clear(); // Ensure model state is valid

            var result = await controller.Create(contactDto) as JsonResult;

            Assert.NotNull(result);



            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(result.Value.ToJsonString());
            bool IsSuccess = Convert.ToBoolean(jsonObject.success);

            Assert.True(IsSuccess);
        }

        [Fact]
        public async Task Create_InvalidContact_ReturnsValidationError()
        {
            // Arrange
            var contactDto = new ContactDTO
            {
                FirstName = "",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Address = "123 Test St"
            };

            controller.ModelState.AddModelError("FirstName", "First name is required");

            // Act
            var result = await controller.Create(contactDto) as JsonResult;

            // Assert
            Assert.NotNull(result);

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(result.Value.ToJsonString());
            bool IsSuccess = Convert.ToBoolean(jsonObject.success);
            Assert.False(IsSuccess);
            Assert.Equal("Validation failed", jsonObject.message);
        }

        [Fact]
        public async Task Create_ServiceThrowsException_ReturnsError()
        {
            // Arrange
            var contactDto = new ContactDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Address = "123 Test St"
            };

            mockService.Setup(service => service.AddOrUpdateService(contactDto))
                       .ThrowsAsync(new System.Exception("Service error"));

            controller.ModelState.Clear();

            // Act
            var result = await controller.Create(contactDto) as JsonResult;

            // Assert
            Assert.NotNull(result);

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(result.Value.ToJsonString());
            bool IsSuccess = Convert.ToBoolean(jsonObject.success);
            Assert.False(IsSuccess);
            Assert.Equal("Service error", jsonObject.message);
        }
    }
}