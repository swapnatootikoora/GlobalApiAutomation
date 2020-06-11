using System;
namespace Common.Models
{
    public class Product: IIdentity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public string BrandId { get; set; }
    }
}
