using System.Collections.Generic;
using BillingLineProductApi.SetUp;
using Common.Api;
using Common.ConfigUtils;
using Common.Helpers;
using Common.Models;

namespace BillingLineItemApi.SetUp
{
    public class BillingLineProductCreation : Assertions<BillingLineProduct>
    {
        public BillingLineProduct BillingLineProduct(string BuyingAreaId)
        {
            return new BillingLineProduct
            {
                Name = "NewBillingLineProduct",
                BuyingAreaIds = new List<string> { BuyingAreaId },
                IsDisabled = false
            };
        }
        public string CreateProductID(BillingLineProduct request)
        {
            var automationVariables = AppSettingsInitialization.GetConfigInstance();
            httpService.EndPoint = automationVariables.BillingLineProductAPI;
            var productResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(productResponse.Content);
            return response.Id;
        }

        public string GetProductId()
        {
            var request = BillingLineProduct(new BillingLineBuyingAreaCreation().GetBuyingAreaId());
            return CreateProductID(request);
        }

        public string GetDisabledProductId()
        {
            var request = BillingLineProduct(new BillingLineBuyingAreaCreation().GetBuyingAreaId());
            request.IsDisabled = true;
            return CreateProductID(request);
        }

    }

}





