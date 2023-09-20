namespace Coolntern.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Job")]
    public partial class Job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Job()
        {
            DetailJobs = new HashSet<DetailJob>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string nameCompany { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        public int? salary { get; set; }

        [StringLength(100)]
        public string location { get; set; }

        public int? vacancy { get; set; }

        public string image { get; set; }

        public string description { get; set; }

        public string requirement { get; set; }

        public string link { get; set; }

        public string meta { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }

        public DateTime? dateapply { get; set; }

        public DateTime? datebegin { get; set; }

        public int? categoryId { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailJob> DetailJobs { get; set; }
    }
}
