using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HelpDesk.Common.Models
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, double y)
        {
            this.Label = label;
            this.Y = y;
        }

        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
        [DataMember(Name = "label")]
        public string Label = "";

       
       
    }
}
