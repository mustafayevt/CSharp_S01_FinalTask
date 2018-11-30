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
        List<EmployerAnnouncement> eAnnouncements = new List<EmployerAnnouncement>();
        
    }
    class EmployerAnnouncement
    {
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
    }
    
}
