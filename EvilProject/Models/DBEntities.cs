using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EvilProject.Models
{
    public class PageNews
    {
        public int id { get; set; }

        [Display(Name = "Tytuł")]
        [Required]
        public string title { get; set; }

        [Display(Name = "Treść")]
        [Required]
        public string body { get; set; }

        [Display(Name = "Data publikacji")]
        [Required]
        public System.DateTime publish_date { get; set; }
    }

    public class TODO
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa projektu")]
        [Required]
        public string project_name { get; set; }

        [Display(Name = "Co jest do zrobienia?")]
        [Required]
        public string description { get; set; }

        [Display(Name = "Data dodania")]
        [Required]
        public System.DateTime add_date { get; set; }

        [Display(Name = "Data wykonania")]
        public Nullable<System.DateTime> done_date { get; set; }
    }
}