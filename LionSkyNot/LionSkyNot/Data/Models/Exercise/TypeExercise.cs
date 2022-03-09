using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Data.Models.Exercise
{
    public class TypeExercise
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string TypeName { get; set; }

    }
}
