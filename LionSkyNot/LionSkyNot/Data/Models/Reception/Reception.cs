﻿using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Data.Models.Reception
{
    public class Reception
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public float Calories { get; set; }

        [Required]
        public float Protein { get; set; }

        [Required]
        public float MyProperty { get; set; }

        [Required]
        public float Carbohydrates { get; set; }



    }
}