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
			                                          "c5qnax",
			                                          "c5qnax",
			                                          "c5qnax",
			                                          true);	

			bool testvatcode = false;
			bool testcreditpolicy = false;
			bool testdebitor = true;
			bool testinvoice = false;
			bool testorder = false;
			bool testproduct = false;

			Console.WriteLine (C5.Runtime.DBConnection.Connect ());

			if (testvatcode)
			{
				foreach (VatCode vc in VatCode.List ())
				{
					Console.WriteLine (vc.Id +" : "+ vc.Text);
				}
			}

			if (testcreditpolicy)
			{
				foreach (CreditPolicy c in CreditPolicy.List ())
				{
					Console.WriteLine (c.Id +" : "+ c.Text);
				}
			}

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

			if (testdebitor)
			{
				Debitor d1 = new Debitor ();
				d1.Name = "Rasmus Pedersen";
				Console.WriteLine (d1.Id);
//				d1.Save ();

//				Debitor d1 = Debitor.Load ("1200470");


//				Console.WriteLine (d1.VatCode.Text);
//				Console.WriteLine (d1.CreditPolicy.Text);

//				d1.VatCode = VatCode.Load ("U25");
//				d1.Save ();

//				d1.CreditPolicy = CreditPolicy.Load ("14dgNet");
//				d1.Save ();




			}

			if (testorder)
			{
//				Order o1 = Order.Load ("11223");
//				o1.RemoveLine (1);

//				Debitor.Delete ("1200470");

				Debitor d1 = Debitor.Load ("1200470");

				Product d2 = Product.Load ("514");
				Product d3 = Product.Load ("016");

				Order o1 = new Order (d1);

				o1.AddLine (d2.Id, d2.Name, DateTime.Now, DateTime.Now, d2.Unit, 1, d2.Price, d2.Price, "Nota linie #1\nNota linie #2");
				o1.AddLine (d3.Id, d3.Name, DateTime.Now, DateTime.Now, d3.Unit, 1, d3.Price, d3.Price, string.Empty);

				o1.Save ();

				Order o2 = Order.Load (o1.Id);

				Console.WriteLine ("id:"+ o2.Id);
				foreach (OrderLine line in o2.OrderLines)
				{
					Console.WriteLine ("text:"+ line.Text);
					Console.WriteLine ("notes:"+ line.Notes);
					Console.WriteLine ("");
				}


//				for (int i = 0; i < 1000; i++) 
//				{
//					Product d2 = Product.Load ("514");
//					Product d3 = Product.Load ("016");
//					
//					Order o1 = new Order (d1);
//					o1.AddLine (d2.Id, d2.Name, DateTime.Now, DateTime.Now, d2.Unit, 1, d2.Price, d2.Price);
//					o1.AddLine (d3.Id, d3.Name, DateTime.Now, DateTime.Now, d3.Unit, 1, d3.Price, d3.Price);
//					
//					foreach (OrderLine line in o1.OrderLines)
//					{
//						Console.WriteLine (line.Id +" "+ line.Sort +" "+ line.Text);
//					}
//					
//					Console.WriteLine (o1.Id);
//					
//					o1.Save ();
//				}

//				foreach (Order o in Order.List (d1))
//				{
//					Console.WriteLine (o.Id +" "+ o.Invoiced);

//					Order.Delete (o.Id);

//				}

//				Environment.Exit (0);





//				o1.RemoveLine (o1.OrderLines[0].Id);
//

//
//
//
//				o1.Save ();
//
//				Order o2 = Order.Load (o1.Id);
//
//				foreach (OrderLine line in o2.OrderLines)
//				{
//					Console.WriteLine (line.Id +" "+ line.Sort +" "+ line.Text);
//				}

//				Console.WriteLine (o1.Id);
			
//				OrderLine.Delete ("194767");

//				OrderLine o1 = OrderLine.Load ("194767");


//				Console.WriteLine (o1.Text);
//				Console.WriteLine (o1.Amount);
//				Console.WriteLine (o1.Unit);
//				Console.WriteLine (o1.Price);
//				Console.WriteLine (o1.Total);


//				Debitor d1 = Debitor.Load ("1200470");
//				Product d2 = Product.Load ("514");


//				Order o1 = new Order (d1);

//				o1.AddLine (d2.Id, d2.Name, DateTime.Now, DateTime.Now, d2.Unit, 1, d2.Price, d2.Price);

//				o1.Save ();
//				Console.WriteLine ("Order id:"+ o1.Id);

//				C5.Order o1 = C5.Order.Load ("11163");

//				Console.WriteLine (o1.Debitor.Name);

//				11170
//				Order o1 = Order.Load ("11170");

//				Console.WriteLine (o1.Debitor.Name);
//				Console.WriteLine (C5.Helpers.NewOrderLineNo (o1.Id));

//				foreach (Order o in C5.Order.List ())
//				{
//					Console.WriteLine (o.Id);
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