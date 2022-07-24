using System.ComponentModel;

using System.ComponentModel.DataAnnotations;

namespace Profile.Models
{
    public class ExpenseCategoryEntity
    {

        [Key]
        public int ExpenseCategoryId { get; set; }
        [Required]
        public string? ExpenseCategoryName { get; set; }
        public int Id { get; internal set; }
    }

}
