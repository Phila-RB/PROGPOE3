using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Prog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Welcome audio and diction variable
            //dictionary contains string for key and list of possible answers for values
            SoundPlayer welcome = new SoundPlayer(Properties.Resources.welcome_audio);
            Dictionary<string,List<string>> dictionary = new Dictionary<string, List<string>>()
            {
                { "hello", new List<string>(){ "Hello how can I help","Hey how can i help", "yo how can i be of service" } },
                { "hi", new List<string>(){ "Hello how can I help","Hey how can i help", "yo how can i be of service" } },
                { "how are you", new List<string>(){ "Im good thanks! how can i help you today","Im great thanks! what would you like to learn about to day", "Im awesome yo, how can i be of service" } },
                { 
                  "what is phishing", new List<string>(){
                  "Phishing is an online con game that functions exactly like fishing. \r\nThe Bait: Scammers send fraudulent, enticing messages (emails, texts, or calls) designed to look legitimate, often using urgent language like \"Your account will be suspended\" or \"You have won a prize\".\r\nThe Hook: The recipient, feeling panic or greed, clicks a malicious link, opens an attachment, or replies with sensitive data.\r\nThe Capture: The attacker steals login credentials, credit card numbers, or installs ransomware on the victim's device.",
                  "Phishing operates in a sequence where a fraudulent message (Bait) lures a user into taking action, such as clicking a link or downloading a file (Hook), resulting in the theft of credentials or installation of malware (Capture).",
                  "Phishing is a type of cyber-attack where attackers masquerade as a trusted entity—such as a bank, colleague, or well-known brand—to deceive victims into revealing sensitive information, clicking malicious links, or downloading malware"
                } },
                { 
                  "what is password safety", new List<string>{
                  "Password safety and protection is the foundational practice of creating, managing, and securing login credentials to prevent unauthorized access to digital accounts and sensitive data. It acts as the primary barrier against cyberattacks such as data breaches, identity theft, and hacking.",
                  "Password safety is the practice of protecting digital accounts and data from unauthorized access using strong, unique, and complex passwords. It involves managing password strength, preventing credential reuse, using secure storage, and adopting technologies like Multi-Factor Authentication (MFA) to prevent data theft and identity breaches.",
                  "Password security and password protection are practices for establishing and verifying identity and restricting access to devices, files, and accounts. They help ensure that only those who can provide a correct password in response to a prompt are given access."
                } },
                {
                  "what is safe browsing", new List<string>{
                  "Safe browsing is a combination of habits, tools, and practices designed to protect your personal information, digital identity, and devices from cyber threats like malware, phishing, and data theft while using the internet.",
                  "Safe Browsing is a security service made by google" +
                  "that protects user devices by warning them before they visit dangerous websites, download malicious software, or fall victim to phishing attacks. It scans the web to identify, warn, and protect users in real-time across Chrome, Android, and Gmail.",
                  "Safe browsing is a practice that we follow to ensure we stay safe on the internet and not expose ourselves to dangerous threats like malware and data theft."
                } },
                { "what can i ask about", new List<string>{ "You can ask about: what is\npassword safety\nphishing\nsafe browsing\nyour purpose" } },
                { 
                  "what is your purpose", new List<string>{ 
                  "My purpose is to educate you on all matter of digital safety.",
                  "My purpose is to help you learn and navigate threats in the digital space.",
                  "My role is to help people understand the digital world so they can remain safe."
                } },
            };

            //display App logo and name
            ACSIIArt.ImageMaker(Properties.Resources.logo);
            ACSIIArt.BorderMaker("Cyber Security Awareness Bot");

            //play welcome audio and delay timer to not conflict audios
            welcome.Play();
            Thread.Sleep(8000);

            Console.WriteLine();
            Console.WriteLine("What is your name");
            TextToSpeech.Speak("What is your name");

            //user name
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Hi friend Welcome to Cuber Help");
                TextToSpeech.Speak("Hi friend Welcome to Cuber Help");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Hi " + name + " Welcome to Cyber Help");
                TextToSpeech.Speak("Hi " + name + " Welcome to Cyber Help");
                Console.WriteLine();
            }


            Console.WriteLine("If your ever lost just ask: what can i ask about");
            TextToSpeech.Speak("If your ever lost just ask: what can i ask about");
            Console.WriteLine();

            string q = Console.ReadLine().ToLower();
            Ask(q, dictionary);

            Console.WriteLine("press any key to exit...");
            Console.ReadKey();

        }

        //Ask Method
        //This method lets the user communicate with the bot and ask questions
        public static void Ask(string question, Dictionary<string, List<string>> list){
            //if question is valid bot will give random related answer 
            Random random = new Random();
            if (list.ContainsKey(question))
            {
                List<string> listAns = list[question];
                int choice = random.Next(listAns.Count());

                Console.WriteLine(listAns[choice]);
                TextToSpeech.Speak(listAns[choice]);
                Console.WriteLine();

                string newQ = Console.ReadLine().ToLower();
                Ask(newQ, list);

            }

            //if quesiton is not valid go through series of if statements trying find/help user ask right questions
            if (!list.ContainsKey(question))
            {
                string newQ;
                if (string.IsNullOrWhiteSpace(question))
                {
                    Console.WriteLine("You just entered nothing, please ask an actual question or ask: what can i ask about");
                    TextToSpeech.Speak("You just entered nothing, please ask an actual question or ask: what can i ask about");
                    Console.WriteLine();
                    newQ = Console.ReadLine().ToLower();
                    Ask(newQ, list);
                }
                else if(question == "quit")
                {
                    Console.WriteLine("Do you want to quit? Please confirm with Yes");
                    TextToSpeech.Speak("Do you want to quit? Please confirm with Yes");
                    Console.WriteLine();
                    newQ = Console.ReadLine().ToLower();
                    if (newQ == "yes") 
                    { 
                        Console.WriteLine("You have been learning with Cyber Bot, GoodBye!");
                        TextToSpeech.Speak("You have been learning with Cyber Bot, GoodBye!");
                        Console.WriteLine();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You didnt say yes, lets continue learning then! What do you want to know?");
                        TextToSpeech.Speak("You didnt say yes, lets continue learning then! What do you want to know?");
                        Console.WriteLine();
                        newQ = Console.ReadLine().ToLower();
                        Ask(newQ, list);
                    }
                }
                else
                {
                    Console.WriteLine("Im sorry i dont know what you mean, could you please rephrase the question or ask:\nwhat can i ask about?");
                    TextToSpeech.Speak("Im sorry i dont know what you mean, could you please rephrase the question or ask:\nwhat can i ask about?");
                    Console.WriteLine();
                    newQ = Console.ReadLine().ToLower();
                    Ask(newQ, list);
                }
            }
        }
    }
}
