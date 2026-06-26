using MySql.Data;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using Prog;
using Prog.Properties;
using System.Data;
using System.Media;
using System.Security.Principal;
using System.Speech.Recognition;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Windows.System;
internal class Program : MySqlClass
{

    public static string name = "";
    public static DataViewer viewFrame = new();
    public static Application app = new Application();
    //dictionary contains string for key and list of possible answers for value
    public static new List<(string Question, string[] Correct, string[] Wrong, string isCorrect, string isWrong)> que = new List<(string Question, string[] Correct, string[] Wrong, string isCorrect, string isWrong)>
        {
            (
                "Which of the following are cybersecurity threats?",
                new[] { "Phishing", "Ransomware", "Malware", "DDoS attack" },
                new[] { "Firewall", "Encryption", "VPN", "Antivirus software" },
                "Thats right, that is a type of attack",
                "Thats incrrect that is a security feature"
            ),
            (
                "Which are strong password practices?",
                new[] { "Use symbols", "Use numbers", "Use uppercase letters", "Use long passwords" },
                new[] { "Use your name", "Use '123456'", "Reuse passwords everywhere", "Write it on paper openly" },
                "Thats right, adding that can make it harder for attackers to get your details",
                "Thats incorrect, that will make it easier for attackers to get your passwords"
            ),
            (
                "Which of these are types of malware?",
                new[] { "Virus", "Trojan horse", "Worm", "Spyware" },
                new[] { "Router", "Firewall", "VPN", "Encryption" },
                "Thats right, that is a type of malware",
                "Thats incorrect, that is a way to protect yourself from malware"
            ),
            (
                "Which actions help protect against phishing?",
                new[] { "Check sender email", "Avoid suspicious links", "Verify website URL", "Use email filters" },
                new[] { "Click all links", "Share passwords", "Ignore security warnings", "Open unknown attachments" },
                "Thats right, that will help sprotect you against phishing attacks",
                "Thats incorrect, that will only make an attach more likely to happen"
            ),
            (
                "Which are features of a firewall?",
                new[] { "Blocks unauthorized access", "Monitors traffic", "Filters network data", "Enforces security rules" },
                new[] { "Stores passwords", "Cleans viruses", "Increases RAM", "Generates WiFi signals" },
                 "Thats right, that is not a firewall feature",
                "Thats incorrect, that is a way to protect yourself from malware"
            ),
            (
                "Which of these use encryption?",
                new[] { "HTTPS websites", "VPN connections", "Online banking", "Secure messaging apps" },
                new[] { "Open WiFi", "Plain text emails", "Unsecured websites", "Local text files" },
                "Thats right, that uses encryption methods",
                "Thats incorrect, that does not use encryption methods"
            ),
            (
                "Which are examples of social engineering?",
                new[] { "Phishing emails", "Pretexting", "Baiting attacks", "Impersonation calls" },
                new[] { "Firewall setup", "Software updates", "Password hashing", "System backup" },
                "Thats right, that is an example of social engineering",
                "Thats incorrect, that is not a form of social engineering"
            ),
            (
                "Which are benefits of using a VPN?",
                new[] { "Hides IP address", "Encrypts traffic", "Protects public WiFi usage", "Improves privacy" },
                new[] { "Removes viruses", "Increases CPU speed", "Deletes spam emails", "Repairs hardware" },
                "Thats right, that is a benifit of a vpn",
                "Thats incorrect, that is not a benifit of a vpn"
            ),
            (
                "Which are signs of malware infection?",
                new[] { "Slow performance", "Unexpected pop-ups", "Unknown programs installed", "Frequent crashes" },
                new[] { "Faster internet", "Improved battery life", "Cleaner desktop", "More storage space" },
                "Thats right, that is a malware infection",
                "Thats incorrect, that is not an example of a malware infection"
            ),
            (
                "Which practices improve cybersecurity?",
                new[] { "Enable 2FA", "Update software regularly", "Use antivirus", "Backup data" },
                new[] { "Disable updates", "Use weak passwords", "Share login details", "Ignore security alerts" },
                                "Thats right, that practice does improve cyber security",
                "Thats incorrect, that is not a practice to improve cyber security"
            ),
            (
                "Which of the following are examples of authentication methods?",
                new[] { "Password", "Fingerprint", "Facial recognition", "Security token" },
                new[] { "Firewall", "Router", "VPN", "USB flash drive" },
                        "Thats right, that is an example of an authentication method",
                "Thats incorrect, that that is not an example of an authentication method"
            ),
            (
                "Which is a good email security practice?",
                new[] { "Verify the sender", "Scan attachments", "Use spam filters", "Avoid suspicious links" },
                new[] { "Open every attachment", "Share your password", "Ignore security warnings", "Click unknown links" },
                        "Thats right, that is good email security practices?",
                "Thats incorrect, that is not a good practice and exposes you to threats"
            )
        };
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
                { "what can i ask about", new List<string>{ "You can ask about: what is\npassword safety\nphishing\nsafe browsing\nyour purpose\nYou can also tell me what your interested in by saying:\nim interested in blank\nor i like blank\nand i will remember it\nyou can also play a quiz game but saying, play game, to test your cyber security knowledge\nor store tasks in the database by saying add task\nupdate task\ndelete task\nor view task to view all the tasks youve made." } },
                {
                  "what is your purpose", new List<string>{
                  "My purpose is to educate you on all matter of digital safety.",
                  "My purpose is to help you learn and navigate threats in the digital space.",
                  "My role is to help people understand the digital world so they can remain safe."
                } },
            };
    public static List<string> userInterests = new() { };
    public static List<string> comfortText = new() { "im sorry i know how frustrating", "its ok to be worried about", "dont stress about it ill teach you tips so you can stay safe." };

    public static Dictionary<DateTime, string> log = new();
    [STAThread]
    static void Main(string[] args)
    {
        MySqlClass.openCon();
        GUI frame = new();

        //Welcome audio and diction variable
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
                welcome.PlaySync();
                BotSpeak("What is your name", frame);
            });

            string name = await frame.RunConversationAsync();

            if (string.IsNullOrEmpty(name))
            {
                BotSpeak("Hi friend Welcome to Cuber Help", frame);
            }
            else
            {
                BotSpeak("Hi " + name + " Welcome to Cyber Help", frame);
            }

            BotSpeak("If your ever lost just ask: what can i ask about", frame);

            string que = await frame.RunConversationAsync();
            
            Ask(que, dictionary, frame);
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
        string[] rem = { "interested in", "i like" };
        string[] senti = { "worried", "frustrated", "scared" };

        if(question.Contains("play") || question.Contains("game"))
        {
            await PlayGame(question, frame);
            return;
        }
       
        if(question.Contains("add") || question.Contains("new") || question.Contains("update") || question.Contains("delete") || question.Contains("view"))
        {
            await MySqlCrud(question, frame);
            return;
        }
   

        if (list.ContainsKey(question))//answers direct questions
        {
            addLog("asked question");
            List<string> listAns = list[question];
            int desc = random.Next(listAns.Count());

            BotSpeak(listAns[desc], frame);

            string newQ = await frame.RunConversationAsync();
            if (knowMore.Any(m => newQ.Contains(m)))
            {
                Ask(question, list, frame);
            }
            Ask(newQ, list, frame);

        }
        else if (rem.Any(m => question.Contains(m)))//remember topics liked
        {
            addLog("stored topic of interest");
            if (check.Any(m => question.Contains(m)))
            {
                string search = check.FirstOrDefault(m => question.Contains(m));
                BotSpeak("That is super cool! I will remember that. Heres a cool fact I know about " + search, frame);
                foreach (string match in list.Keys)
                {
                    if (match.Contains(search))
                    {
                        List<string> listAns = list[match];
                        int choice = random.Next(listAns.Count());

                        BotSpeak(listAns[choice], frame);

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
        else //answers with comfort text and keywords
        {
            if (check.Any(m => question.Contains(m)))
            {
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
                    addLog("Quit program");
                    BotSpeak("You have been learning with Cyber Bot, GoodBye!", frame);
                    MySqlClass.closeCon();
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

    private static async Task PlayGame(string question, GUI frame)
    {
        if (question.Equals("play") || question.Equals("game") || question.Equals("quiz"))
        {
            addLog("Started new game");
            int points = 0;
            foreach (var gameQ in que)
            {
                string q = gameQ.Question;
                var rnd = new Random();
                int num = rnd.Next(gameQ.Correct.Length);
                string[] letters = { "a", "b", "c", "d" };
                List<string> options = gameQ.Wrong
                    .OrderBy(x => rnd.Next())   // Shuffle wrong answers
                    .Take(3)                    // Take 3 random wrong ones
                    .Append(gameQ.Correct[num]) // Add 1 correct answer
                    .OrderBy(x => rnd.Next())   // Shuffle all 4 options
                    .ToList();
                q += "\na: " + options[0];
                q += "\nb: " + options[1];
                q += "\nc: " + options[2];
                q += "\nd: " + options[3];
                BotSpeak(q, frame);
                string ans = await frame.RunConversationAsync();
                int m = options.IndexOf(gameQ.Correct[num]);
                if (ans.Contains(gameQ.Correct[num].ToLower()) || ans.Equals(letters[m]))
                {
                    BotSpeak("That is correct", frame);
                    BotSpeak(gameQ.isCorrect, frame);
                    points++;
                }
                else
                {
                    BotSpeak("That is incorrect", frame);
                    BotSpeak(gameQ.isWrong, frame);
                }
            }
            if (points <= 3)
            {
                BotSpeak($"your score is {points}/12. You need to learn more about cyber security =(", frame);
            }
            if (points >= 4 && points <= 6)
            {
                BotSpeak($"your score is {points}/12. That wasnt bad but you can do better =/, lets go learn more!", frame);
            }
            if (points >= 7 && points <= 9)
            {
                BotSpeak($"your score is {points}/12. That was awesome you basically a pro =), but still room for improvement!", frame);
            }
            if (points >= 10 && points <= 12)
            {
                BotSpeak($"your score is {points}/12. That was great your the best =D, Dont forget threats are always advancing so dont stop here!!", frame);
            }
            addLog($"Ended new game with score of {points}/12");
        }
    }

    private static async Task MySqlCrud(string question, GUI frame)
    {
        if (question.Contains("view tasks"))
        {
            addLog("tasks viewed");
            ShowTasks();
            if (viewFrame != null && viewFrame.IsLoaded)
            {
                viewFrame.Activate();
            }
            else
            {
                // The window is either closed or was never opened. 
                // Create a fresh instance to open it again.
                viewFrame = new DataViewer();
                viewFrame.Show(); //
            }
            viewFrame.Show();
            BotSpeak("here is you tasks, what would you like to do now?",frame);
            string que = await frame.RunConversationAsync();
            Ask(que, dictionary, frame);
            return;
        }
        else if (question.Contains("add task"))
        {
            BotSpeak("What is the title?",frame);
            string title = await frame.RunConversationAsync();

            while (String.IsNullOrEmpty(title))
            {
                BotSpeak("Please enter a title.", frame);
                title = await frame.RunConversationAsync();
            }

            BotSpeak("What is the description?", frame);
            string desc = await frame.RunConversationAsync();
            while (String.IsNullOrEmpty(desc))
            {
                BotSpeak("Please enter the description.", frame);
                desc = await frame.RunConversationAsync();
            }

            BotSpeak("what is the date in must be completed if you have one if not just skip?\nuse dd/mm/yyyy format.", frame);
            string remDate = await frame.RunConversationAsync();
            BotSpeak("What is the time it must be completed if you have one if not just skip?\nuse HH/MM format.", frame);
            string remTime = await frame.RunConversationAsync();

            DateOnly? dDate = null;
            TimeOnly? tTime = null;

            if (DateOnly.TryParse(remDate, out DateOnly parsedDate))
            {
                dDate = parsedDate;
            }

            if (TimeOnly.TryParse(remTime, out TimeOnly parsedTime))
            {
                tTime = parsedTime;
            }
            BotSpeak(remDate + "   " + remTime, frame);
            AddNewTask(title, desc, dDate, tTime);
            BotSpeak("Task successfully added!, what would you like to do now?", frame);
            addLog("new task added");
            string que = await frame.RunConversationAsync();
            Ask(que, dictionary, frame);
            return;
        }
        else if(question.Equals("update task"))
        {
            BotSpeak("Please enter the ID or the Title of the task you want to update", frame);
            var identify = await frame.RunConversationAsync();

            if (int.TryParse(identify.Trim(), out int id))
            {
                BotSpeak("How would you like to mark it?\nCompleted, Incomplete, Pending", frame);
                string stat = await frame.RunConversationAsync();

                int check = UpdateTaskStatus(stat, $"Id = {id}");
                if (check > 0)
                {
                    BotSpeak("Update successful", frame);
                    addLog("task updated");
                }
                else
                {
                    BotSpeak("Sorry i dont think that task exists, update failed", frame);
                    addLog("task update failed");
                }
            }
            else
            {
                string tit = identify;

                BotSpeak("How would you like to mark it?\nCompleted, Incomplete, Pending", frame);
                string stat = await frame.RunConversationAsync();

                int check = UpdateTaskStatus(stat, $"Title = '{tit}'");
                if (check > 0)
                {
                    BotSpeak("Update successful", frame);
                    addLog("task updated");
                }
                else
                {
                    BotSpeak("Sorry i dont think that task exists, update failed", frame);
                    addLog("task update failed");
                }
            }

            BotSpeak("What would you like to do now?", frame);
            string que = await frame.RunConversationAsync();
            Ask(que, dictionary, frame);
            return;
        }
        else if(question.Equals("delete task"))
        {
            BotSpeak("Please enter the ID or the Title of the task you want to delete", frame);
            string del = await frame.RunConversationAsync();
            try
            {
                int id = Int32.Parse(del);
                BotSpeak("Are you sure you want to dolete this task Y/N", frame);
                string ans= await frame.RunConversationAsync();
                
                if (ans.Equals("y") || ans.Equals("yes"))
                {
                    int check = DeleteTask( $"Id = {id}");
                    if (check > 0)
                    {
                        BotSpeak("Deleted task successfully", frame);
                        addLog("task deleted");
                    }
                    else
                    {
                        BotSpeak("Sorry i dont think that task exists, update failed", frame);
                        addLog("task deletion failed");
                    }

                }
                if (ans.Equals("n") || ans.Equals("no"))
                {
                    BotSpeak("Deletion aborted", frame);
                }

                BotSpeak("What would you like to do now?", frame);
                string que = await frame.RunConversationAsync();
                Ask(que, dictionary, frame);
                return;
            }
            catch (Exception e)
            {
                string tit = del.ToString();
                BotSpeak("Are you sure you want to delete this task Y/N", frame);
                string ans = await frame.RunConversationAsync();

                if (ans.Equals("y") || ans.Equals("yes"))
                {
                    int check = DeleteTask($"Title = '{tit}'");
                    if (check > 0)
                    {
                        BotSpeak("Deleted task successfully", frame);
                        addLog("task deleted");
                    }
                    else
                    {
                        BotSpeak("Sorry i dont think that task exists, update failed", frame);
                        addLog("task deletion failed");
                    }

                }
                if (ans.Equals("n") || ans.Equals("no"))
                {
                    BotSpeak("Deletion aborted", frame);
                }
                BotSpeak("What would you like to do now?", frame);
                string que = await frame.RunConversationAsync();
                Ask(que, dictionary, frame);
                return;
            }
        }
    }

    public static void BotSpeak(string text, GUI frame)
    {
        frame.Dispatcher.Invoke(() =>
        {
            TextBlock botMsg = new(); // create text block
            botMsg.Inlines.Add(new Run("Bot: ")
            {
                Foreground = Brushes.Green
            });
            botMsg.Inlines.Add(new Run(text));
            botMsg.Margin = new Thickness(10);
            botMsg.TextWrapping = TextWrapping.Wrap;
            frame.msgView.Children.Add(botMsg); //adds text block to stack panel
            Func<Task> speak = async () => await Task.Run(() => TextToSpeech.Speak(text));
            speak();
        });
    }

    public static void viewLog()
    {
        addLog("viewed log");
        string l = "";
        foreach (var (key,value) in log)
        {
            l += "time: " + key + "     Action:" + value.ToString();
        }
    }
    public static void addLog(string action)
    {
        DateTime d = DateTime.Now;
        log.Add(d, action);
    }


}
