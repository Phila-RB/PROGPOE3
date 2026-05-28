using System.Drawing;

namespace Prog
{
    internal class ACSIIArt
    {
        //create acsii art withthis method
        public static string ImageMaker(Image image)
        {
            //characters to be used in art..values go from brightest to darkest
            var asciiChar = "@#i:,. ";
            Bitmap img = new(image); //create bitmap instance from image
       
            int div = img.Width / 40;
            img = new(img, new Size(img.Width / div, img.Height / div));//new bitmap image size to cater to window size
            string m = "";
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    var pixel = img.GetPixel(j, i);//get colour structure of pixel
                    var avg = (pixel.R + pixel.G + pixel.B) / 3;
                    m += (asciiChar[avg * (asciiChar.Length - 1) / 255]);//place new pixel in string
                }
                m += ('\n');
            }
            return m;//display image
        }

        //create border to fit any text
        public static string BorderMaker(string text)
        {
            string borderH = new('-', 12 + text.Length + 2); //top and bottome of border
            string borderV = "|";//sides of broder
            string pad = new(' ', 6);//padding to text
            string width = new(' ', 12 + text.Length);
            int i = 0;
            string border = "";

            border += borderH + "\n";
            while (i < 6)
            {
                if (i == 2)
                {
                    border += borderV + pad + text + pad + borderV;
                }
                else
                {
                    border += borderV + width + borderV + "\n";
                }
                i++;
            }
            border += borderH + "\n";
            return border;
        }

    }
}
