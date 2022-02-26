using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Getfund.Models
{
    public class ProfileShow
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Nullable<int> NID { get; set; }
    }
}