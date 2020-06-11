using System;
namespace Common.Models
{
    public class Currency: IIdentity
    {
        
            public string Id { get; set; }
            public string name { get; set; }
            public string isoCurrencySymbol { get; set; }
            public string symbol { get; set; }
            public bool IsDisabled { get; set; } = false;
          

}
}
