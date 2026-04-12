using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog
{
    internal class ACSIIArt
    {
        //create acsii art withthis method
        public static void ImageMaker(Image image)
        {
            //characters to be used in art..values go from brightest to darkest
            var asciiChar = "@#i:,. ";
            Bitmap img = new Bitmap(image); //create bitmap instance from image

            int div = img.Width / 40;
            img = new Bitmap(img, new Size(img.Width / div, img.Height / div));//new bitmap image size to cater to window size
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
            Console.WriteLine(m);//display image
        }

        //create border to fit any text
        public static void BorderMaker(string text)
        {
            string borderH = new string('-', 12 + text.Length + 2); //top and bottome of border
            string borderV = "|";//sides of broder
            string pad = new string(' ', 6);//padding to text
            string width = new string(' ', 12 + text.Length);
            int i = 0;

            Console.WriteLine(borderH);
            while (i < 6)
            {
                Console.WriteLine(borderV + width + borderV);
                if(i == 2)
                {
                    Console.WriteLine(borderV + pad + text + pad + borderV);
                }
                i++;
            }
            Console.WriteLine(borderH);
        }

    }
}
