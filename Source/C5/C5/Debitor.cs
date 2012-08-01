using System;
using System.Collections.Generic;

using SNDK.DBI;

namespace C5
{
	public class Debitor
	{
		string _id;

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

		string _vatno;

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

		public Debitor ()
		{
			this._id = string.Empty;

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

			this._vatno = string.Empty;
		}

		public void Save ()
		{
			if (this._id == string.Empty)
			{
				int sequencenumber = C5.Helpers.NewSequenceNumber ();
				string id = C5.Helpers.NewDebitorId ();

				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Insert);
				qb.Table ("debkart");
				
				qb.Columns 	
					(
						"dataset",
						"lxbenummer",
						"sidstrettet",
						"lxs",
						"konto",

						"NAVN",
						"ADRESSE1",
						"ADRESSE2",
						"POSTBY",
						"LAND",

						"ATTENTION",
						"TELEFON",
						"TELEFAX",
						"FAKTURAKONTO",
						"GRUPPE",

						"FASTRABAT",
						"GODKENDT",
						"PRISGRUPPE",
						"rabatgruppe",
						"KASSERABAT",
						"BILLEDE",

						"VALUTA",
						"SPROG",
						"BETALING",
						"LEVERING",
						"SPXRRET",

						"SXLGER",
						"MOMS",
						"SLETSTATISTIK",
						"GIRONUMMER",
						"MOMSNUMMER",

						"RENTE",
						"AFDELING",
						"RYKKERKODE",
						"ENGANGSKUNDE",
						"BEHOLDNING",

						"EDIADRESSE",
						"SALDO",
						"SALDO30",
						"SALDO60",
						"SALDO90",

						"SALDO120",
						"SALDOOVER120",
						"FORFALDEN",
						"BEREGNET",
						"SALDOMAX",

						"SALDODKK",
						"SXGENAVN",
						"SLETTRANSPORT",
						"KONTANT",
						"INDBETALMXDE",

						"ORDREGRUPPE",
						"PROJEKTGRUPPE",
						"HANDELSKODE",
						"TRANSKODE",
						"EMAIL",

						"URL",
						"MOBIL",
						"KRAKNR"
						);
				qb.Values
					(
						"DAT",
						sequencenumber,
						string.Empty,
						0,
						id.PadLeft (10, ' '),

						string.Empty,
						string.Empty,
						string.Empty,
						string.Empty,
						string.Empty,

						string.Empty,
						string.Empty,
						string.Empty,
						string.Empty,
						string.Empty,

						0,
						0,
						string.Empty,
						string.Empty,
						string.Empty,
						string.Empty,

						"DKK",
						0,
						string.Empty,
						string.Empty,
						0,

						string.Empty,
						string.Empty,
						0,
						string.Empty,
						string.Empty,

						string.Empty,
						string.Empty,
						0,
						0,
						0,

						string.Empty,
						0,
						0,
						0,
						0,

						0,
						0,
						0,
						"2012-01-01 00:00:00.000",
						0,

						0,
						string.Empty,
						0,
						0,
						string.Empty,

						string.Empty,
						0,
						0,
						string.Empty,
						string.Empty,

						string.Empty,
						string.Empty,
						string.Empty
					);

				Query query = Runtime.DBConnection.Query (qb.QueryString);

				if (query.AffectedRows == 0) 
				{
					throw new Exception ("COULD NOT CREATE NEW DEBITOR");
				}
							
				query.Dispose ();
				query = null;
				qb = null;

				this._id = id;
			}

			{
				QueryBuilder qb = new QueryBuilder (QueryBuilderType.Update);
				qb.Table ("debkart");
				
				qb.Columns 	
					(
						"NAVN",
						"ADRESSE1",
						"ADRESSE2",
						"POSTBY",
						"LAND"
					);

				qb.Values
					(
						this._name,
						this._address1,
						this._address2,
						this._postcode +" "+ this._city,
						this._country
					);

				qb.AddWhere ("konto like '%"+ this._id +"'");

				Query query = Runtime.DBConnection.Query (qb.QueryString);

				if (query.AffectedRows == 0) 
				{
					throw new Exception ("COULD NOT CREATE NEW DEBITOR");
				}
				
				query.Dispose ();
				query = null;
				qb = null;
			}
		}

		public static Debitor Load (string Id)
		{
			Debitor result = new Debitor ();

			QueryBuilder qb = new QueryBuilder (QueryBuilderType.Select);
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
					"momsnummer"
				);
			
			qb.AddWhere ("konto like '%"+ Id +"'");
			
			Query query = Runtime.DBConnection.Query (qb.QueryString);
			
			if (query.Success)
			{
				if (query.NextRow ())
				{
					result._id = Id;
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
					{}

					result._country = query.GetString (qb.ColumnPos ("land"));

					result._attention = query.GetString (qb.ColumnPos ("attention"));

					result._phone = query.GetString (qb.ColumnPos ("telefon"));
					result._fax = query.GetString (qb.ColumnPos ("telefax"));
					result._email = query.GetString (qb.ColumnPos ("email"));
					result._url = query.GetString (qb.ColumnPos ("url"));

					result._vatno = query.GetString (qb.ColumnPos ("momsnummer"));
				}
			}
			
			query.Dispose ();
			query = null;
			qb = null;

			return result;
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

