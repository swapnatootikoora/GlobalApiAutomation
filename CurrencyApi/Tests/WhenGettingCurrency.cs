using System;
using System.Net;
using Common.Helpers;
using Common.Models;
using Common.TestData;
using Common.Tests;
using NUnit.Framework;

namespace CurrencyApi.Properties.Tests
{

    [TestFixture]
    public class WhenGettingCurrency : TestSetup<Currency>
    {
        Currency Request;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.CurrencyAPI;
            
        }

        [Test]
        public void Should_return_allcurrencies_successfully()
        {
            //act
            var apiResponse = httpService.PerformGet();

            //assert
            ResponseShouldContainInstances(apiResponse, DataClass.USCurrency.Id);
        }

        [Test]
        public void Should_return_specific_currency_successfully()
        {
            //arrange
            Request = DataClass.USCurrency;

            //act
            var apiResponse = httpService.PerformGet(Request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, Request);

        }

        [Test]
        public void Should_return_bad_request_when_getting_currency_with_invalid_Id()
        {
            //assert
            var invalidCurrencyId = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformGet(invalidCurrencyId);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}
