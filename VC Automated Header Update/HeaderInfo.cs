using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC_Automated_Header_Update
{
    public class HeaderInfo
    {
        public string fileName { get; set; }
        public string oldVersionNumber { get; set; }
        public string newVersionNumber { get; set; }

        public HeaderInfo(string fileName, string oldVersionNumber, string newVersionNumber)
        {
            this.fileName = fileName;
            this.oldVersionNumber = oldVersionNumber;
            this.newVersionNumber = newVersionNumber;
        }

        public override string ToString()
        {
            return String.Format("{0,-55}{1}/{2}", fileName, oldVersionNumber, newVersionNumber); 
        }
    }
}
