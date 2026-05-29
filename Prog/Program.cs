using Prog;
using Prog.Properties;
using System.Media;
using System.Speech.Recognition;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Windows.System;
internal class Program
{
    public static string name = "";
    public static Dictionary<string, List<string>> dictionary = new()
            {
                { "hello", new List<string>(){ "Hello how can I help?","Hey how can i help!", "yo how can i be of service!?" } },
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
                { "what can i ask about", new List<string>{ "You can ask about: what is\npassword safety\nphishing\nsafe browsing\nyour purpose\n You can also tell me what your interested in by saying:\n im interested in blank\nor i like blank\n and i will remember it" } },
                {
                  "what is your purpose", new List<string>{
                  "My purpose is to educate you on all matter of digital safety.",
                  "My purpose is to help you learn and navigate threats in the digital space.",
                  "My role is to help people understand the digital world so they can remain safe."
                } },
            };
    public static List<string> userInterests = new() { };
    public static List<string> comfortText = new() { "im sorry i know how frustrating", "its ok to be worried about", "dont stress about it ill teach you tips so you can stay safe." };
    [STAThread]
    static void Main(string[] args)
    {
        //Welcome audio and diction variable
        //dictionary contains string for key and list of possible answers for value

        Application app = new Application();
        GUI frame = new();


        SoundPlayer welcome = new(Resources.welcome_audio);

        //display App logo and name
        TextBlock artASCII = new()
        {
            Text = ACSIIArt.ImageMaker(Resources.logo),
            FontFamily = new FontFamily("Consolas, Courier New, Lucida Console, Monospace")
        };
        artASCII.FontSize = 12;
        artASCII.Margin = new Thickness(20);
        artASCII.TextWrapping = TextWrapping.Wrap;

        frame.msgView.Children.Add(artASCII);

        TextBlock artBorder = new()
        {
            Text = ACSIIArt.BorderMaker("Cyber Security Awareness Bot"),
            FontFamily = new FontFamily("Consolas, Courier New, Lucida Console, Monospace")
        };
        artBorder.FontSize = 12;
        artBorder.Margin = new Thickness(20);
        artBorder.TextWrapping = TextWrapping.Wrap;

        frame.msgView.Children.Add(artBorder);
        frame.Loaded += async (sender, args) =>
        {
            ////play welcome audio and delay timer to not conflict audios
            await Task.Run(() =>
            {
                //welcome.PlaySync();
                //BotSpeak("What is your name", frame);

            });
            //Thread.Sleep(8000);//

            string name = await frame.RunConversationAsync();
         


            //if (string.IsNullOrEmpty(name))
            //{
            //    BotSpeak("Hi friend Welcome to Cuber Help", frame);
            //    //TextToSpeech.Speak("Hi friend Welcome to Cuber Help");
            //}
            //else
            //{
            //    BotSpeak("Hi " + name + " Welcome to Cyber Help", frame);
            //    //TextToSpeech.Speak("Hi " + name + " Welcome to Cyber Help");
            //}


            BotSpeak("If your ever lost just ask: what can i ask about", frame);
            //TextToSpeech.Speak("If your ever lost just ask: what can i ask about");

            string que = await frame.RunConversationAsync();
            Ask(que, dictionary, frame);

            //Console.WriteLine("press any key to exit...");
        };

        app.Run(frame);
    }

