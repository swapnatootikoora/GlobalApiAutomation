using System;
namespace Common.Models
{
    public class Brand: IIdentity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public string ClientId { get; set; }
    }
}
