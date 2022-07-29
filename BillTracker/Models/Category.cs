using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BillTracker.Models
{
    public class Category
    {
        [Key]
        public int categoryId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage="Title is Required.")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string Icon { get; set; } = "";
        [Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; } = "Expense";
        [NotMapped]
        public string? TitleWithIcon
        {
            get
            {
                return this.Icon + " " + this.Title;
            }
        }
    }
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Please Select a Category.")]
        public int CategoryId { get; set; }
        public Category? category { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public int Amount { get; set; }
        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [NotMapped]
        public string? CategoryTitleWithIcon 
        {
            get
            {
                return category == null ? "" : category.Icon + " " + category.Title; 
            }
        }
        public string? FormattedAmount
        {
            get
            {
                return ((category == null || category.Type == "Expense")? "-": "+" ) + Amount.ToString("C0");
            }
        }
    }

    public class BillContext : DbContext
    {
        public BillContext(DbContextOptions<BillContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
