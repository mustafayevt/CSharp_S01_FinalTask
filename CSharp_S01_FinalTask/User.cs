using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSharp_S01_FinalTask
{
    class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RegisterAs RegisterAs { get; set; }
        public Regex pNumberPattern = new Regex(@"^[50|51|55|70|77]{2}[0-9]{7}$");

        public User(User tmp)
        {
            Username = tmp.Username;
            Email = tmp.Email;
            Password = tmp.Password;
            RegisterAs = tmp.RegisterAs;
        }

        public User()
        {

        }
        public override string ToString()
        {
            return $"{Username} " +
                $"{Email} ";
        }

        public static bool IsEmail(string mail)
        {
            string MailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            return Regex.IsMatch(mail, MailPattern);
        }
        
        public static bool IsPassword(string password)
        {
            string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            return Regex.IsMatch(password, PasswordPattern);
        }

        public static User Register(List<Employer>employers, List<Worker> workers)
        {
            User newUser = new User();
        UsernamePoint:
            Console.WriteLine("Enter Username");
            newUser.Username = Console.ReadLine();
            if (CheckUsername(newUser.Username, employers, workers))
            {
                Console.WriteLine("This Username Already Taken");
                goto UsernamePoint;
            }
        EmailPoint:
            Console.WriteLine("Enter Email:");
            newUser.Email = Console.ReadLine();
            if (!User.IsEmail(newUser.Email))
            {
                Console.WriteLine("Email Is Invalid!");
                goto EmailPoint;
            }
            if (CheckEmail(newUser.Email, employers, workers))
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
            newUser.RegisterAs = (RegisterAs)Convert.ToInt32(ChoicePro);
        PasswordPoint:
            Console.WriteLine("Enter Password");
            newUser.Password = Console.ReadLine();
            if (!User.IsPassword(newUser.Password))
            {
                Console.WriteLine("Password Is Invalid!");
                goto PasswordPoint;
            }
            Console.WriteLine("Enter Password Again");
            if (Console.ReadLine() != newUser.Password)
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
            return newUser;
        }
        static bool CheckUsername(string uname, List<Employer> employers, List<Worker> workers)
        {
            return employers.Exists(x => x.Username == uname) || workers.Exists(x => x.Username == uname);
        }

        static bool CheckEmail(string email, List<Employer> employers, List<Worker> workers)
        {
            return employers.Exists(x => x.Email == email) || workers.Exists(x => x.Email == email);
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

    }
    public enum Sex { MALE=1, FEMALE };
    public enum Experience { LESSYEAR=1, ONETOTHREE, THREETOFIVE, HIGHERFIVE };
    public enum City { BAKU=1, SUMQAYIT, GENCE };
    public enum Education { MIDDLE=1,  INCOMPELETEHIGHER, HIGER };
    public enum Category { DOCTOR=1, JOURNALIST, ITPRO, TRANSLATOR };
    public enum RegisterAs { WORKER=1, EMPLOYER };
}
