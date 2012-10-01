using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class CreditPolicy
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

		public static CreditPolicy Default
		{
			get
			{
				CreditPolicy result = new CreditPolicy ();
				result._id = string.Empty;
				result._text = string.Empty;
				return result;
			}
		}
		#endregion

		#region Constructor
		private CreditPolicy ()
		{
			this._id = string.Empty;
			this._text = string.Empty;
		}
		#endregion

		#region Public Static Methods
		public static CreditPolicy Load (string Id)
		{
			CreditPolicy result = new CreditPolicy ();

						
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("betaling");
			qb.Columns 
				(
					"tekst"
				);
				
			qb.AddWhere ("betaling = '"+ Id +"'");
				
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
						
		public static List<CreditPolicy> List ()
		{
			List<CreditPolicy> result = new List<CreditPolicy> ();
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("betaling");
			qb.Columns ("betaling");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			if (query.Success)
			{
				while (query.NextRow ())
				{					
					try
					{
						result.Add (Load (query.GetString (qb.ColumnPos ("betaling"))));
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

