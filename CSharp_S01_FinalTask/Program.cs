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

                Thread.Sleep(20);
            }
            List<Employer> employers = new List<Employer>();
            List<Worker> workers = new List<Worker>();
            uint LoggedIn;
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
                            workers.Add(new Worker(tmp));
                        else employers.Add(new Employer(tmp));
                        Console.WriteLine("Sign Up is Successful");
                        LoggedIn = User.ID;
                        goto MainPoint;
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
        
    }
}
