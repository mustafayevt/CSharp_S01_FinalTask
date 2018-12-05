using Newtonsoft.Json;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_S01_FinalTask
{
    class Program
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            //Loading Program


            Console.CursorVisible = false;
            Thread AnimatedTitleThread = new Thread(AnimatedTitle);
            AnimatedTitleThread.Start("Main Page");

            Thread ShowWindowThread = new Thread(ShowWindow);
            ShowWindowThread.Start();

            List<Employer> employers = new List<Employer>();
            List<Worker> workers = new List<Worker>();
            //FILE SYSTEM
            if (File.Exists("Employers.json") && File.Exists("Workers.json"))
            {
                string jsonEmployers = File.ReadAllText("Employers.json");
                employers = JsonConvert.DeserializeObject<List<Employer>>(jsonEmployers);
                if (employers != null)
                {
                    foreach (var item in employers)
                    {
                        foreach (var item2 in item.eAnnouncements)
                        {
                            if (item2 != null)
                            {
                                EmployerAnnouncement.id = item2.Announcment_ID;
                            }
                        }
                    }
                }
                
                string jsonWorkers = File.ReadAllText("Workers.json");
                workers = JsonConvert.DeserializeObject<List<Worker>>(jsonWorkers);
                if (workers != null)
                {
                    foreach (var item in workers)
                    {
                        if(item.wAnnouncement!= null)
                        {
                            WorkerAnnouncement.id = item.wAnnouncement.CV_ID;
                        }
                    }
                }
            }
            /////////////////
            int LoggedAs = 0;
            int Logged = 0;
        MainPoint:
            Console.ForegroundColor = ConsoleColor.Cyan;
            int MainChoice = 0;
            string[] choices = { "Exit", "Sign In", "Sign Up" };
            ConsoleKeyInfo info;
            while (true)
            {
                Console.Clear();
                ShowChoices(MainChoice, choices);
                info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter) break;
                if (info.Key == ConsoleKey.RightArrow)
                {
                    if (MainChoice < 2) MainChoice++;
                }
                if (info.Key == ConsoleKey.LeftArrow)
                {
                    if (MainChoice > 0) MainChoice--;
                }
            }
            //Operations Screen
            switch (MainChoice)
            {
                case 1:
                    {
                        AnimatedTitleThread.Abort();
                        Thread thread = new Thread(AnimatedTitle);
                        thread.Start("Sign In Page");
                        if (Console.CapsLock)
                        {
                            Console.Beep();
                            Console.WriteLine("Caps Lock is active!");
                        }
                        Console.WriteLine("Enter Username:");
                        var uname = Console.ReadLine();
                        Console.WriteLine("Enter Password:");
                        var pass = Console.ReadLine();
                        foreach (var item in workers)
                        {
                            if (item.Username == uname && item.Password == pass)
                            {
                                logger.Info($"User - {item.Username} is logged in system");
                                LoggedAs = 1;
                                Logged = item.GetHashCode();
                                if (!LoggedIn.ShowLoggedInMenuForWorker(employers, workers, Logged))
                                {
                                    Logged = 0;
                                    LoggedAs = 0;
                                    goto MainPoint;
                                }
                            }
                        }
                        foreach (var item in employers)
                        {
                            if (item.Username == uname && item.Password == pass)
                            {
                                logger.Info($"User - {item.Username} is logged in system");
                                LoggedAs = 2;
                                Logged = item.GetHashCode();
                                if (!LoggedIn.ShowLoggedInMenuForEmployer(employers, workers, Logged))
                                {
                                    Logged = 0;
                                    LoggedAs = 0;
                                    goto MainPoint;
                                }
                            }
                        }
                        Console.WriteLine("Not User Found, press any key for return main menu");
                        Console.ReadKey();
                        break;
                    }
                case 2:
                    {
                        AnimatedTitleThread.Abort();
                        Thread thread = new Thread(AnimatedTitle);
                        thread.Start("Sign Up Page");
                        if (Console.CapsLock)
                        {
                            Console.Beep();
                            Console.WriteLine("Caps Lock is active!");
                        }
                        User tmp = User.Register(employers, workers);
                        if (tmp.RegisterAs == RegisterAs.WORKER)
                        {
                            workers.Add(new Worker(tmp));
                            Logged = workers.Last().GetHashCode();
                            LoggedAs = 1;
                            logger.Info($"User - {workers.Last().Username} registered successfully");
                        }
                        else
                        {
                            employers.Add(new Employer(tmp));
                            Logged = employers.Last().GetHashCode();
                            LoggedAs = 2;
                            logger.Info($"User - {employers.Last().Username} registered successfully");
                        }
                        Console.WriteLine("Sign Up is Successful");
                        if (LoggedAs == 1)
                        {

                            if (!LoggedIn.ShowLoggedInMenuForWorker(employers, workers, Logged))
                            {
                                Logged = 0;
                                LoggedAs = 0;
                                goto MainPoint;
                            }
                        }
                        else if (LoggedAs == 2)
                            if (!LoggedIn.ShowLoggedInMenuForEmployer(employers, workers, Logged))
                            {
                                Logged = 0;
                                LoggedAs = 0;
                                goto MainPoint;
                            }
                        break;
                    }
                case 0:
                    {
                        string jsonEmployer = JsonConvert.SerializeObject(employers);

                        //write string to file
                        System.IO.File.WriteAllText("Employers.json", jsonEmployer);

                        string jsonWorker = JsonConvert.SerializeObject(workers);

                        System.IO.File.WriteAllText("Workers.json", jsonWorker);
                        AnimatedTitleThread.Abort();
                        Thread thread = new Thread(AnimatedTitle);
                        thread.Start("Bye Bye");
                        while (thread.IsAlive){}
                        Environment.Exit(0);
                        break;
                    }
                default:
                    Console.WriteLine("There is no command like this, try again\nPress any key");
                    Console.ReadKey();
                    break;
            }
            goto MainPoint;

        }


        static void AnimatedTitle(object ProgresbarTmp)
        {
            string Progresbar = ProgresbarTmp as string;
            var title = "";
            for (int i = 0; i < Progresbar.Length; i++)
            {
                title += Progresbar[i];
                Console.Title = title;
                Thread.Sleep(400);
            }

        }


        public static void ShowChoices(int current, string[] choices)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                if (current == i)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                Console.Write(choices[i]);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("   ");
            }
            Console.WriteLine("\n\t<--  -->");
        }
        static void ShowWindow()
        {
            for (int i = 1; i < 30; i++)
            {
                Console.SetWindowSize(i + 80, i);
                Thread.Sleep(90);
            }
        }

    }
}
