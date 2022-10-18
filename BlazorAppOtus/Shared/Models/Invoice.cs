using GenericRepository.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppOtus.Shared.Models
{
    public class Invoice:IEntity
    {
        public int? Id { get; set; }
        public string? InvoiceNumber{ get; set; }
        [Required(ErrorMessage="El nombre del cliente es requerido")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "El correo del cliente es requerido")]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        public string CustomerEmail{ get; set; } 
        public string? CustomerPhone { get; set; } 
        public string CreatedBy { get; set; } = "";
        public DateTime CreatedDate{ get; set; }= DateTime.Now;
        public string? UpdatedBy{ get; set; }
        public DateTime? UpdatedDate{ get; set; }
        public int? StatusId{ get; set; }
        public List<InvoiceDetail> Details { get; set; } = new List<InvoiceDetail>() {};
        public bool HasAnyDetail { get => Details != null && Details.Any(); }
        public double TotalAmount{ get => HasAnyDetail ? Details.Sum(x => x.SubTotal) : 0; }
        public double DiscountAmount { get => HasAnyDetail ? Details.Sum(x => x.Discount) : 0; }
        public double NetAmount{ get => TotalAmount - DiscountAmount; }
        public string? FileUrl{ get; set; }
    }
}
