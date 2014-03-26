using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace LocationRewards
{
	[Activity (Label = "@string/app_name", MainLauncher = true,  Icon = "@drawable/ic_launcher")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			ImageView cat_imageView1 = FindViewById<ImageView> (Resource.Id.cat_imageView1);
			ImageView cat_imageView2 = FindViewById<ImageView> (Resource.Id.cat_imageView2);
			ImageView cat_imageView3 = FindViewById<ImageView> (Resource.Id.cat_imageView3);
			ImageView cat_imageView4 = FindViewById<ImageView> (Resource.Id.cat_imageView4);
			ImageView cat_imageView5 = FindViewById<ImageView> (Resource.Id.cat_imageView5);


			//imageButtonNext.Click += delegate {
			//	StartActivity(typeof(SecondaryActivity));

			//cat_imageView1.Click += delegate {
			//	var rewardsFrameActivity = new Intent(this, typeof(RewardsFrameActivity));
			//	rewardsFrameActivity.PutExtra("MainScreenData", cat_imageView1.Tag);
			//	StartActivity(rewardsFrameActivity);
			//};

			//var rewardsFrameActivity = new Intent(this, typeof(RewardsFrameActivity));
			var rewardsFrameActivity = new Intent(this, typeof(TraderPartnerLocationsActivity));

			cat_imageView1.Click += delegate {
				rewardsFrameActivity.PutExtra("MainScreenData", cat_imageView1.Tag.ToString());
				StartActivity(rewardsFrameActivity);
			};
			cat_imageView2.Click += delegate {
				rewardsFrameActivity.PutExtra("MainScreenData", cat_imageView2.Tag.ToString());
				StartActivity(rewardsFrameActivity);
			};
			cat_imageView3.Click += delegate {
				rewardsFrameActivity.PutExtra("MainScreenData", cat_imageView3.Tag.ToString());
				StartActivity(rewardsFrameActivity);
			};
			cat_imageView4.Click += delegate {
				rewardsFrameActivity.PutExtra("MainScreenData", cat_imageView4.Tag.ToString());
				StartActivity(rewardsFrameActivity);
			};
			cat_imageView5.Click += delegate {
				rewardsFrameActivity.PutExtra("MainScreenData", cat_imageView5.Tag.ToString());
				StartActivity(rewardsFrameActivity);
			};



		}

	}
}


