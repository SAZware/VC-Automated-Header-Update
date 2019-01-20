using System;
using System.Collections.Generic;
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
            float temp;

            float.TryParse(m.Value,out temp);
            temp = temp + 1;

            return temp.ToString();
        }

    }
}
