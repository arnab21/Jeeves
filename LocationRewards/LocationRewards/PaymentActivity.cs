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

using Infobip.Mpayments;
using RestSharp; 
using Newtonsoft.Json;

namespace LocationRewards
{
	public class PurchaseListener : IPurchaseListener	
	{
		Button btn;
		public PurchaseListener(Button purchaseBtn)
		{
			btn = purchaseBtn;
		}

		#region IPurchaseListener implementation
		public void OnPurchaseCanceled (PurchaseResponse purchaseResponse)
		{
			btn.SetBackgroundColor(color: Android.Graphics.Color.Orange);
		}
		public void OnPurchaseFailed (PurchaseResponse purchaseResponse)
		{
			btn.SetBackgroundColor(color: Android.Graphics.Color.Red);

		}
		public void OnPurchasePending (PurchaseResponse purchaseResponse)
		{
			btn.SetBackgroundColor(color: Android.Graphics.Color.Gray);
		}
		public void OnPurchaseSuccess (PurchaseResponse purchaseResponse)
		{
			btn.SetBackgroundColor(color: Android.Graphics.Color.Green);
		}
		#endregion
		#region IDisposable implementation
		public void Dispose ()
		{
			this.Dispose ();
		}
		#endregion
		#region IJavaObject implementation
		public IntPtr Handle {
			get {
				return new IntPtr (231312313131);
			}
		}
		#endregion
	}


	[Activity (Label = "PaymentActivity")]			
	public class PaymentActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "PaymentOptions" layout resource
			SetContentView (Resource.Layout.PaymentOptions);

			// Form new purchase request (your api key goes in constructor)
			PurchaseRequest pr = new Infobip.Mpayments.PurchaseRequest ("9b0077c886bdf324bc74ec097169ce5d") ;

			pr.TestModeEnabled=true;
			pr.ClientId = " Shreeman-Test";
			pr.SetLanguageCode ("EN");
			//pr.OfflineModeEnabled = true;
			//pr.PackageIndex (0);
			//pr.Price (30.0);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.payButton);

			button.Click += delegate {
				button.Text = string.Format ("{0}", "Processing Payment");

				//Centili payment gateway
				//PurchaseManager.AttachPurchaseListener(new PurchaseListener(button));
				PurchaseManager.StartPurchase(pr, this.ApplicationContext);

				//Confirm checkout and send message confirmation via Twilio
				ProcessCheckout("23","","","");
			};
		}

		public void ProcessCheckout(string orderValue, string orderDate, string orderTime, string paymentMeans)
		{
			orderValue = "23";
			orderDate = String.Concat(DateTime.Now.Day, "-", DateTime.Now.Month, "-", DateTime.Now.Year);
			orderTime = String.Concat(DateTime.Now.Hour, ":", DateTime.Now.Minute);

			paymentMeans = "CreditCard";

			var client = new RestClient (@"http://servicetest3s.aws.af.cm/");

			var request = new RestRequest (String.Format ("{0}/checkout/{1}/{2}/{3}/{4}", "cf66075a272f1dde9f84efb5545fff60", orderValue, orderDate, orderTime, paymentMeans));
			request.Method = Method.GET;
			string response = client.Execute(request).Content;

			TextView orderConfView = FindViewById<TextView> (Resource.Id.textViewOrderStatus);
			orderConfView.Text = response;

			//Toast.MakeText (this, response, ToastLength.Long).Show();
			//Toast.MakeText (this, "Error: try later", ToastLength.Long).Show();
			//Toast.setGravity(Gravity.BOTTOM|Gravity.LEFT, 0, 0);
		}
	}
}

