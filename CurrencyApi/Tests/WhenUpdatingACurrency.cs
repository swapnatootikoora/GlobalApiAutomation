using Common.Helpers;
using Common.Models;
using Common.TestData;
using Common.Tests;
using NUnit.Framework;

namespace CurrencyApi.Properties.Tests
{
    [TestFixture]
    public class WhenUpdatingACurrency : TestSetup<Currency>
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.CurrencyAPI;
        }

        [Test]
        public void Should_update_currency_status_successfully()
        {
            // arrange
            var getResponse = httpService.PerformGet(DataClass.AUSCurrency.Id);
            var request = JSONLibrary.DeserializeJSon<Currency>(getResponse.Content);
            request.IsDisabled = (!request.IsDisabled) ? true : false;

            //act
            var apiResponse = httpService.PerformPatch(request, request.Id + "/isdisabled");

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }
    }
}



