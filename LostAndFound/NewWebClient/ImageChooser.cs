using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace NewWebClient
{
    public class ImageChooser
    {
       private static OpenFileDialog openFileDialog1;

        public static List<String> getImagePath()
        {
            List<String> fList = new List<string>();
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Browse Item Picturs";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "jpg";
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (String path in openFileDialog1.FileNames)
                {
                    fList.Add(path);
                }
            }
            return fList;
        }
    }
}
