namespace MVCLab04.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            news = new HashSet<news>();
        }

        public int id { get; set; }

        [Required(ErrorMessage ="*")]
        [StringLength(50)]
        public string name { get; set; }
        [EmailAddress(ErrorMessage ="Notcorrect")]
        [Required(ErrorMessage ="*")]
        [StringLength(50)]
        [Remote("checkEmail","User",ErrorMessage ="This Email Found")]
        public string email { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        
        public string password { get; set; }
        [NotMapped]
        [DisplayName("ConfirmPassword")]
        [System.Web.Mvc.Compare("password",ErrorMessage="Not Matched")]
        [Required]
        public string confirm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<news> news { get; set; }
    }
}
