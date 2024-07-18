using Moq;
using Xunit;
using BFFCopilotApi.Controllers;
using BFFCopilotApi.Models; // Ensure this is where your User model is located
using BFFCopilotApi.Services; // Ensure this is where IProfileManagement is located
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ShoppingCartControllerTests
{
    private readonly Mock<IProfileManagement> _mockProfileManagement;
    private readonly ShoppingCartController _controller;
    private readonly Mock<ICartService> _mockCartService;

    public ShoppingCartControllerTests()
    {
        _mockProfileManagement = new Mock<IProfileManagement>();
        _mockCartService = new Mock<ICartService>();
        _controller = new ShoppingCartController(_mockProfileManagement.Object, _mockCartService.Object);
    }

    [Fact]
    public async Task CreateProfile_ReturnsOk_WithValidUser()
    {
        // Arrange
        var user = new User { /* Initialize user properties */ };
        _mockProfileManagement.Setup(service => service.CreateProfile(user))
                              .ReturnsAsync(1); // Assuming 1 is the ID of the created user

        // Act
        var result = await _controller.CreateProfile(user);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        // Additional assertions as necessary
    }

    [Fact]
    public async Task UpdateProfile_ReturnsOk_WhenUpdateIsSuccessful()
    {
        // Arrange
        var user = new User { /* Initialize user properties */ };
        _mockProfileManagement.Setup(service => service.UpdateUserId(It.IsAny<int>(), user))
                              .ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateProfile(1, user); // Assuming 1 is the user ID

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProfile_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
      
        _mockProfileManagement.Setup(service => service.GetUserById(It.IsAny<int>()))
                      .ReturnsAsync((User)null); // Simulates user not found
        // Act
        var result = await _controller.GetProfile(It.IsAny<int>());

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Profile not found.", notFoundResult.Value); // Assuming this is the message returned by your controller
    }

    [Fact]
    public async Task AddCartItem_ReturnsOkResult()
    {
        // Arrange
        var product = new Product { ProductId = 1 };
        var userId = 1;
        _mockCartService.Setup(service => service.AddCartItem(It.IsAny<Product>(), It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AddCartItem(product, userId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCartItem_ReturnsOkResult()
    {
        // Arrange
        var product = new Product { ProductId = 1 };
        var userId = 1;
        _mockCartService.Setup(service => service.UpdateCartItem(It.IsAny<Product>(), It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateCartItem(product, userId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Checkout_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        var cartItems = new List<CartItem> { new CartItem { CartItemId = 1 } };
        var userId = 1;
        _mockCartService.Setup(service => service.Checkout(It.IsAny<ICollection<CartItem>>(), userId)).ReturnsAsync(true);

        // Act
        var result = await _controller.Checkout(cartItems, userId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Checkout_ReturnsBadRequest_WhenFailed()
    {
        // Arrange
        var cartItems = new List<CartItem> { new CartItem { CartItemId = 1 } };
        var userId = 1;
        _mockCartService.Setup(service => service.Checkout(It.IsAny<ICollection<CartItem>>(), userId)).ReturnsAsync(false);

        // Act
        var result = await _controller.Checkout(cartItems, userId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
