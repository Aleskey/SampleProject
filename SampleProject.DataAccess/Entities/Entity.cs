using System.ComponentModel.DataAnnotations;

namespace SampleProject.DataAccess.Entities
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}