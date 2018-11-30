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

                Thread.Sleep(30);
            }
            List<Employer> employers = new List<Employer>();
            List<Worker> workers = new List<Worker>();
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
                            Thread.Sleep(30);
                        }
                        Console.Clear();
                        UsernamePoint:
                        Console.WriteLine("Enter Username");
                        string username = Console.ReadLine();
                        if(CheckUsername(username, employers, workers))
                        {
                            Console.WriteLine("This Username Already Taken");
                            goto UsernamePoint;
                        }
                    EmailPoint:
                        Console.WriteLine("Enter Email:");
                        string mail = Console.ReadLine();
                        if (!User.IsEmail(mail))
                        {
                            Console.WriteLine("Email Is Invalid!");
                            goto EmailPoint;
                        }
                        if (CheckEmail(mail, employers, workers))
                        {
                            Console.WriteLine("This Email Used by Another User");
                            goto EmailPoint;
                        }
                    ChoicePoint:
                        Console.WriteLine("Worker - 1, Employer - 2");
                        string ChoicePro = Console.ReadLine();
                        if (ChoicePro != "1" && ChoicePro != "2")
                        {
                            Console.WriteLine("Wrong Choice");
                            goto ChoicePoint;
                        }
                    PasswordPoint:
                        Console.WriteLine("Enter Password");
                        string password = Console.ReadLine();
                        if (!User.IsPassword(password))
                        {
                            Console.WriteLine("Password Is Invalid!");
                            goto PasswordPoint;
                        }
                        Console.WriteLine("Enter Password Again");
                        if (Console.ReadLine() != password)
                        {
                            Console.WriteLine("Wrong Password");
                            goto PasswordPoint;
                        }
                    CAPTCHAPoint:
                        string CAPTCHA = GenerateCoupon(4);
                        Console.WriteLine($"Enter this CAPTCHA: {CAPTCHA}");
                        if (CAPTCHA != Console.ReadLine())
                        {
                            Console.WriteLine("Wrong CAPTCHA");
                            goto CAPTCHAPoint;
                        }

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

        
        public static string GenerateCoupon(int length)
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
        again:
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            if (!Regex.IsMatch(result.ToString(), @"^[a-zA-Z0-9]+$")) goto again;
            return result.ToString();
        }

        static bool CheckUsername(string uname, List<Employer> employers, List<Worker>workers)
        {
            return employers.Exists(x => x.Username == uname) || workers.Exists(x => x.Username == uname);
        }

        static bool CheckEmail(string email, List<Employer> employers, List<Worker> workers)
        {
            return employers.Exists(x => x.Email == email) || workers.Exists(x => x.Email == email);
        }
    }
}
