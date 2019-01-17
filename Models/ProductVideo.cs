using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectC_v2.Models
{
    public class ProductVideo
    {
        public int ProductVideoId { get; set; }
        public string URL { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}