using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VC_Automated_Header_Update
{
    class CheckVersionNumber : MainWindow
    {
        public string UpdateVerionNumber(Match m)
        { 
            float.TryParse(m.Value, out float floatTemp);
            floatTemp = floatTemp + 1;
            versionNum = floatTemp.ToString("F2");
            return (" " + floatTemp.ToString("F2") + " ");
        }

    }
}
