using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iMaintenanceTotal.Models
{
    public class MaintTask
    {
        [Key]
        public int MaintTaskId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CompleteBy { get; set; }
        public bool Completed { get; set; }
        public string Frequency { get; set; }
        public string Notes { get; set; }
        public DateTime RemindMeOn { get; set; }
        public string RemindMeBy { get; set; }
        public string Category { get; set; }
        public ApplicationUser Owner { get; set; }

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
Owner: ApplicationUser
*/
