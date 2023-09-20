namespace Coolntern.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Footer")]
    public partial class Footer
    {
        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [Column(TypeName = "ntext")]
        public string desciption { get; set; }

        public string link { get; set; }

        public string meta { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }

        public DateTime? datebegin { get; set; }
    }
}
