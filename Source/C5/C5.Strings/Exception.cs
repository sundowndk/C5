using System;

namespace C5.Strings
{
	public class Exception
	{
		public static string DebitorSave = "Could not save Debitor with id: {0}";
		public static string DebitorLoadId = "Could not load Debitor with id: {0}";
		public static string DebitorDeleteId = "Could not delete Debitor with id: {0}";
		public static string DebitorDeleteOrder = "Could not delete Debitor with id: {0}, since it has invoided orderes.";

		public static string OrderSave = "Could not save Order with id: {0}";
		public static string OrderLoadId = "Could not load Order with id: {0}";
		public static string OrderDeleteId = "Could not delete Order with id: {0}";
		public static string OrderDeleteInvoiced = "Could not delete Order with id: {0}, since its been invoiced.";

		public static string OrderLineSave = "Could not save OrderLine with id: {0}";
		public static string OrderLineLoadId = "Could not load OrderLine with id: {0}";
		public static string OrderLineDeleteId = "Could not delete OrderLine with id: {0}";
	}
}
