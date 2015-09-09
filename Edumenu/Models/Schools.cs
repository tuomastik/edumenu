using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edumenu.Models
{
    public class Schools
    {
        public static School tut = new School
        {
            name_en = "Tampere University of Technology",
            name_fi = "Tampereen teknillinen yliopisto",
            nameShort_en = "TUT",
            nameShort_fi = "TTY",
        };
        public static School uta = new School
        {
            name_en = "University of Tampere",
            name_fi = "Tampereen yliopisto",
            nameShort_en = "UTA",
            nameShort_fi = "TAY",
        };
        public static School tays = new School
        {
            name_en = "Tampere University Hospital",
            name_fi = "Tampereen yliopistollinen sairaala",
            nameShort_en = "TAYS",
            nameShort_fi = "TAYS",
        };
        public static School tamk = new School
        {
            name_en = "Tampere University of Applied Sciences",
            name_fi = "Tampereen ammattikorkeakoulu",
            nameShort_en = "TAMK",
            nameShort_fi = "TAMK",
        };
        public static School takk = new School
        {
            name_en = "Tampere Adult Education Centre",
            name_fi = "Tampereen Aikuiskoulutuskeskus",
            nameShort_en = "TAKK",
            nameShort_fi = "TAKK",
        };
    }
}
