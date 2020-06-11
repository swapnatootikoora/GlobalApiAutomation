using System;
using System.Collections.Generic;
using System.Net;
using BillingLineItemApi.SetUp;
using BillingLineProductApi.SetUp;
using Common.Helpers;
using Common.Models;
using Common.TestData;
using Common.Tests;
using NUnit.Framework;
using OrderApi.TestSetup;
using static Common.Models.BillingLineItem;

namespace BillingLineItemApi.Tests
{
    [TestFixture]
    public class WhenGettingBillingLineItem : TestSetup<BillingLineItem>
    {
        BillingLineItem Request;
        private string newOrderId;
        private static string newBuyingAreaId ;
        private string newProductId ;

        public BillingLineItem GetValidRequest()
        {
            var now = DateTime.Now;
            var startMonth = new DateTimeOffset(now.Year, now.Month, 1, 0, 0, 0, new TimeSpan(0, 0, 0));
            //var startMonth = new DateTimeOffset(now).ToOffset(TimeSpan.Zero);
            return new BillingLineItem
            {
                OrderId = newOrderId,
                PlannerId = DataClass.PlannerOnly[0].Id,
                BillingLineProductId = newProductId,
                StartMonth = startMonth,
                DurationInMonths = 2,
                BillingLineBuyingAreaRevenues = new List<BillingLineBuyingAreaRevenue>()
                {
                    new BillingLineBuyingAreaRevenue()
                    {
                        Id = newBuyingAreaId,
                        MonthBuyingAreaRevenues = new List<MonthBuyingAreaRevenue>()
                        {
                            new MonthBuyingAreaRevenue()
                            {
                                Date = startMonth,
                                Value = 50.60M
                            },
                            new MonthBuyingAreaRevenue()
                            {
                                Date = startMonth.AddMonths(1),
                                Value = 89.99M
                            }
                        }
                    }
                }
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            newOrderId = new OrderCreation().GetOrderID();
            newBuyingAreaId = new BillingLineBuyingAreaCreation().GetBuyingAreaId();
            newProductId = new BillingLineProductCreation().GetProductId();
            httpService.EndPoint = AutomationVariables.BillingLineItemAPI;
            Request = GetValidRequest();
            var postResponse = httpService.PerformPost(Request);
            var response = JSONLibrary.DeserializeJSon<BillingLineItem>(postResponse.Content);
            Request.Id = response.Id;
        }

        [Test]
        public void Should_return_all_billing_line_items_successfully()
        {
            //act
            var apiResponse = httpService.PerformGet();

            //assert
            ResponseShouldContainInstances(apiResponse, Request.Id);
        }

        [Test]
        public void Should_return_specific_billing_line_item_successfully()
        {
            //act
            var apiResponse = httpService.PerformGet(Request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, Request);
        }

        [Test]
        public void Should_return_not_found_when_invalid_billing_line_item_provided()
        {
            // arrange
            var invalidBillingLineItemId = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformGet(invalidBillingLineItemId);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}