﻿using System.ComponentModel.DataAnnotations.Schema;

namespace katio.Data.Models
{
    public class Book : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string ISBN10 { get; set; } = string.Empty;
        public string ISBN13 { get; set; } = string.Empty;
        public DateOnly Published { get; set; } = new DateOnly();
        public string Edition { get; set; } = string.Empty;
        public string DeweyIndex { get; set; } = string.Empty;

        public string AuthorN { get; set; } = string.Empty;



        // Relacion
        [ForeignKey("Author")]
    public int AuthorId {get; set;}

    public virtual Author? Author{ get; set;}

    public bool Any()
    {
        return Id > 0;
    }
    }
}
