using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_S01_FinalTask
{
    class Worker:User
    {
        public Worker(User user)
            :base(user)
        {

        }
        public WorkerAnnouncement wAnnouncement = null;

        public void Show()
        {
            Console.WriteLine($@"
Name         - {wAnnouncement.Name}
Surname      - {wAnnouncement.Surname}
Age          - {wAnnouncement.Age}
Education    - {wAnnouncement.Education.ToString()}
Experience   - {wAnnouncement.Experience.ToString()}
Category     - {wAnnouncement.Category.ToString()}
Min Salarey  - {wAnnouncement.MinSalarey}
Phone Number - {wAnnouncement.pNumber}");
        }

        public static WorkerAnnouncement AddCV()
        {
            WorkerAnnouncement newAnnouncement = new WorkerAnnouncement();
            WorkerAnnouncement.id++;
            newAnnouncement.CV_ID = WorkerAnnouncement.id;
            Console.WriteLine("Enter Name");
            newAnnouncement.Name = Console.ReadLine();
            Console.WriteLine("Enter Surname");
            newAnnouncement.Surname = Console.ReadLine();
            Console.WriteLine("Enter Age");
            AgePoint:
            if(!int.TryParse(Console.ReadLine(),out int age))
            {
                Console.WriteLine("Wrong Age, Enter again");
                goto AgePoint;
            }
            newAnnouncement.Age = age;

            EducationPoint:
            Console.WriteLine("Enter Your Education");
            Console.WriteLine($"{Education.MIDDLE.ToString()} - 1");
            Console.WriteLine($"{Education.INCOMPELETEHIGHER.ToString()} - 2");
            Console.WriteLine($"{Education.HIGER.ToString()} - 3");
            if(!int.TryParse(Console.ReadLine(),out int edu))
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
            Console.WriteLine("Enter Your Experience");
            Console.WriteLine($"Less Then yead - 1");
            Console.WriteLine($"One Year to Three Year - 2");
            Console.WriteLine($"Three Year to Five Year - 3");
            Console.WriteLine($"Five Year or High - 4");
            if (!int.TryParse(Console.ReadLine(), out int exp))
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto EducationPoint;
            }
            if (edu > 4 || edu < 1)
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto ExperiencePoint;
            }
            newAnnouncement.Experience = (Experience)exp;
            //
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
            if (edu > 4 || edu < 1)
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto CategoryPoint;
            }
            newAnnouncement.Category = (Category)catg;
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
            if (edu > 3 || edu < 1)
            {
                Console.WriteLine("Wrong Choice, Enter Again");
                goto CityPoint;
            }
            newAnnouncement.City = (City)city;
            SalaryPoint:
            Console.WriteLine("Enter Minimum Slary");
            if(!int.TryParse(Console.ReadLine(),out int Salary))
            {
                Console.WriteLine("Wrong Salary Format, Enter Again");
                goto SalaryPoint;
            }
            newAnnouncement.MinSalarey = Salary;
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
    class WorkerAnnouncement
    {
        public static uint id = 0;
        public uint CV_ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public Education Education { get; set; }
        public Experience Experience { get; set; }
        public City City { get; set; }
        public Category Category { get; set; }
        public decimal MinSalarey { get; set; }
        public string pNumber { get; set; }
    }
}
