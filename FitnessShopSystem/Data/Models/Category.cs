namespace FitnessShopSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;

    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
            this.Programs = new HashSet<TrainingProgram>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<TrainingProgram> Programs { get; set; }
    }
}
