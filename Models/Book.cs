﻿using System.ComponentModel.DataAnnotations;

namespace LibrarySystemManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Author is required.")]
        public string Author { get; set; }
        [Required(ErrorMessage ="Category is required.")]
        public int? CategoryId { get; set; }

    }
}
