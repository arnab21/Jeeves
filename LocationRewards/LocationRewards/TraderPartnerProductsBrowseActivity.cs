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
using System.IO;

namespace LocationRewards
{
	[Activity (Label = "TraderPartnerProductsBrowseActivity")]
	[MetaData ("android.app.default_searchable", Value = "locationRewards.searchActivity")]
	public class TraderPartnerProductsBrowseActivity : ListActivity
	{
		FavoritesViewModel viewModel;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//
			// Load the UI
			//
			SetContentView (Resource.Layout.TraderPartnerLocationsList);

		}

		protected override void OnStart ()
		{
			base.OnStart ();

			//
			// Get the person object from the intent
			//
			string partnerId;
			if (Intent.HasExtra ("TraderPartnerProductsBrowseActivity")) {
				partnerId = Intent.GetStringExtra("TraderPartnerProductsBrowseActivity");
			} else {
				partnerId="100";
			}
			Title = "Choose products/services";

			var repository = new PartnersProductsRestApiRepository (partnerId);
			viewModel = new FavoritesViewModel (repository, groupByLastName: false);

			ListAdapter = new PeopleGroupsAdapter () {
				ItemsSource = viewModel.Groups,
			};

		}

		/// <summary>
		/// Creates the intent that can be used to present this activity given
		/// a specific Person object.
		/// </summary>
		/// <returns>
		/// The intent.
		/// </returns>
		/// <param name='person'>
		/// The Person to show in this activity.
		/// </param>
		public static Intent CreateIntent (Context context, string partnerId)
		{
			var intent = new Intent (context, typeof (TraderPartnerProductsBrowseActivity));
			intent.PutExtra ("TraderPartnerProductsBrowseActivity", partnerId);
			return intent;
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
			var person = ((PeopleGroupsAdapter)ListAdapter).GetPerson (position);
			if (person != null) {
				StartActivity (PartnerProductsActivity.CreateIntent (this, person));
			}
		}
	}
}

