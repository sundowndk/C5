using System;


namespace C5
{
	public class InvoiceLine
	{
		#region Private Fields
		internal decimal _linenumber;
		internal string _partnumber;				
		internal string _unit;
		internal decimal _amount;
		internal decimal _price;
		internal string _text;
		internal decimal _discount;
		internal decimal _total;
		internal string _note;
		internal int _sequenceno;
		#endregion
				
		#region Public Fields
		public decimal LineNumber
		{
			get
			{
				return Math.Round (this._linenumber, 0);
			}
		}
		
		public string PartNumber
		{
			get
			{
				return this._partnumber;
			}
		}
		
		public string Unit
		{
			get
			{
				return this._unit;
			}
		}
		
		public decimal Amount
		{
			get
			{
				return Math.Round (this._amount, 2);
			}
		}
		
		public decimal Price
		{
			get
			{
				return Math.Round (this._price, 2);
			}
		}
		
		public string Text
		{
			get
			{
				return this._text;
			}
		}
		
		public decimal Discount
		{
			get
			{
				return Math.Round (this._discount, 2);
			}
		}
		
		public decimal Total
		{
			get
			{
				return Math.Round (this._total, 2);
			}
		}
		
		public string Note
		{
			get
			{
				return this._note;
			}
		}
		
		public int Sequenceno
		{
			get
			{
				return this._sequenceno;
			}
		}
		#endregion
		
		public InvoiceLine ()
		{
			this._linenumber = 0;
			this._partnumber = string.Empty;
			this._unit = string.Empty;
			this._amount = 0;
			this._price = 0;
			this._text = string.Empty;
			this._discount = 0;
			this._total = 0;
			this._note = string.Empty;
			this._sequenceno = 0;
		}
	}
}

