using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VC_Automated_Header_Update
{
    class CheckVersionNumber
    {

        public string UpdateVerionNumber(Match m)
        {

            if (float.TryParse(m.Value, out float temp))
            { 
                MainWindow.VersionNum = temp + 1;
                return (" " + MainWindow.VersionNum.ToString("F2") + " ");
            }
            return "Error in class CheckVersionNumber";
        }

    }
}
