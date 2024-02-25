using Microsoft.AspNetCore.Mvc;
using Moq;
using ReadyTechDeveloperTechnicalTest.Controllers;
using Service;
using Microsoft.AspNetCore.Http;
using BusinessEntities;

namespace UnitTest;

/// <summary>
/// Unit test cases for Coffee Machine Controller
/// </summary>
public class TestCoffeeMachineController
{
    private Mock<ICoffeeMachineService> _mockCoffeeMachineService;

    [SetUp]
    public void Setup()
    {
        _mockCoffeeMachineService = new Mock<ICoffeeMachineService>();
    }

    /// <summary>
    /// Test for 200 ok response.
    /// </summary>
    [Test]
    public void TestCoffeeMachineController_GetBrewCoffee_ReturnOK_Successfully()
    {
        _mockCoffeeMachineService.Setup(x => x.GetBrewCoffee()).Returns(new BrewCoffeeResponse());

        CoffeeMachineController coffeeMachineController = new CoffeeMachineController(_mockCoffeeMachineService.Object);

        OkObjectResult result = (OkObjectResult)coffeeMachineController.GetBrewCoffee(DateTime.Now);

        Assert.IsNotNull(result);
        Assert.DoesNotThrow(() => coffeeMachineController.GetBrewCoffee(DateTime.Now));
        Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
    }

    /// <summary>
    /// Test to verify for 1st April and if found return 418  I' am Teapot response.
    /// </summary>
    [Test]
    public void TestCoffeeMachineController_GetBrewCoffee_CheckIsFirstApril_Return_Successfully()
    {
        CoffeeMachineController coffeeMachineController = new CoffeeMachineController(_mockCoffeeMachineService.Object);

        ObjectResult result = (ObjectResult)coffeeMachineController.GetBrewCoffee(new DateTime(2024,04,01));

        Assert.IsNotNull(result);
        Assert.DoesNotThrow(() => coffeeMachineController.GetBrewCoffee(new DateTime(2024, 04, 01)));
        Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status418ImATeapot));
    }

    /// <summary>
    /// Negative test if throws exception.
    /// </summary>
    [Test]
    public void TestCoffeeMachineController_GetBrewCoffee_ThrowException_Successfully()
    {
        _mockCoffeeMachineService.Setup(x => x.GetBrewCoffee()).Throws<Exception>();

        CoffeeMachineController coffeeMachineController = new CoffeeMachineController(_mockCoffeeMachineService.Object);

        Assert.Throws<Exception>(() => coffeeMachineController.GetBrewCoffee(DateTime.Now));
    }
}
