using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Person
{
    public enum EnumAnrede
    {
        [Description("Herr")]
        Herr = 0,

        [Description("Frau")]
        Frau = 1
    }
}
