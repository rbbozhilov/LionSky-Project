using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LionSkyNot.Data.Models.Exercise
{
    public class Exercise
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string VideoUrl { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(TypeExercise))]
        public int TypeExerciseId { get; set; }

        public TypeExercise TypeExercise { get; set; }


        public bool IsDeleted { get; set; } = false;



    }
}
