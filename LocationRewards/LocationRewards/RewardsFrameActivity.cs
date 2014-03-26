using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LocationRewards
{
	[Activity (Label = "RewardsFrameActivity")]			
	public class RewardsFrameActivity : Activity
	{
		static readonly string Tag = "ActionBarTabsSupport";

		Fragment[] _fragments;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.RewardsFrame);

			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			ActionBar.Title =  Intent.GetStringExtra("MainScreenData") ?? "Browse Rewards";

			SetContentView(Resource.Layout.RewardsFrame);

			_fragments = new Fragment[]
			{
				new BrowseTraderPartnerLocationsFragment(),
				new BrowsePartnerProductsFragment(),
				new RedeemVoucherFragment()
			};

			AddTabToActionBar(Resource.String.whatson_tab_label, Resource.Drawable.ic_action_whats_on);
			AddTabToActionBar(Resource.String.speakers_tab_label, Resource.Drawable.ic_action_speakers);
			AddTabToActionBar(Resource.String.sessions_tab_label, Resource.Drawable.ic_action_sessions);

		}

		void AddTabToActionBar(int labelResourceId, int iconResourceId)
		{
			ActionBar.Tab tab = ActionBar.NewTab()
				.SetText(labelResourceId)
				.SetIcon(iconResourceId);
			tab.TabSelected += TabOnTabSelected;
			ActionBar.AddTab(tab);
		}

		void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
		{
			ActionBar.Tab tab = (ActionBar.Tab)sender;

			Log.Debug(Tag, "The tab {0} has been selected.", tab.Text);
			Fragment frag = _fragments[tab.Position];
			tabEventArgs.FragmentTransaction.Replace(Resource.Id.fragmentContainer, frag);
		}
	}
}

