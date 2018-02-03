using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Product
    {
        public string name { get; set; }
        public string price { get; set; }
        public string imgUrl { get; set; }
        public string link { get; set; }

        public Product(string name, string price, string imgUrl, string link)
        {
            this.name = name;
            this.price = price;
            this.imgUrl = imgUrl;
            this.link = link;
        }

    }
}
