namespace Coolntern.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailJob")]
    public partial class DetailJob
    {
        [Key]
        [Column(Order = 0)]
        public string id_user { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_job { get; set; }

        public DateTime? dateApply { get; set; }

        public short? approval { get; set; }

        [Column(TypeName = "ntext")]
        public string message { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Job Job { get; set; }
    }
}
