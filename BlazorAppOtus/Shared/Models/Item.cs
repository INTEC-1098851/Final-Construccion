namespace BlazorAppOtus.Shared.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SizeId { get; set; }
        public double UnitPrice { get; set; }
        public double UnitCost{ get; set; }
        public int CategoryId { get; set; }

    }
}
