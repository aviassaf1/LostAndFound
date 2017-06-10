using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Jil;
using Newtonsoft.Json;
using System.Drawing;
using NewWebClient;

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
            List<string> description = new List<string>();
            List<int> colorIndexList = new List<int>();
            DoStuff(path, description, colorIndexList).Wait();
        }

        /*private static string getColorName(int red, int green, int blue)
        {
            foreach (KnownColor kc in Enum.GetValues(typeof(KnownColor)))
            {
                Color known = Color.FromKnownColor(kc);
                if (Color.FromArgb(red, green, blue, 0).ToArgb() == known.ToArgb())
                {
                    return known.Name;
                }
            }
            return "color name not found";
        }*/

        static async Task DoStuff(string filePath, List<string> descArr, List<int> colorIndexList)
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
                        dynamic JsonValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                        dynamic responses = JsonValues["responses"];
                        dynamic temp = responses[0];
                        dynamic labelAnnotations = temp["labelAnnotations"];
                        foreach (dynamic label in labelAnnotations)
                        {
                            descArr.Add(label["description"]);
                        }
                        dynamic imagePropertiesAnnotation = temp["imagePropertiesAnnotation"];
                        dynamic dominantColors = imagePropertiesAnnotation["dominantColors"];
                        dynamic colors = dominantColors["colors"];
                        foreach (dynamic label in colors)
                        {
                            dynamic color = label["color"];
                            int red = color["red"];
                            int green = color["green"];
                            int blue = color["blue"];
                            int colorIndex = ColorFromRGB.getColor(red, green, blue);
                            colorIndexList.Add(colorIndex);
                        }
                    }
                }
            }
        }
    }
}
