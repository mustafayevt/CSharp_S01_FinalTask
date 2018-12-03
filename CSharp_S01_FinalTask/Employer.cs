using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_S01_FinalTask
{
    class Employer : User
    {
        public Employer(User user)
            :base(user)
        {

        }
        public Employer()
        {

        }
        public Dictionary<uint, List<uint>> Coming = new Dictionary<uint, List<uint>>();        
        public List<EmployerAnnouncement> eAnnouncements = new List<EmployerAnnouncement>();
        //key = Employer announcement
        //value = Worker cv (List<uint>)
        public static EmployerAnnouncement addAnnouncement()
        {
            EmployerAnnouncement newAnnouncement = new EmployerAnnouncement();
            EmployerAnnouncement.id++;
            newAnnouncement.Announcment_ID = EmployerAnnouncement.id;
            Console.WriteLine("Enter Announcement Name:");
            newAnnouncement.AnnouncementName = Console.ReadLine();
            Console.WriteLine("Enter Company Name:");
            newAnnouncement.CompanyName = Console.ReadLine();
            Console.WriteLine("Enter Category:");
        CategoryPoint:
            Console.WriteLine("Enter Category");
            Console.WriteLine($"{Category.DOCTOR.ToString()} - 1");
            Console.WriteLine($"{Category.JOURNALIST.ToString()} - 2");
            Console.WriteLine($"{Category.ITPRO.ToString()} - 3");
            Console.WriteLine($"{Category.TRANSLATOR.ToString()} - 4");
            if (!int.TryParse(Console.ReadLine(), out int catg))
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto CategoryPoint;
            }
            if (catg > 4 || catg < 1)
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto CategoryPoint;
            }
            newAnnouncement.Category = (Category)catg;
            Console.WriteLine("Enter Description");
            newAnnouncement.Description = Console.ReadLine();
        CityPoint:
            Console.WriteLine("Enter City");
            Console.WriteLine($"{City.BAKU.ToString()} - 1");
            Console.WriteLine($"{City.SUMQAYIT.ToString()} - 2");
            Console.WriteLine($"{City.GENCE.ToString()} - 3");
            if (!int.TryParse(Console.ReadLine(), out int city))
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto CityPoint;
            }
            if (city > 3 || city < 1)
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto CityPoint;
            }
            newAnnouncement.City = (City)city;
        SalaryPoint:
            Console.WriteLine("Enter Minimum Slary");
            if (!int.TryParse(Console.ReadLine(), out int Salary))
            {
                Console.WriteLine("Wrong Salary Format, Enter Again");
                goto SalaryPoint;
            }
            newAnnouncement.Salary = Salary;

            Console.WriteLine("Enter Age");
        AgePoint:
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Wrong Age, Enter again");
                goto AgePoint;
            }
            newAnnouncement.Age = age;

        EducationPoint:
            Console.WriteLine("Enter Education");
            Console.WriteLine($"{Education.MIDDLE.ToString()} - 1");
            Console.WriteLine($"{Education.INCOMPELETEHIGHER.ToString()} - 2");
            Console.WriteLine($"{Education.HIGER.ToString()} - 3");
            if (!int.TryParse(Console.ReadLine(), out int edu))
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto EducationPoint;
            }
            if (edu > 3 || edu < 1)
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto EducationPoint;
            }
            newAnnouncement.Education = (Education)edu;

        ExperiencePoint:
            Console.WriteLine("Enter Experience");
            Console.WriteLine($"Less Then yead - 1");
            Console.WriteLine($"One Year to Three Year - 2");
            Console.WriteLine($"Three Year to Five Year - 3");
            Console.WriteLine($"Five Year or High - 4");
            if (!int.TryParse(Console.ReadLine(), out int exp))
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto EducationPoint;
            }
            if (exp > 4 || exp < 1)
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto ExperiencePoint;
            }
            newAnnouncement.Experience = (Experience)exp;

        pNumberPoint:
            Console.WriteLine("Enter Mobile Number (554776396)");
            string num = Console.ReadLine();
            if (!IsNumber(num))
            {
                Console.WriteLine("Phone Format is Wrong, Enter Again");
                goto pNumberPoint;
            }
            newAnnouncement.pNumber = num;
            return newAnnouncement;
        }
    }
    class EmployerAnnouncement
    {
        public static uint id = 0; 
        public uint Announcment_ID { get; set; }
        public string AnnouncementName { get; set; }
        public string CompanyName { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public City City { get; set; }
        public int Salary { get; set; }
        public int Age { get; set; }
        public Education Education { get; set; }
        public Experience Experience { get; set; }
        public string pNumber { get; set; }
        public void Show()
        {
            Console.WriteLine($@"
Company Name - {CompanyName}
Age          - {Age}
Category     - {Category.ToString()}
Description  - {Description}
City         - {City.ToString()}
Salary       - {Salary}
Educatioin   - {Education.ToString()}
Experience   - {Experience.ToString()}
Phone Number - {pNumber}");
        }
    }
    
}
