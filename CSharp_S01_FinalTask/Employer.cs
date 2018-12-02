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
        public List<EmployerAnnouncement> eAnnouncements = new List<EmployerAnnouncement>();
        public Dictionary<uint, uint> Coming = new Dictionary<uint, uint>();

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
        public decimal Salary { get; set; }
        public int Age { get; set; }
        public Education Education { get; set; }
        public Experience Experience { get; set; }
        public string pNumber { get; set; }
        public void Show()
        {
            Console.WriteLine(AnnouncementName);
                Console.WriteLine(CompanyName);
                Console.WriteLine($"{Age}");
                Console.WriteLine(Category.ToString());
                Console.WriteLine(City.ToString());
                Console.WriteLine(Description);
                Console.WriteLine(Education.ToString());
                Console.WriteLine(Experience.ToString());
                Console.WriteLine(Salary);
                Console.WriteLine(pNumber);
            
        }
    }
    
}
