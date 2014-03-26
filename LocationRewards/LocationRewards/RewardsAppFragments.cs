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

namespace LocationRewards
{
	public class BrowseTraderPartnerLocationsFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			//View view = inflater.Inflate(Resource.Layout.RewardsFragment, null);
			View view = inflater.Inflate(Resource.Layout.TraderPartnerLocationsList, null);
			view.FindViewById<TextView>(Resource.Id.rewardsTextView).SetText(Resource.String.speakers_tab_label);
			view.FindViewById<ImageView>(Resource.Id.rewardsImageView).SetImageResource(Resource.Drawable.ic_action_speakers);
			return view;
		}
	}

	public class BrowsePartnerProductsFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.RewardsFragment, null);
			view.FindViewById<TextView>(Resource.Id.rewardsTextView).SetText(Resource.String.sessions_tab_label);
			view.FindViewById<ImageView>(Resource.Id.rewardsImageView).SetImageResource(Resource.Drawable.ic_action_sessions);
			return view;
		}
	}

	public class RedeemVoucherFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.RewardsFragment, null);
			view.FindViewById<TextView>(Resource.Id.rewardsTextView).SetText(Resource.String.whatson_tab_label);
			view.FindViewById<ImageView>(Resource.Id.rewardsImageView).SetImageResource(Resource.Drawable.ic_action_whats_on);
			return view;
		}
	}
}

