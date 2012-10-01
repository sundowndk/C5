using System;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class OrderLine
	{
		#region Private Fields
		string _id;

		int _createtimestamp;
		int _updatetimestamp;

		string _orderid;
		string _productid;

		decimal _sort;

		DateTime _periodbegin;
		DateTime _periodend;

		string _text;

		decimal _amount;
		string _unit;

		decimal _price;
		decimal _total;

		string _notes;
		#endregion

		#region Temp Fields
		bool _temp1 = true; // Fales if object has been saved. True if object has not yet been saved.
		#endregion
				
		#region Public Fields
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		public int CreateTimestamp
		{
			get
			{
				return this._createtimestamp;
			}
		}

		public int UpdateTimestamp
		{
			get
			{
				return this._updatetimestamp;
			}
		}

		public string OrderId
		{
			get
			{
			
				return this._orderid;
			}
		}

		public string ProductId
		{
			get
			{
				return this._productid;
			}

			set
			{
				this._productid = value;
			}
		}

		public decimal Sort
		{
			get
			{
				return this._sort;
			}

			set
			{
				this._sort = value;
			}
		}

		public DateTime PeriodBegin
		{
			get
			{
				return this._periodbegin;
			}

			set
			{
				this._periodbegin = value;
			}
		}

		public DateTime PeriodEnd
		{
			get
			{
				return this._periodend;
			}

			set
			{
				this._periodend = value;
			}
		}

		public string Text
		{
			get
			{
				return this._text;
			}
			
			set
			{
				this._text = value;
			}
		}

		public decimal Amount
		{
			get
			{
				return this._amount;
			}

			set
			{
				this._amount = value;
			}
		}

		public string Unit
		{
			get
			{
				return this._unit;
			}
			
			set
			{
				this._unit = value;
			}
		}

		public decimal Price
		{
			get
			{
				return this._price;
			}

			set
			{
				this._price = value;
			}
		}

		public decimal Total
		{
			get
			{
				return this._total;
			}

			set
			{
				this._total = value;
			}
		}

		public string Notes
		{
			get
			{
				return this._notes;
			}

			set
			{
				this._notes = value;
			}
		}
		#endregion

		#region Constructor
		public OrderLine (Order Order)
		{
			this._id = C5.Helpers.NewSequenceNumber ().ToString ();

			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();

			this._orderid = Order.Id;
			this._productid = string.Empty;

			this._sort = 0;

			this._periodbegin = DateTime.Now;
			this._periodend = DateTime.Now;

			this._text = string.Empty;

			this._amount = 0;
			this._unit = string.Empty;

			this._price = 0;
			this._total = 0;

			this._notes = string.Empty;
		}

		private OrderLine ()
		{
			this._id = string.Empty;

			this._createtimestamp = 0;
			this._updatetimestamp = 0;

			this._orderid = string.Empty;
			this._productid = string.Empty;

			this._sort = 0;

			this._periodbegin = DateTime.Now;
			this._periodend = DateTime.Now;

			this._text = string.Empty;

			this._amount = 0;
			this._unit = string.Empty;

			this._price = 0;
			this._total = 0;

			this._notes = string.Empty;
		}
		#endregion

		#region Public Methods
		public void Save ()
		{
			// If OrderLine is new, we need to fill in alot of initial data.
			// TEMP1
			if (this._temp1)
			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Insert);
				qb.Table ("ordlinie");
				
				qb.Columns 	
					(
						"dataset",
						"lxbenummer",
						"sidstrettet",
						"nummer",
						"linienr",
						"varenummer",
						"lokation",
						"antal",
						"pris",
						"rabat",
						"belxb",
						"tekst",
						"enhed",
						"moms",
						"levernu",
						"oprettet",
						"levering",
						"bekrxftet",
						"konto",
						"serienummer",
						"leveret",
						"faktureret",
						"leveretdkk",
						"transaktion",
						"kostpris",
						"sletstatistik",
						"sletafgift",
						"liniestatus",
						"lagerstatus",
						"medarbejder",
						"samlerefid",
						"ordreref",
						"handelskode",
						"antalfysisk",
						"fjernlistekode",
						"prisenhed"
					 );
				
				qb.Values 
					(
						"DAT", // datasaet
						int.Parse (this._id), // lxbenummer
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._updatetimestamp)), // sidstrettet
						this._orderid.PadLeft (10, ' '), // nummer
						this._sort, // linienr
						this._productid, // varenummer
						string.Empty, // lokation
						this._amount, // antal
						this._price, // pris
						0, // rabat 
						this._total, // belxb
						this._text, // tekst
						this._unit, // enhed
						"U25", // moms
						1, // levernu
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._createtimestamp)), // oprettet
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._createtimestamp)), // levering
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._createtimestamp)), // bekrxftet
						string.Empty, // konto
						string.Empty, // serienummer
						0, // leveret
						0, // faktureret
						0, // leveretdkk
						0, // transaktion
						0, // kostpris
						0, // sletstatistik
						string.Empty, // sletafgift
						2, // liniestatus
						2, // lagerstatus
						string.Empty, // medarbejder
						0, // samlerefid
						string.Empty, // ordreref
						string.Empty, // handelskode
						0, // antalfysisk
						0, // fjernlistekode
						1 // prisenhed
					);
				
				Query query = Runtime.DBConnection.Query (qb.QueryString);

				Console.WriteLine (qb.QueryString);
				
				if (query.AffectedRows == 0) 
				{
					// Exception: OrderLineSave
					throw new Exception (string.Format (Strings.Exception.OrderLineSave, this._id));
				}
				
				query.Dispose ();
				query = null;
				qb = null;

				// TEMP1
				this._temp1 = false;
			}
			// If OrderLine is not new, just update the fields needed.
			else
			{
				this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();

				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Update);
				qb.Table ("ordlinie");
				
				qb.Columns 	
					(
						"sidstrettet",
						"varenummer",
						"linienr",
						"antal",
						"pris",
						"belxb",
						"tekst",
						"enhed"
					);
				
				qb.Values
					(
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._updatetimestamp)), // sidstrettet
						this._productid, // varenummer
						this._sort, // linienr
						this._amount, // antal
						this._price, // pris
						this._total, // belxb
						this._text, // tekst
						this._unit // enhed
					);
				
				qb.AddWhere ("lxbenummer like '%"+ this._id +"'");
				
				Query query = Runtime.DBConnection.Query (qb.QueryString);
				
				if (query.AffectedRows == 0) 
				{
					// Exception: OrderLineSave
					throw new Exception (string.Format (Strings.Exception.OrderLineSave, this._id));
				}
				
				query.Dispose ();
				query = null;
				qb = null;
			}

			// Save notes.
			if (this._notes != string.Empty)
			{
				// Remove old notes.
				{
					QueryBuilder qb = new QueryBuilder (QueryBuilderType.Delete);
					qb.Table ("notat");
					qb.AddWhere ("notatrecid = '"+ this._id +"'");
				
					Query query = Runtime.DBConnection.Query (qb.QueryString);
								
					query.Dispose ();
					query = null;
					qb = null;
				}

				// Write new notes.
				{
					int lineno = 0;
					foreach (string note in this._notes.Split ("\n".ToCharArray (), StringSplitOptions.RemoveEmptyEntries))
					{
						QueryBuilder qb = new QueryBuilder (QueryBuilderType.Insert);
						qb.Table ("notat");
						
						qb.Columns 	
							(
								"dataset",
								"lxbenummer",
								"sidstrettet",
								"notatfileid",
								"notatrecid",
								"linienummer",
								"tekst",
								"dato"
								);
						
						qb.Values
							(
								"DAT", // datasaet
								Helpers.NewSequenceNumber (), // lxbenummer
								String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._updatetimestamp)), // sidstrettet
								"128", // notatfileid
								this._id, // notatrecid
								lineno*2, // linienummer
								note, // tekst							
								String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._updatetimestamp)) // dato
								);
												
						Query query = Runtime.DBConnection.Query (qb.QueryString);
						
						if (query.AffectedRows == 0) 
						{
							// Exception: OrderLineSave
//							throw new Exception (string.Format (Strings.Exception.OrderLineSave, this._id));
						}
						
						query.Dispose ();
						query = null;
						qb = null;
					}
				}
			}

