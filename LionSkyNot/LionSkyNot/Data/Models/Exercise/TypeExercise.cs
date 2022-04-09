using System.ComponentModel.DataAnnotations;

using static LionSkyNot.Data.DataConstants.TypeExercise;


namespace LionSkyNot.Data.Models.Exercise
{
    public class TypeExercise
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string TypeName { get; set; }

    }
}