    //Ask Method
    //This method lets the user communicate with the bot and ask questions
    public static async void Ask(string question, Dictionary<string, List<string>> list, GUI frame)
    {
        //if question is valid bot will give random related answer 
        Random random = new();
        string[] check = { "phishing", "password", "scam", "privacy", "browsing" };
        string[] knowMore = { "tell me more", "give me another tip", "elaborate", "expound" };
        string[] rem = { "interested in", "i like"};
        string[] senti = { "worried", "frustrated", "scared" };

        if (list.ContainsKey(question))
        {
            List<string> listAns = list[question];


            int desc = random.Next(comfortText.Count());

            BotSpeak(listAns[desc], frame);
            //TextToSpeech.Speak(listAns[choice]);

           
            string newQ = await frame.RunConversationAsync();
            if (knowMore.Any(m => newQ.Contains(m))){
                Ask(question, list, frame);
            }
            Ask(newQ, list, frame);

        }
        else if (rem.Any(m => question.Contains(m)))//remember topic liked
        {

            if(check.Any(m => question.Contains(m))){
                
                string search = check.FirstOrDefault(m => question.Contains(m));
                BotSpeak("That is super cool! I will remember that. Heres a cool fact I know about " + search, frame);
                foreach (string match in list.Keys)
                {
                        if (match.Contains(search))
                        {
                            List<string> listAns = list[match];

                            int choice = random.Next(listAns.Count());

                            BotSpeak(listAns[choice], frame);
                            //TextToSpeech.Speak(listAns[choice]);

                            string newq = await frame.RunConversationAsync();
                            Ask(newq, list, frame);
                        }
                }
            }
            else
            {
                BotSpeak("That is super cool! I will remember that.", frame);
            }

            string newQ = await frame.RunConversationAsync();
            if (knowMore.Any(m => newQ.Contains(m)))
            {
                Ask(question, list, frame);
            }
            Ask(newQ, list, frame);
        }
        else
        {
            if(check.Any(m => question.Contains(m))){
                if (senti.Any(m => question.Contains(m)))
                {
                    string searchA = check.FirstOrDefault(m => question.Contains(m));
                    int choice = 2;

                    if (choice == 2)
                    {
                        BotSpeak(comfortText[choice] + " " + searchA + " can be", frame);
                    }
                    else
                    {
                        BotSpeak(comfortText[choice] + " " + searchA + " can be", frame);
                    }
                }
                string search = check.FirstOrDefault(m => question.Contains(m));
                foreach (string match in list.Keys)
                {
                        if (match.Contains(search))
                        {
                            List<string> listAns = list[match];

                            int choice = random.Next(listAns.Count());

                            BotSpeak(listAns[choice], frame);
                            //TextToSpeech.Speak(listAns[choice]);

                            string newQ = await frame.RunConversationAsync();
                            if (knowMore.Any(m => newQ.Contains(m)))
                            {
                                Ask(question, list, frame);
                            }
                            Ask(newQ, list, frame);
                        }
                    
                }
            }

        }


        //if quesiton is not valid go through series of if statements trying find/help user ask right questions
        if (!list.ContainsKey(question))
        {
            string newQ;
            if (string.IsNullOrWhiteSpace(question))
            {
                BotSpeak("You just entered nothing, please ask an actual question or ask: what can i ask about", frame);
                newQ = await frame.RunConversationAsync();
                Ask(newQ, list, frame);
            }
            else if (question == "quit")
            {
                BotSpeak("Do you want to quit? Please confirm with Yes", frame);
                newQ = await frame.RunConversationAsync(); 
                if (newQ == "yes")
                {
                    BotSpeak("You have been learning with Cyber Bot, GoodBye!", frame);
                    return;
                }
                else
                {
                    BotSpeak("You didnt say yes, lets continue learning then! What do you want to know?", frame);
                    newQ = await frame.RunConversationAsync();
                    Ask(newQ, list, frame);
                }
            }
            else
            {
                BotSpeak("Im sorry i dont know what you mean, could you please rephrase the question or ask:\nwhat can i ask about?", frame);
                newQ = await frame.RunConversationAsync();
                Ask(newQ, list, frame);
            }
        }
    }

    public static void BotSpeak(string text, GUI frame)
    {
        frame.Dispatcher.Invoke(() =>
        {
            TextBlock botMsg = new();
            botMsg.Inlines.Add(new Run("Bot: ")
            {
                Foreground = Brushes.Green
            });
            botMsg.Inlines.Add(new Run(text));
            botMsg.Margin = new Thickness(20);
            botMsg.TextWrapping = TextWrapping.Wrap;
            frame.msgView.Children.Add(botMsg);
            TextToSpeech.Speak(text);
        });
    }
}
