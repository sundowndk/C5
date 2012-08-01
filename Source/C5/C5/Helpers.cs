using System;

using SNDK.DBI;

namespace C5
{
	public class Helpers
	{
		public static int NewSequenceNumber ()
		{
			int result = 0;
			
			Query query = Runtime.DBConnection.Query ("EXEC "+ Runtime.DBConnection.Database +".dbo.sp_xal_seqno 1, 'DAT'");
			if (query.Success)
			{
				if (query.NextRow ())
				{
					result = query.GetInt (0);
				}
			}
			else
			{
				throw new Exception ("COULD NOT GET SEQUENCE NUMBER");
			}
			
			query.Dispose ();
			query = null;
			
			return result;
		}

		public static string NewDebitorId ()
		{
			string result = string.Empty;

			Query query = Runtime.DBConnection.Query ("SELECT TOP 1 konto FROM debkart WHERE LEN(LTRIM(konto))=7 ORDER BY konto DESC");

			if (query.Success)
			{
				if (query.NextRow ())
				{
					result = (int.Parse (query.GetString (0)) + 1).ToString ();
				}
			}
			else
			{
				throw new Exception ("COULD NOT GET DEBITOR ID");
			}
			
			query.Dispose ();
			query = null;
			
			return result;
		}

		public static string NewOrderId ()
		{
			string result = string.Empty;
			
			Query query = Runtime.DBConnection.Query ("SELECT nummer FROM ordkart ORDER BY nummer DESC");
			
			if (query.Success)
			{
				if (query.NextRow ())
				{
					result = (int.Parse (query.GetString (0)) + 1).ToString ();
				}
			}
			else
			{
				throw new Exception ("COULD NOT GET DEBITOR ID");
			}
			
			query.Dispose ();
			query = null;
			
			return result;
		}
	}
}

