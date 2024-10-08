﻿using System.ComponentModel.DataAnnotations;

namespace LibrarySystemManagement.Models.Categories
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
