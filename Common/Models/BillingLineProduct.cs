using System.Collections.Generic;

namespace Common.Models
{
    public class BillingLineProduct : IIdentity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasCommission { get; set; } = true;
        public List<string> BuyingAreaIds { get; set; }
        public bool IsDisabled { get; set; }
    }
}
