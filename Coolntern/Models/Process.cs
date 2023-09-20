namespace Coolntern.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Process")]
    public partial class Process
    {
        public int id { get; set; }

        public string image { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }

        public string link { get; set; }
    }
}
