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
        WorkerAnnouncement wAnnouncement = null;
    }
    class WorkerAnnouncement
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public Education Education { get; set; }
        public Experience Experience { get; set; }
        public City City { get; set; }
        public int MinSalarey { get; set; }
        public string pNumber { get; set; }
    }
}
