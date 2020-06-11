using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class BillingLineItem : IIdentity
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string PlannerId { get; set; }
        public string BillingLineProductId { get; set; }
        public DateTimeOffset StartMonth { get; set; }
        public int DurationInMonths { get; set; }
        public List<BillingLineBuyingAreaRevenue> BillingLineBuyingAreaRevenues { get; set; }
        public decimal Revenue => CalculateRevenue(BillingLineBuyingAreaRevenues);

        public decimal CalculateRevenue(List<BillingLineBuyingAreaRevenue> billingLineBuyingAreaRevenues)
        {
            var revenueTotal = 0.0M;
            foreach(var billingLineBuyingAreaRevenue in billingLineBuyingAreaRevenues)
            {
                var monthBuyingAreaRevenues = billingLineBuyingAreaRevenue.MonthBuyingAreaRevenues;
                foreach(var monthBuyingAreaRevenue in monthBuyingAreaRevenues)
                {
                    revenueTotal = revenueTotal + monthBuyingAreaRevenue.Value;
                }
            }
            return revenueTotal;
        }
    }

    public class BillingLineBuyingAreaRevenue
    {
        public string Id { get; set; }
        public List<MonthBuyingAreaRevenue> MonthBuyingAreaRevenues { get; set; }
    }

    public class MonthBuyingAreaRevenue
    {
        public DateTimeOffset Date { get; set; }
        public decimal Value { get; set; }
    }
}
