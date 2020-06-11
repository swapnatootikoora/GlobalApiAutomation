using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class People : IIdentity
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsDisabled { get; set; }
        public List<int> Capabilities { get; set; }
    }
}
