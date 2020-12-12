namespace MVCLab04.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        public int id { get; set; }

        [DisplayName("Title of News")]
        [Required(ErrorMessage = "*")]
        public string title { get; set; }
        [DisplayName("Pref ")]

        public string pref { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Description")]

        public string desc { get; set; }

        [StringLength(50)]
        public string photo { get; set; }

        public int? user_id { get; set; }
        [DisplayName("Catogery of News")]

        public int? catogery_id { get; set; }
        [DisplayName("Date and Time of News")]

        [Required(ErrorMessage ="*")]
        public DateTime date { get; set; }
        

        public virtual catogery catogery { get; set; }

        public virtual user user { get; set; }
    }
}
