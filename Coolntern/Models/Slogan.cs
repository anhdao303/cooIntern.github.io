namespace Coolntern.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slogan")]
    public partial class Slogan
    {
        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string job { get; set; }

        public string message { get; set; }

        public string image { get; set; }

        public string link { get; set; }

        public string meta { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }

        public DateTime? datebegin { get; set; }
    }
}
