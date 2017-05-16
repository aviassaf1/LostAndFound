using System;
using UIKit;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace cloudSightXamarin
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			var client = new RestClient("https://api.cloudsightapi.com/");
			var request = new RestRequest("image_requests/", Method.POST);

			// Set the string below to the API key given to you by Cloud Sight 
			string cloudSightAPIKey = "QrLUa0Dg-olEEyLo-1hn3Q";
			request.AddHeader ("Authorization", "CloudSight " + cloudSightAPIKey);
			// Set URL to an image 
			request.AddParameter ("image_request[remote_image_url]", ("http://coolspotters.com/files/photos/825834/smartwater-profile.jpg"));
			request.AddParameter ("image_request[locale]", "en");

			IRestResponse response = client.Execute(request);
			var content = response.Content;

			// deserialize token
			JObject obj=JObject.Parse(content);

			JToken keyValue, keyName;

			// grab token value
			if (obj.TryGetValue ("token", out keyValue)) {} 


			// GET image details 
			var getImageDetails = new RestRequest("/image_responses/" + keyValue, Method.GET);
			getImageDetails.AddHeader("Authorization", "CloudSight " + cloudSightAPIKey);

			do {
				IRestResponse imageResponse = client.Execute (getImageDetails);
				var imageContent = imageResponse.Content;
				Console.WriteLine (imageContent);

				// parse the imageContent
				obj = JObject.Parse (imageContent);

				// grab the status value
				if (obj.TryGetValue ("status", out keyValue)) {}

				// grab the name of imageContent once completed
				if (obj.TryGetValue ("name", out keyName) && keyValue.ToString().ToLower() == "completed") {
					Console.WriteLine(keyName);
				}

			} while (keyValue.ToString().ToLower() == "not completed"); // keep running through loop while it's not complete
		}
			
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

