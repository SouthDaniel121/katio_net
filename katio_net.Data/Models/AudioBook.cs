﻿using System.ComponentModel.DataAnnotations.Schema;

namespace katio.Data.Models
{
    public class AudioBook : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string ISBN10 { get; set; } = string.Empty;
        public string ISBN13 { get; set; } = string.Empty;
        public DateOnly Published { get; set; } = new DateOnly();
        public string Edition { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int LenghtInSeconds { get; set; } = 0;
        public string Path { get; set; } = string.Empty;



        // Relacion
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author? Author { get; set; }
    }
}
