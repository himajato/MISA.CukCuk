using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Atribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequire : Attribute
    {
        public string _fieldName = string.Empty;

        public MISARequire(string fieldName)
        {
            _fieldName = fieldName;
        }
    }
}
