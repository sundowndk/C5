using System;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class Invoice
	{
		#region Private Fields		
		private string _accountid;
		private int _transactionid;
		private Enums.InvoiceType _type;
		
		private DateTime _date;
		private DateTime _duedate;		
		
		private string _name;
		private string _address1;
		private string _address2;
		private string _city;
		private string _zipcode;
		private string _attention;
		
		private decimal _total;
		private decimal _subtotal;
		private decimal _vat;
		
		private List<InvoiceLine> _lines;
		#endregion
		
		#region Public Fields
		public string AccountId
		{
			get
			{
				return this._accountid;
			}
		}
		
		public int TransactionId
		{
			get
			{
				return this._transactionid;
			}
		}
		
		public Enums.InvoiceType Type
		{
			get
			{
				return this._type;
			}
		}
		
		public DateTime Date
		{
			get
			{
				return this._date;
			}
		}
		
		public DateTime DueDate
		{
			get
			{
				return this._duedate;
			}
		}
		
		public string Name
		{
			get
			{
				return this._name;
			}			
		}
		
		public string Address1
		{
			get
			{
				return this._address1;
			}
		}
		
		public string Address2
		{
			get
			{
				return this._address2;
			}
		}
		
		public string ZipCode
		{
			get
			{
				return this._zipcode;
			}
		}
		
		public string City
		{
			get
			{
				return this._city;
			}
		}
		
		public string Attention
		{
			get
			{
				return this._attention;
			}
		}
		
		public decimal Total
		{
			get
			{
				return Math.Round (this._total, 2);
			}
		}
		
		public decimal SubTotal
		{
			get
			{
				return Math.Round (this._subtotal, 2);
			}
		}
		
		public decimal VAT
		{
			get
			{
				return Math.Round (this._vat, 2);
			}
		}
		
		public System.Collections.ObjectModel.ReadOnlyCollection<InvoiceLine> Lines
		{
			get
			{
				return this._lines.AsReadOnly ();
			}
		}
		#endregion
		

		public Invoice ()
		{
			this._accountid = string.Empty;
			this._transactionid = 0;
			this._type = C5.Enums.InvoiceType.Invoice;
			
			this._date = DateTime.Now;
			this._duedate = DateTime.Now;
			
			this._name = string.Empty;
			this._address1 = string.Empty;
			this._address2 = string.Empty;
			this._city = string.Empty;
			this._zipcode = string.Empty;
			this._attention = string.Empty;
			
			this._total = 0;
			this._subtotal = 0;
			this._vat = 0;
			
			this._lines = new List<InvoiceLine> ();
		}
		
		public static Invoice Get (int invoice)
		{
			Invoice result = new Invoice ();
			
			#region GET JOURNAL
			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
				qb.Table ("debjournal");
				qb.Columns 
					(
						"dato",
						"forfald",
						"saldodkk",
						"momsberegnes",
						"moms",
						"transaktion"
					);
			
				qb.AddWhere ("faktura = "+ invoice.ToString ());
				
				Query query = Runtime.DBConnection.Query (qb.QueryString);
	
				if (query.Success)
				{
					if (query.NextRow ())
					{
						result._transactionid = query.GetInt (qb.ColumnPos ("transaktion"));
						result._date = query.GetDateTime (qb.ColumnPos ("dato"));
						result._duedate = query.GetDateTime (qb.ColumnPos ("forfald"));
						result._total = query.GetDecimal (qb.ColumnPos ("saldodkk"));
						result._subtotal = query.GetDecimal (qb.ColumnPos ("momsberegnes"));
						result._vat = query.GetDecimal (qb.ColumnPos ("moms"));
					}
				}
			
				query.Dispose ();
				query = null;
				qb = null;
			}
			#endregion

			#region GET ORDKARTARKIV
			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
				qb.Table ("ordkartarkiv");
				qb.Columns 
					(
						"konto",
						"navn",
						"adresse1",
						"adresse2",
						"postby",
						"attention"
					);
			
				qb.AddWhere ("fakturafxlgeseddel = "+ invoice.ToString ());
			
				Query query = Runtime.DBConnection.Query (qb.QueryString);
				if (query.Success)
				{
					if (query.NextRow ())
					{
						result._accountid = query.GetString (qb.ColumnPos ("konto"));
						result._name = query.GetString (qb.ColumnPos ("navn"));
						result._address1 = query.GetString (qb.ColumnPos ("adresse1"));
						result._address2 = query.GetString (qb.ColumnPos ("adresse2"));
						result._zipcode = query.GetString (qb.ColumnPos ("postby"));
						result._city = query.GetString (qb.ColumnPos ("postby"));
						result._attention = query.GetString (qb.ColumnPos ("attention")); 
					}
				}
			
				query.Dispose ();
				query = null;
				qb = null;
			}
			#endregion
			
			#region GET DEBPOST
			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
				qb.Table ("debpost");
				qb.Columns 
					(
						"posttype"					
					);
			
				qb.AddWhere ("bilag = "+ invoice.ToString ());
			
				Query query = Runtime.DBConnection.Query (qb.QueryString);
				if (query.Success)
				{
					if (query.NextRow ())
					{
						if (query.GetInt (qb.ColumnPos ("posttype")) == 1)
						{
							result._type = Enums.InvoiceType.Invoice;
						}
						else if (query.GetInt (qb.ColumnPos ("posttype")) == 2)
						{
							result._type = Enums.InvoiceType.InvoiceCredit;
						}
					}
				}
			
				query.Dispose ();
				query = null;
				qb = null;
			}
			#endregion
			
			#region GET ORDLINEARKIV
			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
				qb.Table ("ordliniearkiv");
				qb.Columns 
					(
						"linienr",
						"varenummer",
						"antal",
						"enhed",
						"pris",
						"rabat",
						"belxb",
						"tekst",
						"lxbenummer"
					);
			
				qb.AddWhere ("transaktion = "+ result._transactionid);
				qb.OrderBy ("linienr", QueryBuilderOrder.Accending);
			
				Query query = Runtime.DBConnection.Query (qb.QueryString);
				if (query.Success)
				{					
					while (query.NextRow ())
					{
						InvoiceLine line = new InvoiceLine ();
						line._linenumber = query.GetDecimal (qb.ColumnPos ("linienr"));
						line._partnumber = query.GetString (qb.ColumnPos ("varenummer"));
						line._amount = query.GetDecimal (qb.ColumnPos ("antal"));
						line._price = query.GetDecimal (qb.ColumnPos ("pris"));
						line._unit = query.GetString (qb.ColumnPos ("enhed"));
						line._discount = query.GetDecimal (qb.ColumnPos ("rabat"));
						line._total = query.GetDecimal (qb.ColumnPos ("belxb"));
						line._text = query.GetString (qb.ColumnPos ("tekst"));
						line._sequenceno = query.GetInt (qb.ColumnPos ("lxbenummer"));
							
						#region GET NOTAT
						{
							QueryBuilder qb2 = new QueryBuilder (QueryBuilderType.Select);
							qb2.Table ("notat");
							qb2.Columns 
								(
									"tekst"
									);
							qb2.AddWhere ("notatrecid = "+ line._sequenceno.ToString ());
							
							Query query2 = Runtime.DBConnection.Query (qb2.QueryString);
							if (query2.Success)
							{
								if (query2.NextRow ())
								{
									line._note = query2.GetString (qb2.ColumnPos ("tekst"));
								}
							}
						
							query2.Dispose ();						
							query2 = null;
							qb2 = null;
						}
						#endregion
						
						result._lines.Add (line);
					}
				}
			
				query.Dispose ();
				query = null;
				qb = null;
			}
			#endregion
			
			return result;
		}
	}
}


