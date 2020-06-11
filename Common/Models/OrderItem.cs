using System;

namespace Common.Models
{
    public class OrderItem : IIdentity
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public string TypeId { get; set; }
        public string TypeKey { get; set; }
        public string OwnerId { get; set; }
        public bool IsDisabled { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public decimal? Revenue { get; set; }
        public int Status { get; set; } = 2;
    }
}