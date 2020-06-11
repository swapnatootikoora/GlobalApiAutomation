using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api;
using Common.ConfigUtils;
using Common.Helpers;
using Common.Models;

namespace BillingLineProductApi.SetUp
{
    public class BillingLineBuyingAreaCreation : Assertions<BillingLineBuyingArea>
    {
        private BillingLineBuyingArea GetRequest()
        {
            return new BillingLineBuyingArea
            {
                ParentId = "",
                Name = "AutomationBillingLineBuyingArea",
                EndDate = new DateTimeOffset(DateTime.Now.AddMonths(1)),
                AvailableForBillingLine = true,
                IsDisabled = false
            };
        }

        public BillingLineBuyingArea CreateBuyingArea(BillingLineBuyingArea request)
        {
            AutomationVariables AutomationVariables = AppSettingsInitialization.GetConfigInstance();
            httpService.EndPoint = AutomationVariables.BillingLineBuyingAreaAPI;
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineBuyingArea>(postResponse.Content);
            return response;
        }

        public string GetBuyingAreaId()
        {
            var automationVariables = AppSettingsInitialization.GetConfigInstance();
            httpService.EndPoint = automationVariables.OrderAPI;
            var orderResponse = httpService.PerformGet();
            var response = JSONLibrary.DeserializeJSon<List<BillingLineBuyingArea>>(orderResponse.Content);
            var selectedResponse = response.FirstOrDefault(r => r.Name == "AutomationBillingLineBuyingArea");
            var request = GetRequest();
            return (selectedResponse == null) ? CreateBuyingArea(request).Id : selectedResponse.Id;
        }

        public string GetDisabledBuyingAreaId()
        {
            var automationVariables = AppSettingsInitialization.GetConfigInstance();
            httpService.EndPoint = automationVariables.OrderAPI;
            var orderResponse = httpService.PerformGet();
            var response = JSONLibrary.DeserializeJSon<List<BillingLineBuyingArea>>(orderResponse.Content);
            var selectedResponse = response.FirstOrDefault(r => r.Name == "AutomationDisabledBillingLineBuyingArea");
            var request = GetRequest();
            request.Name = "AutomationDisabledBillingLineBuyingArea";
            request.IsDisabled = true;
            return (selectedResponse == null) ? CreateBuyingArea(request).Id : selectedResponse.Id;
        }
    }
}
