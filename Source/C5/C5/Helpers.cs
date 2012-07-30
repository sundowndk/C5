using System;

using SNDK.DBI;

namespace C5
{
	public class Helpers
	{
		public static int GetSequenceNumber ()
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
	}
}

