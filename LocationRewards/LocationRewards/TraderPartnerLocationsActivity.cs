using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using TradingPartnersDirectory.ViewModels;
using TradingPartnersDirectory.Data;

namespace LocationRewards
{
	[Activity (Label = "TraderPartnerLocationsActivity")]
	[MetaData ("android.app.default_searchable", Value = "locationRewards.searchActivity")]
	public class TraderPartnerLocationsActivity : ListActivity
	{
		FavoritesViewModel viewModel;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//
			// Load the UI
			//
			SetContentView (Resource.Layout.PartnerProductsList);
		}

		protected override void OnStart ()
		{
			base.OnStart ();

			string partnerType =  Intent.GetStringExtra("MainScreenData");
			var repository = new TradingPartnersRestApiRepository (partnerType);
			viewModel = new FavoritesViewModel (repository, groupByLastName: true);
			Title = partnerType;


			ListAdapter = new PeopleGroupsAdapter () {
				ItemsSource = viewModel.Groups,
			};			 
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.AppActivityOptionsMenu, menu);
			var searchManager = (SearchManager)GetSystemService (Context.SearchService);
			var searchView = (SearchView)menu.FindItem (Resource.Id.MenuSearch).ActionView;
			var searchInfo = searchManager.GetSearchableInfo (ComponentName);
			searchView.SetSearchableInfo (searchInfo);
			return base.OnCreateOptionsMenu (menu);
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			//var person = ((PeopleGroupsAdapter)ListAdapter).GetPerson (position);
			//if (person != null) {
			//	StartActivity (PartnerProductsActivity.CreateIntent (this, person));
			//}

			var person = ((PeopleGroupsAdapter)ListAdapter).GetPerson (position);
			if (person != null) {
				StartActivity (TraderPartnerProductsBrowseActivity.CreateIntent (this, person.Id));
			}
		}
	}
}

