using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edumenu.Models
{
    public class Diet
    {
        public string name_en { get; set; }
        public string name_fi { get; set; }
        public string nameShort_en { get; set; }
        public string nameShort_fi { get; set; }
        public Uri iconPath { get; set; }

        public static Diet glutenFree = new Diet
        {
            name_en = "Gluten-free",
            name_fi = "Gluteeniton",
            nameShort_fi = "G",
        };

    }
}
