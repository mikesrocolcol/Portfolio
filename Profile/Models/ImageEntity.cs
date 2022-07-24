
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Profile.Models;

public class ImageEntity
{
    [Key]
    public int Id { get; set; }

 
   
   
    public string? ImageFile { get; set; }
    [Required]
    [NotMapped]
    [Display(Name = "File")]
    public IFormFile? FormFile { get; set; }

  
}



