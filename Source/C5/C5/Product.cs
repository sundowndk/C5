using System;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class Product
	{
		string _id;
		int _transactionid;
		string _name;
		decimal _price;
						
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		public string Name
		{
			get
			{
				return this._name;
			}
		}

		public decimal Price
		{
			get
			{
				return Math.Round (this._price, 2);
			}
		}
				
		public Product ()
		{
			this._id = string.Empty;
		}
				
		public static Product Load (string Id)
		{
			Product result = new Product ();

			// get LAGKART
			{			
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
				qb.Table ("lagkart");
				qb.Columns 
					(
						"lxbenummer",
						"varenummer",
						"varenavn1"
					);
			
				qb.AddWhere ("varenummer like '%"+ Id +"'");
			
				Query query = Runtime.DBConnection.Query (qb.QueryString);
			
				if (query.Success)
				{
					if (query.NextRow ())
					{
						result._id = Id;
						result._transactionid = query.GetInt (qb.ColumnPos ("lxbenummer"));
						result._name = query.GetString (qb.ColumnPos ("varenavn1"));										
					}
				}

				query.Dispose ();
				query = null;
				qb = null;
			}

			// get LAGPRIS
			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
				qb.Table ("lagpris");
				qb.Columns 
					(
						"pris"
						);
				
				qb.AddWhere ("varenummer like '%"+ Id +"'");
				
				Query query = Runtime.DBConnection.Query (qb.QueryString);
				
				if (query.Success)
				{
					if (query.NextRow ())
					{
						result._price = query.GetDecimal (qb.ColumnPos ("pris"));
					}
				}
				
				query.Dispose ();
				query = null;
				qb = null;
			}
			
			return result;
		}
		
		public static List<Product> List ()
		{
			List<Product> result = new List<Product> ();
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("lagkart");
			qb.Columns ("varenummer");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			if (query.Success)
			{
				while (query.NextRow ())
				{					
					try
					{


						result.Add (Load (query.GetString (qb.ColumnPos ("varenummer"))));
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
	}
}

