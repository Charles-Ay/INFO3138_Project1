using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Account
    {
        public string Description { get; set; }
        public int UserID { get; set; }
        public string LoginUrl { get; set; }
        public int AccountNum { get; set; }
        public Password Password { get; set; }
    }

    public class Password
    {
        public string Value { get; set; }
        public int StrengthNum { get; set; }
        public string StrengthText { get; set; }
        public string LastReset { get; set; }
    }

    public class Root
    {
        public Account Account { get; set; }
    }
}
