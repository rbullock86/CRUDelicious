using System;
using System.ComponentModel.DataAnnotations;

namespace Crudelicious.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set;}

        [Required]
        [MinLength(2)]
        [MaxLength(45)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(45)]
        public string Chef { get; set; }

        [Required]
        [Range(1, 5)]
        public int Tastiness { get; set; }

        [Required]
        [Range(0, 1000000000)]
        public int Calories { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}