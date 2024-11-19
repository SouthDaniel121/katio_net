﻿using System.ComponentModel.DataAnnotations.Schema;

namespace katio.Data.Models
{
    public class Author : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }

        public string Description { get; set; } = string.Empty;

       

    }
}
