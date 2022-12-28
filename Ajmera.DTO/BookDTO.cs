using System;
using System.ComponentModel.DataAnnotations;

namespace Ajmera.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string AuthorName { get; set; }
    }
}
