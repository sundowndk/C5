using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class VatCode
	{
		#region Private Fields
		string _id;
		string _text;
		#endregion
		
		#region Public fields
		public string Id
		{
			get
			{
				return this._id;
			}
		}
		
		public string Text
		{
			get
			{
				return this._text;
			}
		}
		
		public static VatCode Default
		{
			get
			{
				VatCode result = new VatCode ();
				result._id = string.Empty;
				result._text = string.Empty;
				return result;
			}
		}
		#endregion
		
		#region Constructor
		private VatCode ()
		{
			this._id = string.Empty;
			this._text = string.Empty;
		}
		#endregion
		
		#region Public Static Methods
		public static VatCode Load (string Id)
		{
			VatCode result = new VatCode ();
			
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("moms");
			qb.Columns 
				(
					"tekst"
					);
			
			qb.AddWhere ("moms = '"+ Id +"'");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			
			if (query.Success)
			{
				if (query.NextRow ())
				{
					result._id = Id;
					result._text = query.GetString (qb.ColumnPos ("tekst"));
				}
			}
			
			query.Dispose ();
			query = null;
			qb = null;
			
			return result;
		}
		
		public static List<VatCode> List ()
		{
			List<VatCode> result = new List<VatCode> ();
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("moms");
			qb.Columns ("moms");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			if (query.Success)
			{
				while (query.NextRow ())
				{					
					try
					{
						result.Add (Load (query.GetString (qb.ColumnPos ("moms"))));
					}
					catch
					{					
					}
				}
			}
			
			query.Dispose ();
			query = null;
			qb = null;
			
			return result;
		}
		#endregion
	}
}

