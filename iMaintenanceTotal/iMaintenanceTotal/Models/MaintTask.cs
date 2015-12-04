using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMaintenanceTotal.Models
{
    public class MaintTask
    {
        public int MaintTaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CompleteBy { get; set; }
        public bool Completed { get; set; }
        public string Repeat { get; set; }
        public DateTime RemindMeOn { get; set; }
    }
}

/*
*****MaintTask*****
TaskID: int
Title: string
Description: string
CreatedAt: date
DueDate: date
Completed: bool
Frequency: date
Reminder: date
Category: string
*/