using GenericRepository.Entities;

namespace BlazorAppOtus.Shared.Models
{
    public class InvoiceDetail:IEntity
    {
        public InvoiceDetail()
        {

        }
        public InvoiceDetail(string description,int quantity,double unitPrice)
        {
            this.Description=description;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
        }
        public int? Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double SubTotal { get => (Quantity * UnitPrice); }
        public double Total { get => (Quantity * UnitPrice)-Discount; }
        public string CreatedBy { get; set; } = "";
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? StatusId { get; set; }

    }
}
