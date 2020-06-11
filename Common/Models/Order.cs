using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class Order : IIdentity, ICloneable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
        public List<string> ContactIds { get; set; }
        public List<string> OwnerIds { get; set; }
        public int Status { get; set; }
        public string ClientBrandId { get; set; }
        public string BrandProductId { get; set; }
        public string AgencyId { get; set; }

        public object Clone()
        {
            var request = new Order();
            request.Id = this.Id;
            request.Name = this.Name;
            request.ClientId = this.ClientId;
            request.ContactIds = this.ContactIds;
            request.OwnerIds = this.OwnerIds;
            request.Status = this.Status;
            request.ClientBrandId = this.ClientBrandId;
            request.BrandProductId = this.BrandProductId;
            request.AgencyId = this.AgencyId;
            return request;
        }
    }

    public interface IIdentity
    {
        public string Id { get; set; }
    }
}
