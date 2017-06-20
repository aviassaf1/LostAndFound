using System.Collections.Generic;

namespace NewWebClient
{
    public interface IimageProcessing
    {
        void processImage(string path, List<string> types, List<int> colorIndexList);
    }
}