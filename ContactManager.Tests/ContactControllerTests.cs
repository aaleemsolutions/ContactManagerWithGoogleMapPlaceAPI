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

        [Fact]
        public async Task Create_ValidContact_ReturnsSuccess()
        {
            // Arrange
            var mockService = new Mock<IContactService>();
            var mockLogger = new Mock<ILogger<ContactController>>();
            var contactDto = new ContactDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                Address = "123 Test St"
            };

            mockService.Setup(service => service.AddOrUpdateService(contactDto))
                       .ReturnsAsync(contactDto);

            var controller = new ContactController(mockService.Object);
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
            var mockService = new Mock<IContactService>();
            var mockLogger = new Mock<ILogger<ContactController>>();
            var contactDto = new ContactDTO
            {
                FirstName = "", 
                LastName = "Doe",
                Email = "john.doe@example.com",
                Address = "123 Test St"
            };

            var controller = new ContactController(mockService.Object);
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
            var mockService = new Mock<IContactService>();
            var mockLogger = new Mock<ILogger<ContactController>>();
            var contactDto = new ContactDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Address = "123 Test St"
            };

            mockService.Setup(service => service.AddOrUpdateService(contactDto))
                       .ThrowsAsync(new System.Exception("Service error"));

            var controller = new ContactController(mockService.Object);
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