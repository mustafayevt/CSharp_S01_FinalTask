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

        public static User Register()
        {
            User newUser = new User();
        }
    }
    public enum Sex { MALE=1, FEMALE };
    public enum Experience { LESSYEAR=1, ONETOTHREE, THREETOFIVE, HIGHERFIVE };
    public enum City { BAKU=1, SUMQAYIT, GENCE };
    public enum Education { MIDDLE=1,  INCOMPELETEHIGHER, HIGER };
    public enum Category { DOCTOR=1, JOURNALIST, ITPRO, TRANSLATOR };
    public enum RegisterAs { WORKER=1, EMPLOYER };
}
