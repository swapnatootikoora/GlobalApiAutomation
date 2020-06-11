using System;

namespace Common.Models
{
    public class BillingLineBuyingArea : IIdentity
    {  
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public bool AvailableForBillingLine { get; set; } = true;
        public bool IsDisabled { get; set; }
    }
}
    

