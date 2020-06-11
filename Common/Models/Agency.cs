using System;
namespace Common.Models
{
    public class Agency : IIdentity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CustomerReferenceNumber { get; set; }
        public string externalId { get; set; }
        public bool IsDisabled { get; set; }
    }
}
