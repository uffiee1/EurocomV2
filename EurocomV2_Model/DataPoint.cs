using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace EurocomV2_Model
{
	[DataContract]
	public class DataPoint
	{
		public DataPoint(DateTime x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "x")]
		public Nullable<DateTime> X = null;

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}
