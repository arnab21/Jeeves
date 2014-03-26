using System;
using System.Collections;
using System.Collections.Generic;

namespace TradingPartnersDirectory
{
	public class Coord
	{
		public double lat { get; set; }
		public double @long { get; set; }
	}

	public class Id
	{
		public string __invalid_name__oid { get; set; }
	}

	public class TradingPartner
	{
		public Coord Coord { get; set; }
		public string OpenTime { get; set; }
		public string PartnerLogo { get; set; }
		public double Long { get; set; }
		public string Wed { get; set; }
		public string Sun { get; set; }
		public string Thu { get; set; }
		public string PartnerTag { get; set; }
		public string PostCode { get; set; }
		public string DistanceinMiles { get; set; }
		public List<double> Coordinates { get; set; }
		public string PartnerType { get; set; }
		public double Lat { get; set; }
		public int PartnerId { get; set; }
		public string Mon { get; set; }
		public string CloseTime { get; set; }
		public string Country { get; set; }
		public string Fri { get; set; }
		public string Partner { get; set; }
		public Id _id { get; set; }
		public string Tue { get; set; }
		public string Sat { get; set; }
	}
}

