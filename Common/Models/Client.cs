using System;
namespace Common.Models
{
    public class Client : IIdentity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CustomerReferenceNumber { get; set; }
        public string ExternalId { get; set; }
        public bool IsDisabled { get; set; }
        public int TradingType { get; set; }
        public string AgencyId { get; set; }        
    }
}
