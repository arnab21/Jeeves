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

using Android.Graphics;
using TradingPartnersDirectory.Utilities;

namespace LocationRewards
{
	public class AndroidImageDownloader : ImageDownloader
	{
		public AndroidImageDownloader ()
			: base (2)
		{
		}

		protected override object LoadImage (System.IO.Stream stream)
		{
			return BitmapFactory.DecodeStream (stream);
		}
	}
}

