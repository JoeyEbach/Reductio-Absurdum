using System;
public class Product
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public bool Available { get; set; }
		public int ProductTypeId { get; set; }
		public DateTime DateStocked { get; set; }
		public int DaysOnShelf
		{
			get
			{
				TimeSpan timeOnShelf = DateTime.Now - DateStocked;
				return timeOnShelf.Days;
			}
		}
	}


