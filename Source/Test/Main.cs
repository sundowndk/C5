using System;
using C5;
using SNDK.DBI;

namespace Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			C5.Runtime.DBConnection = new Connection (	SNDK.Enums.DatabaseConnector.Mssql,
				                                          "172.20.0.54",
				                                          "testdb",
				                                          "testdb",
				                                          "testdb",
				                                          true);	

			bool testdebitor = false;
			bool testinvoice = false;
			bool testorder = true;
			bool testproduct = false;

			if (testproduct)
			{
//				C5.Product p1 = C5.Product.Load ("516");

//				Console.WriteLine (p1.Name);
//				Console.WriteLine (p1.Price);

				foreach (C5.Product p in C5.Product.List ())
				{
					Console.WriteLine (p.Name +" = "+ p.Price);
				}
			}

			if (testorder)
			{
				Debitor d1 = Debitor.Load ("1200470");


				Order o1 = new Order (d1);

				o1.Save ();
				Console.WriteLine ("Order id:"+ o1.Id);

//				C5.Order o1 = C5.Order.Load ("11163");

//				Console.WriteLine (o1.Debitor.Name);

//				foreach (Order o in C5.Order.List ())
//				{
//					Console.WriteLine (o.Debitor.Name);
//				}

//				Console.WriteLine ("GetDebitorId: "+ C5.Helpers.GetDebitorId ());
//
//				C5.Debitor debitor = new C5.Debitor ();
//				debitor.Name = "Rasmus Pedersen";
//				debitor.Address1 = "Agersøvej 303";
//				debitor.PostCode = "4200";
//				debitor.City = "Slagelse";
//				debitor.Save ();


//				C5.Debitor debitor = C5.Debitor.Load ("1200380");
//				
//				Console.WriteLine ("Name: "+ debitor.Name);
//				Console.WriteLine ("Address1: "+ debitor.Address1);
//				Console.WriteLine ("Address2: "+ debitor.Address2);
//				Console.WriteLine ("PostCode: "+ debitor.PostCode);
//				Console.WriteLine ("City: "+ debitor.City);
//				Console.WriteLine ("Country: "+ debitor.Country);
//				Console.WriteLine ("Attention: "+ debitor.Attention);
//				Console.WriteLine ("Phone: "+ debitor.Phone);
//				Console.WriteLine ("Fax: "+ debitor.Fax);
//				Console.WriteLine ("Email: "+ debitor.Email);
//				Console.WriteLine ("Url: "+ debitor.Url);
//				Console.WriteLine ("VatNo: "+ debitor.VatNo);
//
//				Console.WriteLine ("");
//				foreach (C5.Debitor d in C5.Debitor.List ())
//				{
//					Console.WriteLine (d.Name);
//				}
			}

			if (testinvoice)
			{
				C5.Invoice invoice = C5.Invoice.Get (2236);
			
				Console.WriteLine (invoice.AccountId);
				Console.WriteLine (invoice.Name);
				Console.WriteLine (invoice.Address1);
				Console.WriteLine (invoice.Address2);
				Console.WriteLine (invoice.ZipCode);
				Console.WriteLine (invoice.City);
				Console.WriteLine (invoice.Attention);
				Console.WriteLine ("");
				Console.WriteLine (invoice.Date);
				Console.WriteLine (invoice.DueDate);
				Console.WriteLine ("");
				Console.WriteLine (invoice.Type);
				Console.WriteLine ("");
			
				foreach (InvoiceLine line in invoice.Lines)
				{
					Console.WriteLine (line.LineNumber +" "+ line.PartNumber +" "+ line.Text +" "+ line.Note +" "+ line.Amount +" "+ line.Unit +" "+ line.Price +" "+ line.Total);
				}
			
				Console.WriteLine ("");
			
				Console.WriteLine (invoice.SubTotal);
				Console.WriteLine (invoice.VAT);
				Console.WriteLine (invoice.Total);
			}

		}
	}
}