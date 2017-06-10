using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Jil;


namespace VisionApiTest
{
    public class VisionApiRequest
    {
        public VisionApiImage image { get; set; }
        public IEnumerable<VisionApiFeatures> features { get; set; }
    }

    public class VisionApiImage
    {
        public string content { get; set; }
    }

    public class VisionApiFeatures
    {
        //public enum FeatureTypes
        //{
        //    Unknown = 0,
        //    LabelDetection = 1
        //}

        public string type { get; set; }  // TODO use an enum here
        public int maxResults { get; set; }
    }

    public class VisionApiRequests
    {
        public IEnumerable<VisionApiRequest> requests { get; set; }
    }

    class ImageProccessingClass
    {
        static void processImage(string path)
        {
            DoStuff(path).Wait();
        }

        static async Task DoStuff(string filePath)
        {
            const string apiKey = "AIzaSyAqXXOCmKkLYOWIhZ7j1ssYF2pcM-KigsI";
            const string baseUrlFormat = "https://vision.googleapis.com/v1/images:annotate?key={0}";

            //from local file
            var imageBase64 = Convert.ToBase64String(File.ReadAllBytes(filePath)); // TODO async

            //from web
            //const string url = "https://scontent.ftlv1-1.fna.fbcdn.net/v/t1.0-9/18953080_419248961793283_2445475930130003386_n.jpg?oh=f591d1bfe3f2433991dccbcd2a45e8bc&oe=59D7B917";
            //byte[] downloadedUrl = new System.Net.WebClient().DownloadData(url);
            //var imageBase64 = Convert.ToBase64String(downloadedUrl); // TODO async


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var apiUri = new Uri(string.Format(baseUrlFormat, apiKey));
                var requests = new VisionApiRequests()
                {
                    requests = new[]
                    {
                        new VisionApiRequest()
                        {
                            image = new VisionApiImage() { content = imageBase64 },
                            features = new[]
                            {
                                new VisionApiFeatures()
                                {
                                    type = "LABEL_DETECTION",
                                    maxResults = 10
                                },

                                 new VisionApiFeatures()
                                {
                                    type = "IMAGE_PROPERTIES",
                                    maxResults = 10
                                }
                            }
                        }
                    }
                };

                using (var response = await client.PostAsync(apiUri, new StringContent(JSON.Serialize(requests), Encoding.UTF8, "application/json")))
                {
                    using (var content = response.Content)
                    {
                        var json = await content.ReadAsStringAsync();
                        Console.WriteLine(json);
                    }
                }
            }
        }

    }
}
