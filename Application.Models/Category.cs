namespace Application.Models
{
    public class Category : BaseEntity
    {
        public string name { get; set; }
        public ICollection<Product>? products { get; set; }
        public Category()
        {
            products = new List<Product>();
        }
    }
}
