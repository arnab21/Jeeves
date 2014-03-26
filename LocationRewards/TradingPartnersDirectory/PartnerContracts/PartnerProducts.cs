using System;

namespace TradingPartnersDirectory
{
	public class PartnerProductsId
	{
		public string __invalid_name__oid { get; set; }
	}

	public class PartnerProducts
	{
		public string ProductDescription { get; set; }
		public string ProductChannel { get; set; }
		public double Price { get; set; }
		public string ProductName { get; set; }
		public string ProductImage { get; set; }
		public string Currency { get; set; }
		public int PartnerId { get; set; }
		public int RedemptionPoints { get; set; }
		public PartnerProductsId _id { get; set; }
		public int ProductId { get; set; }
	}
}

