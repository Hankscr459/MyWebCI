using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWebCI.Api.Controllers;
using MyWebCI.Api.Dto.Response;
using System.Net.Http.Json;

namespace MyWebCI.Tests.Controllers
{
    public class ItemControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly ItemController _controller;
        protected IConfiguration Configuration { get; }
        private readonly HttpClient _client;
        public ItemControllerTests(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            var projectDir = Directory.GetCurrentDirectory();
            // 初始化控制器
            Configuration = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.Testing.json", optional: false, reloadOnChange: true)
                .Build();
            _controller = new ItemController(Configuration);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithDefaultItem()
        {
            // Act (執行)
            //var result = _controller.Get();

            //// Assert (驗證)
            //var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            //var item = okResult.Value.Should().BeOfType<ItemResponseDto>().Subject;
            var response =  _client.GetFromJsonAsync<ItemResponseDto>("/item").Result;

            response.Id.Should().Be(3);
            response.Name.Should().Be("Item01");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void GetById_WithValidId_ReturnsOkResult(int testId)
        {
            // Act
            var result = _controller.GetById(testId);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var item = okResult.Value.Should().BeOfType<ItemResponseDto>().Subject;

            item.Id.Should().Be(testId);
            item.Name.Should().Be($"Item{testId}");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GetById_WithInvalidId_ReturnsBadRequest(int invalidId)
        {
            // Act
            var result = _controller.GetById(invalidId);

            // Assert
            var badRequestResult = result.Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("Invalid ID");
        }
    }
}
