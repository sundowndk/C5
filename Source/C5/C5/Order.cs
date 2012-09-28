using System;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class Order
	{
		string _id;
		C5.Debitor _debitor;

		List<OrderLine> _orderlines;

		public string Id
		{
			get
			{
				return this._id;
			}
		}

		public Debitor Debitor
		{
			get
			{
				return this._debitor;
			}
		}

		public List<OrderLine> OrderLines
		{
			get
			{
				return this._orderlines;
			}
		}

		public Order (Debitor Debitor)
		{
			this._id = string.Empty;
			this._debitor = Debitor;
		}

		public Order ()
		{
			this._id = string.Empty;
			this._debitor = null;
		}

		public void Save ()
		{
			if (this._id == string.Empty)
			{
				int sequencenumber = C5.Helpers.NewSequenceNumber ();
				this._id = C5.Helpers.NewOrderId ();

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
						"1900-01-01 00:00:00.000", // sidstrettet
						0, // lxs
						this._id.PadLeft (10, ' '), // nummer
						"Webordre #"+ this._id, // sxgenavn
						"1900-01-01 00:00:00.000", // oprettet
						"1900-01-01 00:00:00.000", // leveres 
						string.Empty, // konto
						string.Empty, // navn
						string.Empty, // adresse1
						string.Empty, // adresse2
						string.Empty, // postby
						string.Empty, // land
						string.Empty, // attention
						string.Empty, // telefon
						string.Empty, // telefax
						string.Empty, // fakturakonto
						string.Empty, // gruppe
						0, // fastrabat
						"Salg", // prisgruppe
						string.Empty, // rabatgruppe
						string.Empty, // kasserabat
						"DKK", // valuta
						0, // sprog
						string.Empty, // betaling 
						string.Empty, // levering
						0, // spxrret
						string.Empty, //sxlger
						string.Empty, // moms
						0, // beholdning
						string.Empty, // afdeling
						string.Empty, // gironummer
						string.Empty, // momsnummer
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
						0, // gebyrfm
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
						"1900-01-01 00:00:00.000", // fakturafxlgeseddeldato
						0, // kontant
						0, // listekode
						0, // linierabat
						0, // varebelxb
						0, // momsgrundlag
						string.Empty, // handelskode
						string.Empty, // transkode
						string.Empty, // enummer
						string.Empty, // email
						string.Empty, // levmail
						string.Empty // betalingstid
					);

				Query query = Runtime.DBConnection.Query (qb.QueryString);
				
				if (query.AffectedRows == 0) 
				{
					throw new Exception ("COULD NOT CREATE NEW ORDER");
				}
				
				query.Dispose ();
				query = null;
				qb = null;
			}

			{
				int gebyrfm = 0;
				if (this._debitor.Url == string.Empty)
				{
					gebyrfm = 30;
				}

				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Update);
				qb.Table ("ordkart");
				
				qb.Columns 	
					(
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
						this._debitor.Id.PadLeft (10, ' '),
						this._debitor.Name,
						this._debitor.Address1,
						this._debitor.Address2,
						this._debitor.PostCode +" "+ this._debitor.City,
						this._debitor.Country,
						this._debitor.Attention,
						this._debitor.Phone,
						this._debitor.Fax,
						this._debitor.Email,
						this._debitor.Id.PadLeft (10, ' '),
						this._debitor.CreditPolicy,
						this._debitor.VatCode,
						this._debitor.VatNo,
						gebyrfm
					);
				
				qb.AddWhere ("nummer like '%"+ this._id +"'");
				
				Query query = Runtime.DBConnection.Query (qb.QueryString);
				
				if (query.AffectedRows == 0) 
				{
					throw new Exception ("COULD NOT UPDATE NEW ORDER");
				}
				
				query.Dispose ();
				query = null;
				qb = null;
			}
		}

		public static Order Load (string Id)
		{
			Order result = new Order ();
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("ordkart");
			qb.Columns 
				(
					"konto"
				);
			
			qb.AddWhere ("nummer like '%"+ Id +"'");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			
			if (query.Success)
			{
				if (query.NextRow ())
				{
					result._id = Id;
					result._debitor = C5.Debitor.Load (query.GetString (qb.ColumnPos ("konto")).Replace (" ", ""));
				}
			}
			
			query.Dispose ();
			query = null;
			qb = null;
			
			return result;
		}

		public static List<Order> List ()
		{
			List<Order> result = new List<Order> ();
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("ordkart");
			qb.Columns ("nummer");
			
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

