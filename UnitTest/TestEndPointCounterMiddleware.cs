using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace UnitTest
{
	[TestFixture]
	public class TestEndPointCounterMiddleware
	{
        /// <summary>
        /// Unit Test to verify middleware executed successfully
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestEndPointCounterMiddleware_Executed_Successfully()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/brew-coffee";

            var wasExecuted = false;
            RequestDelegate requestDelegate = (HttpContext context) =>
            {
                wasExecuted = true;
                return Task.CompletedTask;
            };

            var endPointMiddleware = new EndpointCounterMiddleware(requestDelegate);
            await endPointMiddleware.InvokeAsync(httpContext);

            Assert.True(wasExecuted);
        }

        /// <summary>
        /// Unit test to check api endpoint is returning status 503 for every 5th request.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestEndPointCounterMiddleware_CheckEndpointRequestCounter_Successfully()
        {

            MemoryStream bodyStream = new MemoryStream();
            var httpContext = new DefaultHttpContext();

            httpContext.Response.Body = bodyStream;
            httpContext.Request.Path = "/brew-coffee";

            var wasExecuted = false;
            RequestDelegate requestDelegate = (HttpContext context) =>
            {
                wasExecuted = true;
                return Task.CompletedTask;
            };

            var endPointMiddleware = new EndpointCounterMiddleware(requestDelegate);

            // executing the api request 5 times.
            await endPointMiddleware.InvokeAsync(httpContext);
            await endPointMiddleware.InvokeAsync(httpContext);
            await endPointMiddleware.InvokeAsync(httpContext);
            await endPointMiddleware.InvokeAsync(httpContext);
            await endPointMiddleware.InvokeAsync(httpContext);

            string response;
            bodyStream.Seek(0, SeekOrigin.Begin);

            using (var stringReader = new StreamReader(bodyStream)) {
                response = await stringReader.ReadToEndAsync();
            }

            Assert.That(response, Is.EqualTo("503 Service Unavailable"));
            Assert.That(httpContext.Response.StatusCode, Is.EqualTo(StatusCodes.Status503ServiceUnavailable));
            Assert.True(wasExecuted);
        }
    }
}

