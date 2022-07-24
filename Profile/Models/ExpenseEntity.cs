using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Profile.Models;

public class ExpenseEntity
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please Enter Expense Amount")]
    public int Amount { get; set; }
    [DataType(DataType.Date)]
    [Required]
    public DateTime Createdate { get; set; }
    [Required]
    public string? Description { get; set; }

    [Display(Name = "Expense Category")]
    public int ExpenseCategoryId { get; set; }

    [ForeignKey("ExpenseCategoryId")]
    public virtual ExpenseCategoryEntity? ExpenseCategory { get; set; }
    [Display(Name = "Reciept")]
    public string? Image { get; set; }

    [NotMapped]
    [Required(ErrorMessage = "Select Image")]
    [Display(Name = "Receipt ")]
    public IFormFile? FormFile { get; set; }



}

