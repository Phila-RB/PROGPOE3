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
        
        public static void ImageMaker(Image image)
        {
            var asciiChar = "@#i:,. ";
            Bitmap img = new Bitmap(image);

            int div = img.Width / 40;
            img = new Bitmap(img, new Size(img.Width / div, img.Height / div));
            string m = "";
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    var pixel = img.GetPixel(j, i);
                    var avg = (pixel.R + pixel.G + pixel.B) / 3;
                    m += (asciiChar[avg * (asciiChar.Length - 1) / 255]);
                }
                m += ('\n');
            }
            Console.WriteLine(m);
        }

        public static void BorderMaker(string text)
        {
            string borderH = new string('-', 12 + text.Length + 2);
            string borderV = "|";
            string pad = new string(' ', 6);
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
