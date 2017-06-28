using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace NewWebClient
{
    public class imageProcessingMicrosoft : IimageProcessing
    {
        private string _subscriptionKey;
        private string _key2;
        Dictionary<string, int> _color2index;
        int _lastIndex;
        public imageProcessingMicrosoft()
        {
            _subscriptionKey = "98285e0f35ed4e6f921fd35509fa2b87";
            _key2 = "29d4419bdfb14f908e9dfcc613e62ca0";
            _color2index = new Dictionary<string, int>();
            _color2index.Add("PINK", 0);
            _color2index.Add("BLACK", 1);
            _color2index.Add("BLUE", 2);
            _color2index.Add("RED", 3);
            _color2index.Add("GREEN", 4);
            _color2index.Add("YELLOW", 5);
            _color2index.Add("WHITE", 6);
            _color2index.Add("PURPLE", 7);
            _color2index.Add("ORANGE", 8);
            _color2index.Add("GRAY", 9);
            _color2index.Add("BROWN", 10);
            _color2index.Add("GOLD", 11);
            _color2index.Add("SILVER", 12);
            _lastIndex = _color2index.Count;
        }

        public void processImage(string path, List<string> types, List<int> colorIndexList)
        {
            List<AnalysisResult> container = new List<AnalysisResult>();
            UploadAndAnalyzeImage(path, container).Wait();
            AnalysisResult res = container.First();
            foreach (var item in res.Tags)
            {
                types.Add(item.Name);
            }
            foreach(var item in res.Description.Captions)
            {
                var arr = item.Text.ToString().Split(' ');
                foreach(string s in arr)
                {
                    types.Add(s);
                }
            }
            if (types.Contains("laptop"))
            {
                types.Add("PC");
            }
            foreach (string color in res.Color.DominantColors)
            {
                if (_color2index.Keys.Contains(color.ToUpper()))
                {
                    colorIndexList.Add(_color2index[color.ToUpper()]);
                }
                else
                {
                    colorIndexList.Add(_lastIndex + 1);
                }
            }

        }

        private async Task<AnalysisResult> UploadAndAnalyzeImage(string imageFilePath, List<AnalysisResult> container)
        {
            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE STARTS HERE
            // -----------------------------------------------------------------------

            //
            // Create Project Oxford Vision API Service client
            //
            VisionServiceClient VisionServiceClient = new VisionServiceClient(_subscriptionKey, "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0");

            using (Stream imageFileStream = File.OpenRead(imageFilePath))
            {
                //
                // Analyze the image for all visual features
                //
                VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };
                AnalysisResult analysisResult = await VisionServiceClient.AnalyzeImageAsync(imageFileStream, visualFeatures);
                container.Add(analysisResult);
                return analysisResult;
            }

            // -----------------------------------------------------------------------
            // KEY SAMPLE CODE ENDS HERE
            // -----------------------------------------------------------------------
        }


    }
}