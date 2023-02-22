using System.ComponentModel.DataAnnotations;

namespace CrudOperation.Models
{
    public class Item
    {
        public Item()
        {
            this.PurchaseItems = new HashSet<PurchaseItem>();
        }

        public int Id { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        public string Discription { get; set; }

        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }

    }
}
