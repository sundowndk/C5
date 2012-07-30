using System;
<<<<<<< HEAD

using C5;

using SNDK.DBI;

=======
using System.Xml;

using SNDK;
using SNDK.DBI;

using SorentoLib;


>>>>>>> eb7ef0d4301d8935ba066019459bef4fac22cf6a
namespace Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
<<<<<<< HEAD
			C5.Runtime.DBConnection = new Connection (	SNDK.Enums.DatabaseConnector.Mssql,
			                                          "172.20.0.54",
			                                          "testdb",
			                                          "testdb",
			                                          "testdb",
			                                          true);	
			
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
			
			
			
			
=======
			SorentoLib.Services.Database.Connection = new Connection (SNDK.Enums.DatabaseConnector.Mysql,
			                                                          "localhost",
			                                                          "sorentotest.sundown.dk",
			                                                          "sorentotest",
			                                                          "qwerty",
			                                                          true);
			
			SorentoLib.Services.Database.Prefix = "didiustest_";

			if (SorentoLib.Services.Database.Connection.Connect ())
			{
				Console.WriteLine ("Connected to database.");

				bool testcustomergroup = false;
				bool testcustomer = false;

				bool testcase = false;
				bool testitem = true;

				bool testauction = false;

				bool testbid = false;

				#region CUSTOMERGROUP
				if (testcustomergroup)
				{
					Console.WriteLine ("Testing Didius.CustomerGroup\n");

					// SAVE
					Console.WriteLine ("\tSave\n");
					Didius.CustomerGroup cg1 = new Didius.CustomerGroup ();
					cg1.Name = "Name";
					
					cg1.Save ();

					// LOAD
					Console.WriteLine ("\tLoad\n");
					Didius.CustomerGroup cg2 = Didius.CustomerGroup.Load (cg1.Id);

					Console.WriteLine ("\t\tName: "+ cg2.Name);

					// LIST
					Console.WriteLine ("\r\tList\n");
					foreach (Didius.CustomerGroup cg in Didius.CustomerGroup.List ())
					{
						Console.WriteLine ("\t\t"+ cg.Name);
					}
						
					// TOXMLDOCUMENT
					Console.WriteLine ("\r\tToXmlDocument\n");
					XmlDocument cg1xml = cg1.ToXmlDocument ();
					Console.WriteLine ("\t\t"+ cg1xml.InnerXml.ToString ());

					// FROMXMLDOCUMENT
					Console.WriteLine ("\r\tFromXmlDocument\n");
					Didius.CustomerGroup cg3 = Didius.CustomerGroup.FromXmlDocument (cg1xml);
					Console.WriteLine ("\t\tName: "+ cg3.Name);

					// DELETE
					Console.WriteLine ("\r\tDelete\n");
					Didius.CustomerGroup.Delete (cg1.Id);

					// CLEANUP
					Console.WriteLine ("\tCleanup");
				}
				#endregion

				#region CUSTOMER
				if (testcustomer)
				{
					Console.WriteLine ("\rTesting Didius.Customer\n\r");

					// SAVE
					Console.WriteLine ("\tSave\n");
					Didius.Customer c1 = new Didius.Customer ();

					Didius.CustomerGroup cg1 = new Didius.CustomerGroup ();
					cg1.Name = "Group1";
					cg1.Save ();

					Didius.CustomerGroup cg2 = new Didius.CustomerGroup ();
					cg2.Name = "Group2";
					cg2.Save ();

					c1.Groups.Add (cg1);
					c1.Groups.Add (cg2);

					c1.Name = "Name";
					c1.Address1 = "Address1";
					c1.Address2 = "Address2";
					c1.PostCode = "PostCode";
					c1.City = "City";
					c1.Country = "Country";
					c1.Att = "Att";
					c1.Email = "Email";
					c1.PhoneNumbers.Add ("PhoneNumber");
					c1.PhoneNumbers.Add ("PhoneNumber");
					c1.Vat = true;
					c1.VatNo = "VatNo";
					c1.BankName = "BankName";
					c1.BankRegistrationNo = "BankRegistrationNo";
					c1.BankAccountNo = "BankAccountNo";
					c1.Notes = "Notes";
					c1.Status = Didius.Enums.CustomerStatus.Enabled;

					SorentoLib.User u1 = new User ("TESTUSER", "TEST@TEST.TEST");
					u1.Save ();

					c1.User = u1;

					c1.Save ();

					// LOAD
					Console.WriteLine ("\tLoad\n");
					Didius.Customer c2 = Didius.Customer.Load (c1.Id);

					Console.WriteLine ("\t\tNo: "+ c2.No);

					Console.WriteLine ("\t\tGroups:");
					foreach (Didius.CustomerGroup cg in c2.Groups)
					{
						Console.WriteLine ("\t\t\t"+ cg.Name);
					}

					Console.WriteLine ("\r\t\tName: "+ c2.Name);
					Console.WriteLine ("\t\tAddress1: "+ c2.Address1);
					Console.WriteLine ("\t\tAddress2: "+ c2.Address2);
					Console.WriteLine ("\t\tPostcode: "+ c2.PostCode);
					Console.WriteLine ("\t\tCity: "+ c2.City);
					Console.WriteLine ("\t\tCountry: "+ c2.Country);
					Console.WriteLine ("\t\tAtt.: "+ c2.Att);
					Console.WriteLine ("\t\tEmail: "+ c2.Email);

					Console.WriteLine ("\r\t\tPhoneNumbers:");
					foreach (string phonenumber in c2.PhoneNumbers)
					{
						Console.WriteLine ("\t\t\t"+ phonenumber);
					}

					Console.WriteLine ("\r\t\tVat: "+ c2.Vat);
					Console.WriteLine ("\t\tVatNo.: "+ c2.VatNo);
					Console.WriteLine ("\t\tBankName.: "+ c2.BankName);
					Console.WriteLine ("\t\tBankRegistrationNo.: "+ c2.BankRegistrationNo);
					Console.WriteLine ("\t\tBankAccountNo.: "+ c2.BankAccountNo);
					Console.WriteLine ("\t\tNotes: "+ c2.Notes);
					Console.WriteLine ("\t\tStatus: "+ c2.Status);

					Console.WriteLine ("\t\tUser: "+ c2.User.Username);
					Console.WriteLine ("\t\tUserStatus: "+ c2.User.Status);

					// LIST
					Console.WriteLine ("\r\tList\n");
					foreach (Didius.Customer c in Didius.Customer.List ())
					{
						Console.WriteLine ("\t\t"+ c1.Name);
					}

					// TOXMLDOCUMENT
					Console.WriteLine ("\r\tToXmlDocument\n");
					XmlDocument c1xml = c1.ToXmlDocument ();
					Console.WriteLine ("\t\t"+ c1xml.InnerXml.ToString ());
					
					// FROMXMLDOCUMENT
					Console.WriteLine ("\r\tFromXmlDocument\n");
					Didius.Customer c3 = Didius.Customer.FromXmlDocument (c1xml);
					Console.WriteLine ("\t\tNo: "+ c3.No);
					
					Console.WriteLine ("\t\tGroups:");
					foreach (Didius.CustomerGroup cg in c3.Groups)
					{
						Console.WriteLine ("\t\t\t"+ cg.Name);
					}
					
					Console.WriteLine ("\r\t\tName: "+ c3.Name);
					Console.WriteLine ("\t\tAddress1: "+ c3.Address1);
					Console.WriteLine ("\t\tAddress2: "+ c3.Address2);
					Console.WriteLine ("\t\tPostcode: "+ c3.PostCode);
					Console.WriteLine ("\t\tCity: "+ c3.City);
					Console.WriteLine ("\t\tCountry: "+ c3.Country);
					Console.WriteLine ("\t\tAtt.: "+ c3.Att);
					Console.WriteLine ("\t\tEmail: "+ c3.Email);
					
					Console.WriteLine ("\r\t\tPhoneNumbers:");
					foreach (string phonenumber in c3.PhoneNumbers)
					{
						Console.WriteLine ("\t\t\t"+ phonenumber);
					}
					
					Console.WriteLine ("\r\t\tVat: "+ c3.Vat);
					Console.WriteLine ("\t\tVatNo.: "+ c3.VatNo);
					Console.WriteLine ("\t\tBankName.: "+ c3.BankName);
					Console.WriteLine ("\t\tBankRegistrationNo.: "+ c3.BankRegistrationNo);
					Console.WriteLine ("\t\tBankAccountNo.: "+ c3.BankAccountNo);
					Console.WriteLine ("\t\tNotes: "+ c3.Notes);
					Console.WriteLine ("\t\tStatus: "+ c3.Status);

					Console.WriteLine ("\t\tUser: "+ c2.User.Username);
					Console.WriteLine ("\t\tUserStatus: "+ c2.User.Status);

					// DELETE
					Console.WriteLine ("\r\tDelete");
					Didius.Customer.Delete (c1.Id);

					// CLEANUP
					Console.WriteLine ("\r\tCleanup");
					Didius.CustomerGroup.Delete (cg1.Id);
					Didius.CustomerGroup.Delete (cg2.Id);
					SorentoLib.User.Delete (u1.Id);
				}
				#endregion			
			
				#region CASE
				if (testcase)
				{
					Console.WriteLine ("Testing Didius.Case\n");
					
					// SAVE
					Console.WriteLine ("\tSave\n");

					Didius.Customer _c = new Didius.Customer ();
					_c.Name = "TEST CUSTOMER";
					_c.Save ();

					Didius.Case c1 = new Didius.Case (_c);
					
					c1.Save ();
					
					// LOAD
					Console.WriteLine ("\tLoad\n");
					Didius.Case c2 = Didius.Case.Load (c1.Id);
					
					Console.WriteLine ("\t\tNo: "+ c2.No);
					Console.WriteLine ("\t\tCustomerId: "+ c2.CustomerId);
					
					// LIST
					Console.WriteLine ("\r\tList\n");
					foreach (Didius.Case c in Didius.Case.List ())
					{
						Console.WriteLine ("\t\t"+ c.No);
					}

					// LIST CUSTOMER
					Console.WriteLine ("\r\tList with Customer\n");
					foreach (Didius.Case c in Didius.Case.List (_c))
					{
						Console.WriteLine ("\t\t"+ c.No);
					}
					
					// TOXMLDOCUMENT
					Console.WriteLine ("\r\tToXmlDocument\n");
					XmlDocument c1xml = c1.ToXmlDocument ();
					Console.WriteLine ("\t\t"+ c1xml.InnerXml.ToString ());
					
					// FROMXMLDOCUMENT
					Console.WriteLine ("\r\tFromXmlDocument\n");
					Didius.Case c3 = Didius.Case.FromXmlDocument (c1xml);
					Console.WriteLine ("\t\tNo: "+ c3.No);
					Console.WriteLine ("\t\tCustomerId: "+ c3.CustomerId);
					
					// DELETE
					Console.WriteLine ("\r\tDelete\n");
					Didius.Case.Delete (c1.Id);
					
					// CLEANUP
					Console.WriteLine ("\tCleanup");

					Didius.Customer.Delete (_c.Id);				
				}
				#endregion

				#region ITEM
				if (testitem)
				{
					Console.WriteLine ("Testing Didius.Item\n");
					
					// SAVE
					Console.WriteLine ("\tSave\n");
					
					Didius.Customer d1 = new Didius.Customer ();
					d1.Name = "TEST CUSTOMER";
					d1.Save ();

					Didius.Case d2 = new Didius.Case (d1);
					d2.Save ();
					
					Didius.Item t1 = new Didius.Item (d2);					
					t1.Title = "TITLE";
					t1.Description = "DESCRIPTION";
					t1.Fields.Add ("FIELD1", "VALUE1");
					t1.Fields.Add ("FIELD2", "VALUE2");
					t1.Fields.Add ("FIELD3", "VALUE3");
					t1.Fields.Add ("FIELD4", "VALUE4");
					t1.Save ();
					
					// LOAD
					Console.WriteLine ("\tLoad\n");
					Didius.Item t2 = Didius.Item.Load (t1.Id);
					Console.WriteLine ("\t\tCaseId: "+ t2.CaseId);
					Console.WriteLine ("\t\tTitle: "+ t2.Title);
					Console.WriteLine ("\t\tDescription: "+ t2.Description);

					foreach (string key in t2.Fields.Keys)
					{
						Console.WriteLine ("\t\t\t"+ key +": "+ t2.Fields[key]);
					}
										
					// LIST
					Console.WriteLine ("\r\tList\n");
					foreach (Didius.Item t3 in Didius.Item.List ())
					{
						Console.WriteLine ("\t\tTitle: "+ t3.Title);
					}

					// TOXMLDOCUMENT
					Console.WriteLine ("\r\tToXmlDocument\n");
					XmlDocument t1xml = t1.ToXmlDocument ();
					Console.WriteLine ("\t\t"+ t1xml.InnerXml.ToString ());
					
					// FROMXMLDOCUMENT
					Console.WriteLine ("\r\tFromXmlDocument\n");
					Didius.Item t4 = Didius.Item.FromXmlDocument (t1xml);
					Console.WriteLine ("\t\tId: "+ t4.Id);
					Console.WriteLine ("\t\tTitle: "+ t4.Title);
					Console.WriteLine ("\t\tDescription: "+ t4.Description);

					foreach (string key in t2.Fields.Keys)
					{
						Console.WriteLine ("\t\t\t"+ key +": "+ t2.Fields[key]);
					}

					// DELETE
					Console.WriteLine ("\r\tDelete\n");
					Didius.Item.Delete (t1);
					
					// CLEANUP
					Console.WriteLine ("\tCleanup");

					Didius.Case.Delete (d2.Id);
					Didius.Customer.Delete (d1.Id);				
				}
				#endregion

				#region AUCTION
				if (testauction)
				{
					Console.WriteLine ("Testing Didius.Auction\n");
					
					// SAVE
					Console.WriteLine ("\tSave\n");
					
					Didius.Auction a1 = new Didius.Auction ();					
					a1.Save ();
										
					// LOAD
					Console.WriteLine ("\tLoad\n");
					Didius.Auction a2 = Didius.Auction.Load (a1.Id);

					Console.WriteLine ("\t\tId: "+ a2.Id);
					
					// LIST
					Console.WriteLine ("\r\tList\n");
					foreach (Didius.Auction a in Didius.Auction.List ())
					{
						Console.WriteLine ("\t\tId: "+ a.Id);
					}
										
					// TOXMLDOCUMENT
					Console.WriteLine ("\r\tToXmlDocument\n");
					XmlDocument a1xml = a1.ToXmlDocument ();
					Console.WriteLine ("\t\t"+ a1xml.InnerXml.ToString ());
					
					// FROMXMLDOCUMENT
					Console.WriteLine ("\r\tFromXmlDocument\n");
					Didius.Auction a3 = Didius.Auction.FromXmlDocument (a1xml);
					Console.WriteLine ("\t\tId: "+ a3.Id);
					
					// DELETE
					Console.WriteLine ("\r\tDelete\n");
					Didius.Auction.Delete (a1.Id);
					
					// CLEANUP
					Console.WriteLine ("\tCleanup");
				}
				#endregion

				#region BID
				if (testbid)
				{
					Console.WriteLine ("Testing Didius.Bid\n");
					
					// SAVE
					Console.WriteLine ("\tSave\n");

					Didius.Customer d1 = new Didius.Customer ();
					d1.Name = "TEST CUSTOMER";
					d1.Save ();

					Didius.Case d2 = new Didius.Case (d1);
					d2.Save ();

					Didius.Item d3 = new Didius.Item (d2);
					d3.Save ();
								
					Didius.Bid b1 = new Didius.Bid (d1, d3, 100);
					b1.Save ();
					
					// LOAD
					Console.WriteLine ("\tLoad\n");
					Didius.Bid b2 = Didius.Bid.Load (b1.Id);					
					Console.WriteLine ("\t\tId: "+ b2.Id);
					Console.WriteLine ("\t\tAmount: "+ b2.Amount);
					
					// LIST
					Console.WriteLine ("\r\tList\n");
					foreach (Didius.Bid b in Didius.Bid.List ())
					{
						Console.WriteLine ("\t\tId: "+ b.Id);
					}
					
					// TOXMLDOCUMENT
					Console.WriteLine ("\r\tToXmlDocument\n");
					XmlDocument b1xml = b1.ToXmlDocument ();
					Console.WriteLine ("\t\t"+ b1xml.InnerXml.ToString ());
					
					// FROMXMLDOCUMENT
					Console.WriteLine ("\r\tFromXmlDocument\n");
					Didius.Bid b3 = Didius.Bid.FromXmlDocument (b1xml);
					Console.WriteLine ("\t\tId: "+ b3.Id);
					Console.WriteLine ("\t\tAmount: "+ b3.Amount);
					
					// DELETE
					Console.WriteLine ("\r\tDelete\n");
					Didius.Bid.Delete (b1.Id);
					
					// CLEANUP
					Console.WriteLine ("\tCleanup");

					Didius.Item.Delete (d3.Id);
					Didius.Case.Delete (d2.Id);
					Didius.Customer.Delete (d1.Id);
				}
				#endregion
			}
			else
			{
				Console.WriteLine ("Could not connect to database.");
			}
>>>>>>> eb7ef0d4301d8935ba066019459bef4fac22cf6a
		}
	}
}