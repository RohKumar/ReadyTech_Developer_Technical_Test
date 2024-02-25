using System;
using Microsoft.Extensions.Logging;
using Moq;
using Service;
using BusinessEntities;
using Service.WebApi;

namespace UnitTest
{
    /// <summary>
    /// Unit Test for CoffeeMachineService.
    /// </summary>
    [TestFixture]
	public class TestCoffeeMachineService
	{
		Mock<ICoffeeMachineService> _mockCoffMachineService;
        private Mock<IWebApiRequest> _mockWebApiRequest;
        private Mock<ILogger<CoffeeMachineService>> _mockLogger;

        [SetUp]
		public void SetUp()
		{
            _mockWebApiRequest = new Mock<IWebApiRequest>();
            _mockLogger = new Mock<ILogger<CoffeeMachineService>>();
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
			BrewCoffeeResponse response= (BrewCoffeeResponse)_mockCoffMachineService.Object.GetBrewCoffee();

			Assert.That(response.Message, Is.EqualTo(brewCoffee.Message));
            Assert.That(response.Prepared, Is.EqualTo(brewCoffee.Prepared));
        }

        /// <summary>
        /// Negative test to cover exception
        /// </summary>
        [Test]
        public void TestCoffeeMachineService_GetBrewCoffee_ThrowsException()
        {
            _mockCoffMachineService.Setup(x => x.GetBrewCoffee()).Throws<Exception>();
            Assert.Throws<Exception>(()=> _mockCoffMachineService.Object.GetBrewCoffee());
        }

        /// <summary>
        /// Test to get tempreture from third party web api.
        /// </summary>
        [Test]
        public void TestCoffeeMachineService_GetCurrentTempreture_Return_Successfully()
        {
            double currentTempreture = 18.76;
            _mockWebApiRequest.Setup(x => x.GetCurrentWeather(Constants.MelbourneLatitude,Constants.MelbourneLongitude)).Returns(GetWeatherData());

            CoffeeMachineService coffeeMachine = new CoffeeMachineService(_mockLogger.Object, _mockWebApiRequest.Object);
            double tempreture = coffeeMachine.GetCurrentTempreture(Constants.MelbourneLatitude, Constants.MelbourneLongitude);

            Assert.That(tempreture, Is.EqualTo(currentTempreture));
        }

        /// <summary>
        /// Test to verify service is returning brew coffee when tempreture is above threshold(30).
        /// </summary>
        [Test]
        public void TestCoffeeMachineService_GetBrewCoffee_TempretureAboveThreshold_Return_Successfully()
        {
            _mockWebApiRequest.Setup(x => x.GetCurrentWeather(Constants.MelbourneLatitude, Constants.MelbourneLongitude)).Returns(GetWeatherDataAboveThresholdTempreture());

            CoffeeMachineService coffeeMachine = new CoffeeMachineService(_mockLogger.Object, _mockWebApiRequest.Object);
            string response = (string)coffeeMachine.GetBrewCoffee();

            Assert.That(response, Is.EqualTo("Your refreshing iced coffee is ready"));
        }

        public string GetWeatherData()
        {
            string weatherData = File.ReadAllText(@"../../../WeatherData/Weather.txt");
            return weatherData;
        }

        public string GetWeatherDataAboveThresholdTempreture()
        {
            string weatherData = File.ReadAllText(@"../../../WeatherData/WetherTempretureAboveThreshold.txt");
            return weatherData;
        }
    }
}