//			if (trim($line['note']) != ""){
//				$split = explode("\n", $line['note']);
//				$o = 1;
//				foreach($split as $part){
//					mssql_query("INSERT INTO notat VALUES ('DAT', '".$this->get_lxbenummer()."', '".strftime("%Y-%m-%d")." 00:00:00.000', 128, ".$LXBE.", ".($o*2).", '".$part."', '".strftime("%Y-%m-%d")." 00:00:00.000')");
//					$o++;
//				}
//			}

		}
		#endregion

		#region Public Static Methods
		public static OrderLine Load (string Id)
		{
			bool success = false;
			OrderLine result = new OrderLine ();

			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
				qb.Table ("ordlinie");
				qb.Columns 
					(
						"sidstrettet",
						"varenummer",
						"linienr",
						"antal",
						"pris",
						"belxb",
						"tekst",
						"enhed",
						"oprettet"
					);
			
				qb.AddWhere ("lxbenummer like '"+ Id +"'");
			
				Query query = Runtime.DBConnection.Query (qb.QueryString);
			
				if (query.Success)
				{
					if (query.NextRow ())
					{
						result._id = Id;
						result._createtimestamp = SNDK.Date.DateTimeToTimestamp (query.GetDateTime (qb.ColumnPos ("oprettet")));
						result._productid = query.GetString (qb.ColumnPos ("varenummer"));
						result._sort = query.GetDecimal (qb.ColumnPos ("linienr"));
						result._amount = query.GetDecimal (qb.ColumnPos ("antal"));
						result._price = query.GetDecimal (qb.ColumnPos ("pris"));
						result._total = query.GetDecimal (qb.ColumnPos ("belxb"));
						result._text = query.GetString (qb.ColumnPos ("tekst"));
						result._unit = query.GetString (qb.ColumnPos ("enhed"));
						result._updatetimestamp = SNDK.Date.DateTimeToTimestamp (query.GetDateTime (qb.ColumnPos ("sidstrettet")));

						success = true;
					}
				}
			
				query.Dispose ();
				query = null;
				qb = null;
			
				if (!success)
				{
					// Exception: Exception.OrderLineLoadId
					throw new Exception (string.Format (Strings.Exception.OrderLineLoadId, Id));
				}
			}

			// Load notes.
			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
				qb.Table ("notat");
				qb.Columns 
					(
						"tekst"
						);

				qb.AddWhere ("notatrecid = '"+ Id +"'");

				qb.OrderBy ("linienummer", QueryBuilderOrder.Accending);
				
				Query query = Runtime.DBConnection.Query (qb.QueryString);
				
				if (query.Success)
				{
					while (query.NextRow ())
					{
						result._notes += query.GetString (qb.ColumnPos ("tekst")) +"\n";
					}
				}
								
				query.Dispose ();
				query = null;
				qb = null;

				result._notes = result._notes.TrimEnd ("\n".ToCharArray ());
			}

			// TEMP1
			result._temp1 = false;

			return result;
		}

		public static void Delete (string Id)
		{
			bool success = false;

			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Delete);
			qb.Table ("ordlinie");			
			qb.AddWhere ("lxbenummer like '"+ Id +"'");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			
			if (query.AffectedRows > 0) 
			{
				success = true;
			}
			
			query.Dispose ();
			query = null;
			qb = null;
			
			if (!success) 
			{
				// Exception: OrderLineDelete
				throw new Exception (string.Format (Strings.Exception.OrderLineDeleteId, Id));
			}
		}

		public static List<OrderLine> List ()
		{
			return List (string.Empty);
		}

		public static List<OrderLine> List (Order Order)
		{
			return List (Order.Id);
		}

		internal static List<OrderLine> List (string OrderId)
		{
			List<OrderLine> result = new List<OrderLine> ();

			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("ordlinie");
			qb.Columns ("lxbenummer");

			if (OrderId != string.Empty)
			{
				qb.AddWhere ("nummer like '%"+ OrderId +"'");
			}

			Query query = Runtime.DBConnection.Query (qb.QueryString);
			if (query.Success)
			{
				while (query.NextRow ())
				{					
					try
					{
						result.Add (Load (query.GetInt (qb.ColumnPos ("lxbenummer")).ToString ()));
					}
					catch
					{					
					}
				}
			}
			
			query.Dispose ();
			query = null;
			qb = null;

			result.Sort (delegate (OrderLine ol1, OrderLine ol2) { return ol1._sort.CompareTo (ol2._sort); });

			return result;
		}
		#endregion
	}
}
