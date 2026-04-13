using System.Speech.Synthesis;

namespace ProgPoe
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
