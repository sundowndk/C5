using System;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class Debitor
	{
		#region Private Fields
		string _id;

		int _updatetimestamp;

		string _name;

		string _address1;
		string _address2;
		string _postcode;
		string _city;
		string _country;

		string _attention;

		string _phone;
		string _fax;
		string _email;
		string _url;

		string _creditpolicy;

		string _vatno;
		string _vatcode;
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

		public int UpdateTimestamp
		{
			get
			{
				return this._updatetimestamp;
			}
		}

		public string Name
		{
			get
			{
				return this._name;
			}

			set
			{
				this._name = value;
			}
		}

		public string Address1
		{
			get
			{
				return this._address1;
			}
			
			set
			{
				this._address1 = value;
			}
		}

		public string Address2
		{
			get
			{
				return this._address2;
			}
			
			set
			{
				this._address2 = value;
			}
		}

		public string PostCode
		{
			get
			{
				return this._postcode;
			}
			
			set
			{
				this._postcode = value;
			}
		}

		public string City
		{
			get
			{
				return this._city;
			}
			
			set
			{
				this._city = value;
			}
		}

		public string Country
		{
			get
			{
				return this._country;
			}
			
			set
			{
				this._country = value;
			}
		}


		public string Attention
		{
			get
			{
				return this._attention;
			}
			
			set
			{
				this._attention = value;
			}
		}

		public string Phone
		{
			get
			{
				return this._phone;
			}
			
			set
			{
				this._phone = value;
			}
		}

		public string Fax
		{
			get
			{
				return this._fax;
			}
			
			set
			{
				this._fax = value;
			}
		}

		public string Email
		{
			get
			{
				return this._email;
			}
			
			set
			{
				this._email = value;
			}
		}

		public string Url
		{
			get
			{
				return this._url;
			}
			
			set
			{
				this._url = value;
			}
		}

		public string CreditPolicy
		{
			get
			{
				return this._creditpolicy;
			}

			set
			{
				this._creditpolicy = value;
			}
		}

		public string VatNo
		{
			get
			{
				return this._vatno;
			}
			
			set
			{
				this._vatno = value;
			}
		}

		public string VatCode
		{
			get
			{
				return this._vatcode;
			}

			set
			{
				this._vatcode = value;
			}
		}
		#endregion

		#region Constructor
		public Debitor ()
		{
			this._id = C5.Helpers.NewDebitorId ();

			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();

			this._name = string.Empty;

			this._address1 = string.Empty;
			this._address2 = string.Empty;
			this._postcode = string.Empty;
			this._city = string.Empty;
			this._country = string.Empty;

			this._attention = string.Empty;

			this._phone = string.Empty;
			this._fax = string.Empty;
			this._email = string.Empty;
			this._url = string.Empty;

			this._creditpolicy = string.Empty;

			this._vatno = string.Empty;
			this._vatcode = "U25";
		}
		#endregion

		#region Public Methods
		public void Save ()
		{
			// If Debitor has not yet been saved, we need to insert a new record.
			// TEMP1
			if (this._temp1)
			{
				int sequencenumber = C5.Helpers.NewSequenceNumber ();

				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Insert);
				qb.Table ("debkart");
				
				qb.Columns 	
					(
						"dataset",
						"lxbenummer",
						"sidstrettet",
						"lxs",
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
						"godkendt",
						"prisgruppe",
						"rabatgruppe",
						"kasserabat",
						"billede",
						"valuta",
						"sprog",
						"betaling",
						"levering",
						"spxrret",
						"sxlger",
						"moms",
						"sletstatistik",
						"gironummer",
						"momsnummer",
						"rente",
						"afdeling",
						"rykkerkode",
						"engangskunde",
						"beholdning",
						"ediadresse",
						"saldo",
						"saldo30",
						"saldo60",
						"saldo90",
						"saldo120",
						"saldoover120",
						"forfalden",
						"beregnet",
						"saldomax",
						"saldoddk",
						"sxgenavn",
						"slettransport",
						"kontakt",
						"indbetalmxde",
						"ordregruppe",
						"projektgruppe",
						"handelskode",
						"transkode",
						"email",
						"url",
						"mobil",
						"kraknr"
						);
				qb.Values
					(
						"DAT", // dataset
						sequencenumber, // lxbenummer
						String.Format ("{0:yyyy-MM-dd} 00:00:00.000", SNDK.Date.TimestampToDateTime (this._updatetimestamp)), // sidstrettet
						0, // lxs
						this._id.PadLeft (10, ' '), // konto
						this._name, // navn
						this._address1, // adresse1
						this._address2, // address2
						this._postcode +" "+ this._city, // postby
						this._country, // land
						this._attention, // attention
						this._phone, // telefon
						this._fax, // fax
 						string.Empty, // fakturakonto
						string.Empty, // gruppe
						0, // fastrabat
						0, // godkendt
						string.Empty, // prisgruppe
						string.Empty, // rabatgruppe
						string.Empty, // kasserabat
						string.Empty, // billede
						"DKK", // valuta
						0, // sprog
						this._creditpolicy, // betaling
						string.Empty, // levering
						0, // spxrret
						string.Empty, // sxlger
						this._vatcode, // moms
						0, // sletstatistik
						string.Empty, // gironummer
						this._vatno, // momsnummer
						string.Empty, // rente
						string.Empty, // afdeling
						0, // rykkerkode
						0, // engangskunde
						0, // beholdning
						string.Empty, // ediadresse
						0, // saldo
						0, // saldo30
						0, // saldo60
						0, // saldo90
						0, // saldo120
						0, // saldoover120
						0, // forfalden
						"1900-01-01 00:00:00.000", // beregnet
						0, // saldomax
						0, // saldoddk
						string.Empty, // sxgenavn
						0, // slettransport
						0, // kontakt
						string.Empty, // indbetalmxde
						string.Empty, // ordregruppe
						0, // projektgruppe
						0, // handelskode
						string.Empty, // transkode
						this._email, // email
						string.Empty, // url
						string.Empty, // mobil
						string.Empty // kraknr
					);

				Query query = Runtime.DBConnection.Query (qb.QueryString);

				if (query.AffectedRows == 0) 
				{
					// Exception: DebitorSave
					throw new Exception (string.Format (Strings.Exception.DebitorSave, this._id));
				}
							
				query.Dispose ();
				query = null;
				qb = null;

				// TEMP1
				this._temp1 = false;
			}
			// If OrderLine has allready been saved before we only need to update the record.
			else
			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Update);
				qb.Table ("debkart");
				
				qb.Columns 	
					(
						"navn",
						"adresse1",
						"adresse2",
						"postby",
						"land",
						"attention",
						"telefon",
						"telefax",
						"email",
						"url",
						"betaling",
						"momsnummer",
						"moms"
					);

				qb.Values
					(
						this._name, // navn
						this._address1, // adresse1
						this._address2, // address2
						this._postcode +" "+ this._city, // postby
						this._country, // land
						this._attention, // attention
						this._phone, // telefon
						this._fax, // fax
						this._email, // email
						this._url, // url
						this._creditpolicy, // betaling
						this._vatno, // momsnummer
						this._vatcode // moms
					);

				qb.AddWhere ("konto like '%"+ this._id +"'");

				Query query = Runtime.DBConnection.Query (qb.QueryString);

				if (query.AffectedRows == 0) 
				{
					// Exception: DebitorSave
					throw new Exception (string.Format (Strings.Exception.DebitorSave, this._id));
				}
				
				query.Dispose ();
				query = null;
				qb = null;
			}
		}
		#endregion

		#region Public Static Methods
		public static Debitor Load (string Id)
		{
			bool success = true;
			Debitor result = new Debitor ();

			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("debkart");
			qb.Columns 
				(
					"sidstrettet",
					"navn",
					"adresse1",
					"adresse2",
					"postby",
					"land",
					"attention",
					"telefon",
					"telefax",
					"email",
					"url",
					"betaling",
					"momsnummer",
					"moms"
				);
			
			qb.AddWhere ("konto like '%"+ Id +"'");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			
			if (query.Success)
			{
				if (query.NextRow ())
				{
					result._id = Id;
					result._updatetimestamp = SNDK.Date.DateTimeToTimestamp (query.GetDateTime (qb.ColumnPos ("sidstrettet")));
					result._name = query.GetString (qb.ColumnPos ("navn"));
					result._address1 = query.GetString (qb.ColumnPos ("adresse1"));
					result._address2 = query.GetString (qb.ColumnPos ("adresse2"));

					try
					{
						string postby = query.GetString (qb.ColumnPos ("postby"));
						result._postcode = postby.Split (" ".ToCharArray (), StringSplitOptions.RemoveEmptyEntries)[0];
						result._city = postby.Split (" ".ToCharArray (), StringSplitOptions.RemoveEmptyEntries)[1];
					}
					catch
					{
						// This will catch empty postcode or city.
					}

					result._country = query.GetString (qb.ColumnPos ("land"));

					result._attention = query.GetString (qb.ColumnPos ("attention"));

					result._phone = query.GetString (qb.ColumnPos ("telefon"));
					result._fax = query.GetString (qb.ColumnPos ("telefax"));
					result._email = query.GetString (qb.ColumnPos ("email"));
					result._url = query.GetString (qb.ColumnPos ("url"));

					result._creditpolicy = query.GetString (qb.ColumnPos ("betaling"));

					result._vatno = query.GetString (qb.ColumnPos ("momsnummer"));
					result._vatcode = query.GetString (qb.ColumnPos ("moms"));
				}
			}
			
			query.Dispose ();
			query = null;
			qb = null;

			if (!success)
			{
				// Exception: DebitorLoadId
				throw new Exception (string.Format (Strings.Exception.DebitorLoadId, Id));
			}

			// TEMP1
			result._temp1 = false;

			return result;
		}

		public static void Delete (string Id)
		{
			bool success = false;

			try
			{
				foreach (Order order in Order.List (Debitor.Load (Id)))
				{
					Order.Delete (order.Id);
				}
			}
			catch
			{
				// Exception: DebitorDeleteId
				throw new Exception (string.Format (Strings.Exception.DebitorDeleteOrder, Id));
			}
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Delete);
			qb.Table ("debkart");
			
			qb.AddWhere ("konto like '%"+ Id +"'");
			
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
				// Exception: DebitorDeleteId
				throw new Exception (string.Format (Strings.Exception.DebitorDeleteId, Id));
			}
		}

		public static List<Debitor> List ()
		{
			List<Debitor> result = new List<Debitor> ();
			
			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
			qb.Table ("debkart");
			qb.Columns ("konto");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			if (query.Success)
			{
				while (query.NextRow ())
				{					
					try
					{
						result.Add (Load (query.GetString (qb.ColumnPos ("konto")).Replace (" ", "")));
					}
					catch
					{	
						// This will catch load exceptions while adding debitors to the list. That way corrupt debitors will be ommited.
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

