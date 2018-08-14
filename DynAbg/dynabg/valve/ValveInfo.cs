using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DynAbg.valve
{
	internal class ValveInfo
	{
		[DataMember]
		public string Manufacturer { get; set; }
		[DataMember]
		public string TGA { get; set; }
		[DataMember]
		public string Description { get; set; }
	}
}
