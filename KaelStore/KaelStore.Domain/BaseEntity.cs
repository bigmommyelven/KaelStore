using System.ComponentModel.DataAnnotations;

namespace KaelStore.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
