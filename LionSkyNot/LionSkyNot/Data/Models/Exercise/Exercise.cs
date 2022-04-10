using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

using static LionSkyNot.Data.DataConstants.Exercise;


namespace LionSkyNot.Data.Models.Exercise
{
    public class Exercise
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        [Required]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(TypeExercise))]
        public int TypeExerciseId { get; set; }

        public TypeExercise TypeExercise { get; set; }

        public bool IsDeleted { get; set; } = false;


    }
}
