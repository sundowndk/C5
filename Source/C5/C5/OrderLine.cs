using System;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class OrderLine
	{
		string _id;


		
		public string Id
		{
			get
			{
				return this._id;
			}
		}
		
		public OrderLine (Debitor Debitor)
		{
			this._id = string.Empty;

		}
		
		public OrderLine ()
		{
			this._id = string.Empty;

		}
	}
}

