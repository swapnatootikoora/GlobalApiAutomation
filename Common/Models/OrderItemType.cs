using System;

namespace Common.Models
{
    public class OrderItemType : IIdentity, ICloneable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public string InitialStatus { get; set; }

        public object Clone()
        {
            var request = new OrderItemType();
            request.Id = this.Id;
            request.Name = this.Name;
            request.IsDisabled = this.IsDisabled;
            request.Url = this.Url;
            request.Key = this.Key;
            request.InitialStatus = this.InitialStatus;
            return request;
        }
    }
}