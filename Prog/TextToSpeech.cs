using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Prog
{
    internal class TextToSpeech
    {
        //text to speech sythn
        static SpeechSynthesizer ss = new SpeechSynthesizer();
        public static void Speak(string text)
        {
            ss.Speak(text);//speak
        }
    }
}
