//
//  Copyright 2012, Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.IO.IsolatedStorage;
using System.Xml;
using RestSharp; 
using Newtonsoft.Json;

namespace TradingPartnersDirectory.Data
{
	[XmlRoot ("Favorites")]
	public class PartnersProductsRestApiRepository : IFavoritesRepository
	{

		private string _repositoryIdentifier;
		public event EventHandler Changed;

		public List<Person> People { get; set; }

		public PartnersProductsRestApiRepository(string repositoryIdentifier)
		{
			_repositoryIdentifier = repositoryIdentifier;
		}

		public void GetPartnerProducts()
		{
			var client = new RestClient (@"http://servicetest3s.aws.af.cm/");

			var request = new RestRequest (String.Format ("{0}/getproductsforpartner/{1}", "cf66075a272f1dde9f84efb5545fff60", _repositoryIdentifier));
			request.Method = Method.GET;
			string response = client.Execute(request).Content;

			PartnerProducts[] partnerProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<PartnerProducts[]> (response);
			MapPartnerProductsToPeople (partnerProducts);
		}

		private void MapPartnerProductsToPeople(PartnerProducts[] partnerProducts)
		{
			People = new List<Person> ();

			foreach (PartnerProducts product in partnerProducts) {

				var p = new Person();
				p.Id = product.ProductId.ToString();
				p.Email = product.ProductImage;
				p.Name = product.ProductName;
				p.Title = product.ProductDescription;
				p.Department = product.Price.ToString();
				p.ImageUrl = product.ProductImage;
				p.Twitter =  String.Format("Redeem for {0} clubcard points", product.RedemptionPoints);

				People.Add (p);

			}
		}



		#region IFavoritesRepository implementation

		public IEnumerable<Person> GetAll ()
		{
			GetPartnerProducts();
			return People;
		}

		public Person FindById (string id)
		{
			return People.FirstOrDefault (x => x.Id == id);
		}

		public bool IsFavorite (Person person)
		{
			return People.Any (x => x.Id == person.Id);
		}

		public void InsertOrUpdate (Person person)
		{

		}

		public void Delete (Person person)
		{
		}

		#endregion
	}
}

