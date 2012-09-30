using System;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class Order
	{
		#region Private Fields
		string _id;

		int _createtimestamp;
		int _updatetimestamp;

		C5.Debitor _debitor;

		List<OrderLine> _orderlines;
		#endregion

		#region Temp Fields
		string _temp1 = string.Empty; // Holds OrderLine ids that needs to be deleted.
		bool _temp2 = true; // Fales if object has been saved. True if object has not yet been saved.
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

		public bool Invoiced
		{
			get
			{
				bool result = false;

				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
				qb.Table ("ordkartarkiv");
				qb.Columns 
					(
						"lxbenummer"
						);
				
				qb.AddWhere ("nummer like '%"+ this._id +"'");
				
				Query query = Runtime.DBConnection.Query (qb.QueryString);
				if (query.Success)
				{
					if (query.NextRow ())
					{
						result = true;
					}
				}
				
				query.Dispose ();
				query = null;
				qb = null;

				return result;
			}
		}

		public Debitor Debitor
		{
			get
			{
				return this._debitor;
			}
		}

		public IList<OrderLine> OrderLines
		{
			get
			{
				return this._orderlines.AsReadOnly ();
			}
		}
		#endregion

		#region Constructor
		public Order (Debitor Debitor)
		{
			this._id = C5.Helpers.NewOrderId ();

			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();

			this._debitor = Debitor;

			this._orderlines = new List<OrderLine> ();		
		}

		private Order ()
		{
			this._id = string.Empty;

			this._createtimestamp = 0;
			this._updatetimestamp = 0;

			this._debitor = null;

			this._orderlines = new List<OrderLine> ();
		}
		#endregion

		#region Public Methods
		public void Save ()
		{
			// TODO: This should be fixed.
			int gebyrfm = 0;
			if (this._debitor.Url == string.Empty)
			{
				gebyrfm = 30;
			}

			// If Order has not yet been saved, we need to insert a new record.
			// TEMP2
			if (this._temp2)
			{
				int sequencenumber = C5.Helpers.NewSequenceNumber ();

				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Insert);
				qb.Table ("ordkart");

				qb.Columns 	
					(
						"dataset",
						"lxbenummer",
						"sidstrettet",
						"lxs",
						"nummer",
						"sxgenavn",
						"oprettet",
						"leveres",
						"konto",
						"navn",
						"adresse1",
						"adresse2",
						"postby",
						"land",
						"attention",
						"telefon",
						"telefax",
						"fakturakonto",
						"gruppe",
						"fastrabat",
						"prisgruppe",
						"rabatgruppe",
						"kasserabat",
						"valuta",
						"sprog",
						"betaling",
						"levering",
						"spxrret",
						"sxlger",
						"moms",
						"beholdning",
						"afdeling",
						"gironummer",
						"momsnummer",
						"billede",
						"levering1",
						"levering2",
						"levering3",
						"levering4",
						"levland",
						"deresref",
						"vorref",
						"ordre",
						"momsberegnes",
						"momsberegnet",
						"rabat",
						"afgiftfm",
						"gebyrfm",
						"afrunding",
						"momsbelxb",
						"afgiftem",
						"gebyrem",
						"fakturatotal",
						"liniemoms",
						"transaktion",
						"sletstatistik",
						"slettransport",
						"godkendt",
						"lagerstatus",
						"fakturafxlgeseddel",
						"fakturafxlgeseddeldato",
						"kontant",
						"listekode",
						"linierabat",
						"varebelxb",
						"momsgrundlag",
						"handelskode",
						"transkode",
						"enummer",
						"email",
						"levemail",
						"betalingsid"
					);

				qb.Values 
					(
						"DAT", // dataset
						sequencenumber, // lxbenummer
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._updatetimestamp)), // sidstrettet
						0, // lxs
						this._id.PadLeft (10, ' '), // nummer
						"Webordre #"+ this._id, // sxgenavn
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._createtimestamp)), // oprettet
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._createtimestamp)), // leveres
						this._debitor.Id.PadLeft (10, ' '), // konto
						this._debitor.Name, // navn
						this._debitor.Address1, // address1
						this._debitor.Address2, // address2
						this._debitor.PostCode +" "+ this._debitor.City, // postby
						this._debitor.Country, // land
						this._debitor.Attention, // attention
						this._debitor.Phone, // telefon
						this._debitor.Fax, // telefax
						this._debitor.Id.PadLeft (10, ' '), // fakturakonto
						string.Empty, // gruppe
						0, // fastrabat
						"Salg", // prisgruppe
						string.Empty, // rabatgruppe
						string.Empty, // kasserabat
						"DKK", // valuta
						0, // sprog
						this._debitor.CreditPolicy, // betaling
						string.Empty, // levering
						0, // spxrret
						string.Empty, //sxlger
						this._debitor.VatCode, // moms
						0, // beholdning
						string.Empty, // afdeling
						string.Empty, // gironummer
						this._debitor.VatNo, // momsnummer
						string.Empty, // billede
						string.Empty, // levering1
						string.Empty, // levering2
						string.Empty, // levering3
						string.Empty, // levering4
						string.Empty, // levland
						string.Empty, // deresref
						string.Empty, // vorref
						string.Empty, // ordre
						0, // momsberegnes
						0, // momsberegnet
						0, // rabat
						0, // afgiftfm
						gebyrfm, // gebyrfm
						0, // afrunding
						0, // momsbelxb
						0, // afgiftem
						0, // gebyrem
						0, // fakturatotal
						0, // liniemoms
						0, // transaktion
						0, // sletstatestik
						0, // slettransport
						1, // godkendt
						2, // lagerstatus
						string.Empty, // fakturafxlgeseddel
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._createtimestamp)), // fakturafxlgeseddeldato
						0, // kontant
						0, // listekode
						0, // linierabat
						0, // varebelxb
						0, // momsgrundlag
						string.Empty, // handelskode
						string.Empty, // transkode
						string.Empty, // enummer
						this._debitor.Email, // email
						string.Empty, // levmail
						string.Empty // betalingstid
					);

				Query query = Runtime.DBConnection.Query (qb.QueryString);

				if (query.AffectedRows == 0) 
				{
					// Exception: OrderSave
					throw new Exception (string.Format (Strings.Exception.OrderSave, this._id));
				}
				
				query.Dispose ();
				query = null;
				qb = null;

				// TEMP2
				this._temp2 = false;
			}
			// If OrderLine has allready been saved before we only need to update the record.
			else
			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Update);
				qb.Table ("ordkart");
				
				qb.Columns 	
					(
						"sidstrettet",					
						"konto",
						"navn",
						"adresse1",
						"adresse2",
						"postby",
						"land",
						"attention",
						"telefon",
						"telefax",
						"email",
						"fakturakonto",
						"betaling",
						"momsnummer",
						"moms",
						"gebyrfm"
					);
				
				qb.Values
					(
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._updatetimestamp)), // sidstrettet
						this._debitor.Id.PadLeft (10, ' '), // konto
						this._debitor.Name, // navn
						this._debitor.Address1, // address1
						this._debitor.Address2, // address2
						this._debitor.PostCode +" "+ this._debitor.City, // postby
						this._debitor.Country, // land
						this._debitor.Attention, // attention
						this._debitor.Phone, // telefon
						this._debitor.Fax, // telefax
						this._debitor.Email, // email
						this._debitor.Id.PadLeft (10, ' '), // fakturakonto
						this._debitor.CreditPolicy, // betaling
						this._debitor.VatNo, // momsnummer
						this._debitor.VatCode, // moms
						gebyrfm // gebyrfm
					);
				
				qb.AddWhere ("nummer like '%"+ this._id +"'");
				
				Query query = Runtime.DBConnection.Query (qb.QueryString);
				
				if (query.AffectedRows == 0) 
				{
					// Exception: OrderSave
					throw new Exception (string.Format (Strings.Exception.OrderSave, this._id));
				}
				
				query.Dispose ();
				query = null;
				qb = null;
			}

			// Save orderlines added.
			foreach (OrderLine line in this._orderlines)
			{
				line.Save ();				
			}

			// Delete orderlines removed.
			// TEMP1
			foreach (string orderlineid in this._temp1.Split (";".ToCharArray (), StringSplitOptions.RemoveEmptyEntries))
			{
				try
				{
					OrderLine.Delete (orderlineid);
				}
				catch
				{
					// This will catch deletion of orderlines not yet saved.
				}
			}
		}

		public void AddLine (string ProductId, string Text, DateTime PeriodBegin, DateTime PeriodEnd, string Unit, decimal Amount, decimal Price, decimal Total)
		{
			OrderLine line = new OrderLine (this);

			line.ProductId = ProductId;
			line.Sort = this._orderlines.Count;
			line.Text = Text;
			line.PeriodBegin = PeriodBegin;
			line.PeriodEnd = PeriodEnd;
			line.Unit = Unit;
			line.Amount = Amount;
			line.Price = Price;
			line.Total = Total;

			this._orderlines.Add (line);
		}

		public void RemoveLine (string OrderLineId)
		{
			// Keep track of what orderlines to delete on save.
			if (this._orderlines.Find (delegate (OrderLine ol) { return ol.Id == OrderLineId; }) != null)
			{
				// TEMP1
				this._temp1 += ";"+ OrderLineId;
				this._orderlines.RemoveAll (delegate (OrderLine ol) { return ol.Id == OrderLineId; });
			}
		}
		#endregion

		#region Public Static Methods
		public static Order Load (string Id)
		{
			bool success = false;
			Order result = new Order ();
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("ordkart");
			qb.Columns 
				(
					"sidstrettet",
					"oprettet",
					"konto"
				);
			
			qb.AddWhere ("nummer like '%"+ Id +"'");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);


			if (query.Success)
			{
				if (query.NextRow ())
				{
					result._id = Id;
					result._createtimestamp = SNDK.Date.DateTimeToTimestamp (query.GetDateTime (qb.ColumnPos ("oprettet")));
					result._updatetimestamp = SNDK.Date.DateTimeToTimestamp (query.GetDateTime (qb.ColumnPos ("sidstrettet")));
					result._debitor = C5.Debitor.Load (query.GetString (qb.ColumnPos ("konto")).Replace (" ", ""));

					success = true;
				}
			}
			
			query.Dispose ();
			query = null;
			qb = null;

			if (!success)
			{
				// Exception: OrderLoadId
				throw new Exception (string.Format (Strings.Exception.OrderLoadId, Id));
			}

			// Load orderlines.
			result._orderlines = OrderLine.List (result);

			// TEMP2
			result._temp2 = false;

			return result;
		}

		public static void Delete (string Id)
		{
			bool success = false;

			if (Order.Load (Id).Invoiced)
			{
				// Exception: OrderLineDeleteInvoiced
				throw new Exception (string.Format (Strings.Exception.OrderDeleteInvoiced, Id));
			}
						
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Delete);
			qb.Table ("ordkart");			
			qb.AddWhere ("nummer like '%"+ Id +"'");
			
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

			// Delete orderlines connected to order that is being deleted.
			foreach (OrderLine line in OrderLine.List (Id))
			{
				OrderLine.Delete (line.Id);
			}
		}

		public static List<Order> List ()
		{
			return List (string.Empty);
		}

		public static List<Order> List (Debitor Debitor)
		{
			return List (Debitor.Id);
		}

		internal static List<Order> List (string DebitorId)
		{
			List<Order> result = new List<Order> ();
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("ordkart");
			qb.Columns ("nummer");

			if (DebitorId != string.Empty)
			{
				qb.AddWhere ("konto like '%"+ DebitorId +"'");
			}
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			if (query.Success)
			{
				while (query.NextRow ())
				{					
					try
					{
						result.Add (Load (query.GetString (qb.ColumnPos ("nummer")).Replace (" ", "")));
					}
					catch
					{					
						// This will catch load exceptions while adding orders to the list. That way corrupt orders will be ommited.
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

