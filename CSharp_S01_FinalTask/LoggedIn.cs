using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp_S01_FinalTask
{
    static class LoggedIn
    {

        public static bool ShowLoggedInMenuForWorker(List<Employer> employers, List<Worker> workers, int logged)
        {
            int i = 0;
        ReturnMain:
            if (i > 0)
                Console.ReadKey();
            i++;
            string[] Choices = { "Add CV", "Find Job", "Search", "Show My Info", "Show All Jobs", "Log Out" };
            int currentChoice = 0;
            ConsoleKeyInfo info;
            while (true)
            {
                Console.Clear();
                Program.ShowChoices(currentChoice, Choices);
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
            Console.Clear();
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
                        goto ReturnMain;
                    }
                case 1:
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
                                        Console.WriteLine("----------------------");
                                        item2.Show();
                                        Console.WriteLine("----------------------");
                                    }
                                }
                            }
                        }
                        goto ReturnMain;
                    }
                case 2:
                    {
                        Console.WriteLine(@"
Search for: 
            Category   - 1
            Education  - 2
            City       - 3
            Salary     - 4
            Experience - 5");
                        int choiceSearch = Convert.ToInt32(Console.ReadLine());
                        IEnumerable<EmployerAnnouncement> tmp = null;
                        switch (choiceSearch)
                        {
                            case 1:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.Category == item.wAnnouncement.Category));
                                        }
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.Education == item.wAnnouncement.Education));
                                        }
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.City == item.wAnnouncement.City));
                                        }
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.Salary == item.wAnnouncement.MinSalarey));
                                        }
                                    }
                                    break;
                                }
                            case 5:
                                {
                                    foreach (var item in workers)
                                    {
                                        if (item.GetHashCode() == logged)
                                        {
                                            tmp = employers.SelectMany(x => x.eAnnouncements.Where(s => s.Experience == item.wAnnouncement.Experience));
                                        }
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        if (tmp != null)
                        {
                            foreach (var item in tmp)
                            {
                                item.Show();
                                Console.WriteLine("-------------------------------");
                            }
                        }
                        else Console.WriteLine("Not Found");
                        goto ReturnMain;
                    }
                case 3:
                    {
                        foreach (var item in workers)
                        {
                            if (item.GetHashCode() == logged && item.wAnnouncement != null)
                            {
                                item.Show(); goto ReturnMain;
                            }
                        }
                        Console.WriteLine("Not Found Any CV");
                        goto ReturnMain;
                    }
                case 4:
                    {
                    returnJobs:
                        var tmp = employers.SelectMany(x => x.eAnnouncements).ToList();
                        if (tmp.Count != 0)
                        {
                            int h = 1;
                            foreach (var item in tmp)
                            {
                                Console.WriteLine($"{h++} - {item.AnnouncementName}");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Job Not Found");
                            goto ReturnMain;
                        }
                        int select = Convert.ToInt32(Console.ReadLine());
                        select--;
                        if (select >= tmp.Count)
                        {
                            Console.WriteLine("Out of Range");
                            goto returnJobs;
                        }
                        tmp[select].Show();
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("Send CV For This Job y/n");
                        var sendJob = Console.ReadLine();
                        if (sendJob.ToLower() == "y")
                        {
                            var cvID = workers.Find(x => x.GetHashCode() == logged).wAnnouncement.CV_ID;
                            uint eCvID = 0;
                            foreach (var item in employers)
                            {
                                eCvID = item.eAnnouncements.Find(x => x.Announcment_ID == tmp[select].Announcment_ID).Announcment_ID;
                            }
                            foreach (var item in workers)
                            {
                                if (item.GetHashCode() == logged)
                                {
                                    foreach (var item2 in employers)
                                    {
                                        foreach (var item3 in item2.eAnnouncements)
                                        {
                                            if (item3.Announcment_ID == tmp[select].Announcment_ID)
                                            {
                                                if (item2.Coming.Keys.Contains(item3.Announcment_ID))
                                                {
                                                    if (!item2.Coming[item3.Announcment_ID].Contains(item.wAnnouncement.CV_ID))
                                                        item2.Coming[item3.Announcment_ID].Add(item.wAnnouncement.CV_ID);
                                                }
                                                else
                                                {
                                                    item2.Coming.Add(item3.Announcment_ID, new List<uint>() { item.wAnnouncement.CV_ID });
                                                }
                                                //item2.Coming.Add(new Dictionary<uint, uint>() { { item3.Announcment_ID, item.wAnnouncement.CV_ID } });
                                            }
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("Sended!");
                            goto ReturnMain;
                        }
                        else goto returnJobs;
                    }
                case 5:
                    {
                        return false;
                    }
                default:
                    break;
            }
            return false;
        }
        public static bool ShowLoggedInMenuForEmployer(List<Employer> employers, List<Worker> workers, int logged)
        {
            int i = 0;
        ReturnMain:
            if (i > 0)
                Console.ReadKey();
            i++;
            string[] Choices = { "Add Announcement", "Work Applications", "Log Out" };
            int currentChoice = 0;
            ConsoleKeyInfo info;
            while (true)
            {
                Console.Clear();
                Program.ShowChoices(currentChoice, Choices);
                info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter) break;
                if (info.Key == ConsoleKey.RightArrow)
                {
                    if (currentChoice < 2) currentChoice++;
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
                        employers.Find(x => x.GetHashCode() == logged).eAnnouncements.Add(Employer.addAnnouncement());
                        goto ReturnMain;
                    }
                case 1:
                    {
                        bool checkExist = false;
                        foreach (var key in employers.Find(x => x.GetHashCode() == logged).Coming)
                        {
                            foreach (var item in employers)
                            {
                                foreach (var item2 in item.eAnnouncements)
                                {
                                    if (key.Key == item2.Announcment_ID)
                                    {
                                        checkExist = true;
                                        Console.BackgroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("ANNOUNCEMENT:");
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        Console.BackgroundColor = ConsoleColor.DarkGray;
                                        item2.Show();
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        foreach (var list in key.Value)
                                        {
                                            Console.WriteLine("-------------------------------------");
                                            Console.BackgroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("WORKER:");
                                            Console.BackgroundColor = ConsoleColor.Black;
                                            workers.Find(x => x.wAnnouncement.CV_ID == list).Show();
                                        }
                                    }
                                }
                            }
                        }
                        if (!checkExist)
                        {
                            Console.WriteLine("Empty!");
                        }
                        goto ReturnMain;
                    }
                case 2:
                    {
                        return false;
                    }
                default:
                    break;
            }
            Console.Clear();
            return false;
        }

    }
}
