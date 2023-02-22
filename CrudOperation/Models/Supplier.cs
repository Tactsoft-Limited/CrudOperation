using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CrudOperation.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string SupplierName { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Email")]
        public string SupplierEmail { get; set; }

        [Display(Name = "Phone")]
        public string SupplierPhone { get; set; }

        [Display(Name = "Address")]
        public string SupplierAddress { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        public Country Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }


        public IList<Purchase> Purchases { get; set; }

    }
}
