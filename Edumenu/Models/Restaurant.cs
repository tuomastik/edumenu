using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edumenu.Models
{
    public class Restaurant
    {
        public string name { get; set; }
        public string menu { get; set; }
        public Uri menuUrl { get; set; }
        public Uri homeUrl { get; set; }
    }
}
