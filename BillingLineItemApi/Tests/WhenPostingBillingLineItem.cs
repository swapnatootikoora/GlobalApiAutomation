using System;
using System.Collections.Generic;
using System.Net;
using BillingLineItemApi.SetUp;
using BillingLineProductApi.SetUp;
using Common.Models;
using Common.TestData;
using Common.Tests;
using NUnit.Framework;
using OrderApi.TestSetup;

namespace BillingLineItemApi.Tests
{
    [TestFixture]
    public class WhenPostingBillingLineItem : TestSetup<BillingLineItem>
    {
        private string newOrderId;
        private string newBuyingAreaId;
        private string newDisabledBuyingAreaId = new BillingLineBuyingAreaCreation().GetDisabledBuyingAreaId();
        private string newProductId;
        private string newDisabledProductId = new BillingLineProductCreation().GetDisabledProductId();

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
        }

        [Test]
        public void Should_create_billing_line_item_successfully()
        {
            // arrange
            var request = GetValidRequest();

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_blank_order_id()
        {
            // arrange
            var request = GetValidRequest();
            request.OrderId = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_blank_planner_id()
        {
            // arrange
            var request = GetValidRequest();
            request.PlannerId = null;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_blank_product_id()
        {
            // arrange
            var request = GetValidRequest();
            request.BillingLineProductId = null;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_disabled_product_id()
        {
            // arrange
            var request = GetValidRequest();
            request.BillingLineProductId = newDisabledProductId;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_start_month_in_past()
        {
            // arrange
            var request = GetValidRequest();
            request.StartMonth = DateTimeOffset.Now.AddMonths(-1);

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_duration_in_months_is_zero()
        {
            // arrange
            var request = GetValidRequest();
            request.DurationInMonths = 0;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_duration_in_months_is_more_than_13_months()
        {
            // arrange
            var request = GetValidRequest();
            request.DurationInMonths = 14;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_null_revenue()
        {
            // arrange
            var request = GetValidRequest();
            request.BillingLineBuyingAreaRevenues[0].MonthBuyingAreaRevenues[0].Value = 0;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_disabled_buyingarea_id()
        {
            // arrange
            var request = GetValidRequest();
            request.BillingLineBuyingAreaRevenues[0].Id = newDisabledBuyingAreaId;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_billing_line_item_with_owner_capability()
        {
            // arrange
            var request = GetValidRequest();
            request.PlannerId = DataClass.OwnerOnly[0].Id;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }
    }
}
