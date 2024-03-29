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


namespace TradingPartnersDirectory.Data
{
	[XmlRoot ("Favorites")]
	public class XmlFavoritesRepository : IFavoritesRepository
	{
		public string IsolatedStorageName { get; set; }

		public event EventHandler Changed;

		public List<Person> People { get; set; }

		public static XmlFavoritesRepository OpenIsolatedStorage (string isolatedStorageName)
		{
			var serializer = new XmlSerializer (typeof(XmlFavoritesRepository));
			var iso = IsolatedStorageFile.GetUserStoreForApplication ();

			try {
				using (var f = iso.OpenFile (isolatedStorageName, FileMode.Open)) {
					var repo = (XmlFavoritesRepository)serializer.Deserialize (f);
					repo.IsolatedStorageName = isolatedStorageName;
					return repo;
				}
			} catch (Exception) {
				return new XmlFavoritesRepository {
					IsolatedStorageName = isolatedStorageName,
					People = new List<Person> ()
				};
			}
		}

		public static XmlFavoritesRepository OpenFile (string path)
		{
			var serializer = new XmlSerializer (typeof(XmlFavoritesRepository));            
#if WINDOWS_PHONE
            using(var f = System.Windows.Application.GetResourceStream(new Uri(path, UriKind.RelativeOrAbsolute)).Stream){
#else
			using (var f = File.OpenRead (path)) {
#endif
				var repo = (XmlFavoritesRepository)serializer.Deserialize (f);
				repo.IsolatedStorageName = Path.GetFileName (path);
				return repo;
			}
		}

		void Commit ()
		{
			var serializer = new XmlSerializer (typeof(XmlFavoritesRepository));
			var iso = IsolatedStorageFile.GetUserStoreForApplication ();

			using (var f = iso.OpenFile (IsolatedStorageName, FileMode.Create)) {
				serializer.Serialize (f, this);
			}

			var ev = Changed;
			if (ev != null) {
				ev (this, EventArgs.Empty);
			}
		}

		#region IFavoritesRepository implementation

		public IEnumerable<Person> GetAll ()
		{
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
			var existing = People.FirstOrDefault (x => x.Id == person.Id);
			if (existing != null) {
				People.Remove (existing);
			}
			People.Add (person);
			Commit ();
		}

		public void Delete (Person person)
		{
			var newPeopleQ = from p in People where p.Id != person.Id select p;
			var newPeople = newPeopleQ.ToList ();
			var n = People.Count - newPeople.Count;
			People = newPeople;
			if (n != 0) {
				Commit ();
			}
		}

		#endregion
	}
}

