using Newtonsoft.Json;
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
                                LoggedAs = 1;
                                Logged = item.GetHashCode();
                                if (!ShowLoggedInMenuForWorker(employers, workers, Logged))
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
                                LoggedAs = 2;
                                Logged = item.GetHashCode();
                                if (!ShowLoggedInMenuForEmployer(employers, workers, Logged))
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
                        }
                        else
                        {
                            employers.Add(new Employer(tmp));
                            Logged = employers.Last().GetHashCode();
                            LoggedAs = 2;
                        }
                        Console.WriteLine("Sign Up is Successful");
                        if (LoggedAs == 1)
                        {

                            if (!ShowLoggedInMenuForWorker(employers, workers, Logged))
                            {
                                Logged = 0;
                                LoggedAs = 0;
                                goto MainPoint;
                            }
                        }
                        else if (LoggedAs == 2)
                            if (!ShowLoggedInMenuForEmployer(employers, workers, Logged))
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
                        AnimatedTitle("Bye Bye");
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


        static void ShowChoices(int current, string[] choices)
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

        static bool ShowLoggedInMenuForWorker(List<Employer> employers, List<Worker> workers, int logged)
        {
        ReturnMain:
            //Console.ReadKey();
            string[] Choices = { "Add CV", "Find Job", "Search", "Show My Info", "Show All Jobs", "Log Out" };
            int currentChoice = 0;
            ConsoleKeyInfo info;
            while (true)
            {
                Console.Clear();
                ShowChoices(currentChoice, Choices);
                info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter) break;
                if (info.Key == ConsoleKey.RightArrow)
                {
                    if (currentChoice < 5) currentChoice++;
                }
                if (info.Key == ConsoleKey.LeftArrow)
                {
                    if (currentChoice > 0) currentChoice--;
                }
            }
            Console.Clear();
            switch (currentChoice)
            {
                case 0:
                    {
                        if (workers.Find(x => x.GetHashCode() == logged).wAnnouncement == null)
                        {
                            workers.Find(x => x.GetHashCode() == logged).wAnnouncement = Worker.AddCV();
                        }
                        else
                        {
                            Console.WriteLine("You Already Added CV");
                            goto ReturnMain;
                        }
                        goto ReturnMain;
                    }
                case 1:
                    {
                        foreach (var item in workers)
                        {
                            if (item.GetHashCode() == logged)
                            {
                                var tmp = employers.Where(x => x.eAnnouncements.Any(s => s.Age == item.wAnnouncement.Age || s.Category == item.wAnnouncement.Category || s.City == item.wAnnouncement.City || s.Education == item.wAnnouncement.Education || s.Experience == item.wAnnouncement.Experience || s.Salary == item.wAnnouncement.MinSalarey));
                                foreach (var item1 in tmp)
                                {
                                    foreach (var item2 in item1.eAnnouncements)
                                    {
                                        Console.WriteLine("----------------------");
                                        item2.Show();
                                        Console.WriteLine("----------------------");
                                    }
                                }
                            }
                        }
                        goto ReturnMain;
                    }
                case 2:
                    {
                        Console.WriteLine(@"
Search for: 
            Category   - 1
            Education  - 2
            City       - 3
            Salary     - 4
            Experience - 5");
                        int choiceSearch = Convert.ToInt32(Console.ReadLine());
                        IEnumerable<EmployerAnnouncement> tmp = null;
                        switch (choiceSearch)
                        {
                            case 1:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.Category == item.wAnnouncement.Category));
                                        }
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.Education == item.wAnnouncement.Education));
                                        }
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.City == item.wAnnouncement.City));
                                        }
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.Salary == item.wAnnouncement.MinSalarey));
                                        }
                                    }
                                    break;
                                }
                            case 5:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.Experience == item.wAnnouncement.Experience));
                                        }
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        if (tmp != null)
                        {
                            foreach (var item in tmp)
                            {
                                item.Show();
                                Console.WriteLine("-------------------------------");
                            }
                        }
                        else Console.WriteLine("Not Found");
                        goto ReturnMain;
                    }
                case 3:
                    {
                        foreach (var item in workers)
                        {
                            if (item.GetHashCode() == logged && item.wAnnouncement != null)
                            {
                                item.Show(); goto ReturnMain;
                            }
                        }
                        Console.WriteLine("Not Found Any CV");
                        goto ReturnMain;
                    }
                case 4:
                    {
                    returnJobs:
                        var tmp = employers.SelectMany(x => x.eAnnouncements).ToList();
                        if (tmp.Count != 0)
                        {
                            int i =1;
                            foreach (var item in tmp)
                            {
                                Console.WriteLine($"{i++} - {item.AnnouncementName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Job Not Found");
                            goto ReturnMain;
                        }
                        int select = Convert.ToInt32(Console.ReadLine());
                        select--;
                        if (select >= tmp.Count)
                        {
                            Console.WriteLine("Out of Range");
                            goto returnJobs;
                        }
                        tmp[select].Show();
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("Send CV For This Job y/n");
                        var sendJob = Console.ReadLine();
                        if (sendJob.ToLower() == "y")
                        {
                            var cvID = workers.Find(x => x.GetHashCode() == logged).wAnnouncement.CV_ID;
                            uint eCvID=0;
                            foreach (var item in employers)
                            {
                                eCvID = item.eAnnouncements.Find(x => x.Announcment_ID == tmp[select].Announcment_ID).Announcment_ID;
                            }
                            foreach (var item in workers)
                            {
                                if (item.GetHashCode() == logged)
                                {
                                    foreach (var item2 in employers)
                                    {
                                        foreach (var item3 in item2.eAnnouncements)
                                        {
                                            if (item3.Announcment_ID == tmp[select].Announcment_ID)
                                            {
                                                item2.Coming.Add(new Dictionary<uint, uint>() { { item3.Announcment_ID, item.wAnnouncement.CV_ID } });
                                            }
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("Sended!");
                            goto ReturnMain;
                        }
                        else goto returnJobs;
                    }
                case 5:
                    {
                        return false;
                    }
                default:
                    break;
            }
            return false;
        }
        static bool ShowLoggedInMenuForEmployer(List<Employer> employers, List<Worker> workers, int logged)
        {
        ReturnMain:
            //Console.ReadKey();
            string[] Choices = { "Add CV", "Work Applications", "Log Out" };
            int currentChoice = 0;
            ConsoleKeyInfo info;
            while (true)
            {
                Console.Clear();
                ShowChoices(currentChoice, Choices);
                info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter) break;
                if (info.Key == ConsoleKey.RightArrow)
                {
                    if (currentChoice < 2) currentChoice++;
                }
                if (info.Key == ConsoleKey.LeftArrow)
                {
                    if (currentChoice > 0) currentChoice--;
                }
            }
            switch (currentChoice)
            {
                case 0:
                    {
                        employers.Find(x => x.GetHashCode() == logged).eAnnouncements.Add(Employer.addAnnouncement());
                        goto ReturnMain;
                    }
                case 1:
                    {
                        bool checkExist = false;
                        foreach (var item in employers.Find(x => x.GetHashCode() == logged).Coming)
                        {
                            foreach (var key in item)
                            {
                                foreach (var item2 in employers.Find(x => x.GetHashCode() == logged).eAnnouncements)
                                {
                                    if (key.Key == item2.Announcment_ID)
                                    {
                                        var tmp = workers.Find(s => s.wAnnouncement.CV_ID == key.Value).wAnnouncement;
                                        Console.WriteLine("-----------------------------");
                                        Console.WriteLine($@"Announcement:");
                                        item2.Show();
                                        //Console.WriteLine("-----------------------------");
                                        Console.BackgroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("\nWorker:\n");
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        tmp.Show();
                                        Console.WriteLine("-----------------------------");
                                        checkExist = true;
                                    }
                                }
                            }
                        }
                        if (!checkExist)
                        {
                            Console.WriteLine("Empty");
                            Console.ReadKey();
                        }
                        goto ReturnMain;
                    }
                case 2:
                    {
                        return false;
                    }
                default:
                    break;
            }
            Console.Clear();
            return false;
        }
    }
}
