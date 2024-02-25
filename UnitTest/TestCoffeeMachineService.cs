using System;
using Microsoft.Extensions.Logging;
using Moq;
using Service;
using BusinessEntities;

namespace UnitTest
{
    /// <summary>
    /// Unit Test for CoffeeMachineService.
    /// </summary>
    [TestFixture]
	public class TestCoffeeMachineService
	{
		Mock<ICoffeeMachineService> _mockCoffMachineService;

        [SetUp]
		public void SetUp()
		{
			_mockCoffMachineService = new Mock<ICoffeeMachineService>();
		}

        /// <summary>
        /// Test to verify service is returning brew coffee expected response.
        /// </summary>
		[Test]
		public void TestCoffeeMachineService_GetBrewCoffee_Return_Successfully()
		{
			DateTime today = DateTime.Now;

			BrewCoffeeResponse brewCoffee = new BrewCoffeeResponse();
            brewCoffee.Prepared = today.ToString(Constants.ISO8601DateFormat);
            brewCoffee.Message = "Your piping hot coffee is ready";

			_mockCoffMachineService.Setup(x => x.GetBrewCoffee()).Returns(brewCoffee);
			BrewCoffeeResponse response= _mockCoffMachineService.Object.GetBrewCoffee();

			Assert.That(response.Message, Is.EqualTo(brewCoffee.Message));
            Assert.That(response.Prepared, Is.EqualTo(brewCoffee.Prepared));
        }

        /// <summary>
        /// Negative test to cover exception using moq.
        /// </summary>
        [Test]
        public void TestCoffeeMachineService_GetBrewCoffee_ThrowsException()
        {
            _mockCoffMachineService.Setup(x => x.GetBrewCoffee()).Throws<Exception>();
            Assert.Throws<Exception>(()=> _mockCoffMachineService.Object.GetBrewCoffee());
        }
    }
}

