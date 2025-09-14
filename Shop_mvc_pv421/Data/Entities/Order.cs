using Microsoft.AspNetCore.Identity;

namespace Shop_mvc_pv421.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
