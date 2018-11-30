using System;
using System.Collections.Generic;
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
            for (int i = 1; i < 30; i++)
            {
                Console.SetWindowSize(i + 80, i);

                Thread.Sleep(40);
            }
            List<Employer> employers = new List<Employer>();
            List<Worker> workers = new List<Worker>();
            int LoggedAs = 0;
            int Logged = 0;
        MainPoint:
            AnimatedTitle("Main Page");
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
                        AnimatedTitle("Sign In Page");
                        if (Console.CapsLock)
                        {
                            Console.Beep();
                            Console.WriteLine("Caps Lock is active!");
                        }
                        break;
                    }
                case 2:
                    {
                        AnimatedTitle("Sign Up Page");
                        if (Console.CapsLock)
                        {
                            Console.Beep();
                            Console.WriteLine("Caps Lock is active!");
                        }
                        Console.Clear();
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
                        Console.ReadKey();
                        if (LoggedAs == 1)
                            ShowLoggedInMenuForWorker(employers,workers, Logged);
                        //else if (LoggedAs == 2)
                            //ShowLoggedInMenuForEmployer(employers, Logged);
                        break;
                    }
                case 0:
                    {
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


        static void AnimatedTitle(string Progresbar)
        {
            var title = "";
            for (int i = 0; i < Progresbar.Length; i++)
            {
                title += Progresbar[i];
                Console.Title = title;
                Thread.Sleep(100);
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
        }

        static void ShowLoggedInMenuForWorker(List<Employer> employers, List<Worker> workers, int logged)
        {
        ReturnMain:
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
                        break;
                    }
                case 1:
                    {
                        //if(workers.Find(x=>x.GetHashCode()==logged).wAnnouncement)
                        break;
                    }
                default:
                    break;
            }
        }
        static void SearchJob(List<Employer>employers, List<Worker> workers,int logged)
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
                            item2.Show();
                        }
                    }
                }
            }
        }
    }
}
