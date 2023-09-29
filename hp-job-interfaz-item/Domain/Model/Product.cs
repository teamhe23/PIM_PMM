namespace Domain.Model
{
    public class Product
    {
        public string Id { get; set; }
        public string TipoProducto { get; set; }

        public string CodLinea { get; set; }

        public List<ProductAttribute> Attributes { get; set; }
    }
}