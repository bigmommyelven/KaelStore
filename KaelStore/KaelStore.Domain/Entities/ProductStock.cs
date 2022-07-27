namespace KaelStore.Domain.Entities
{
    public class ProductStock
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public Product Product { get; set; }
    }
}
