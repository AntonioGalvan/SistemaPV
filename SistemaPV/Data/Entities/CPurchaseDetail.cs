namespace SistemaPV.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class CPurchaseDetail
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }

        public CProduct Product { get; set; }

        public CSale Sales { get; set; }
    }
}
