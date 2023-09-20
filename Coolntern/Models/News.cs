namespace Coolntern.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public int id { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        public string desciption { get; set; }

        [Column(TypeName = "ntext")]
        public string content { get; set; }

        public string image { get; set; }

        public string link { get; set; }

        public string meta { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }

        public DateTime? datebegin { get; set; }

        public int? categoryId { get; set; }

        public virtual NCategory NCategory { get; set; }
    }
}
