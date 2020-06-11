using System;
namespace Common.Models
{
    public class Contact : IIdentity
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsDisabled { get; set; }
        public string ClientId { get; set; }
        public string AgencyId { get; set; }
    }
}