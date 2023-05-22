using System.ComponentModel.DataAnnotations;

namespace Library_Loan.DAL.Entities
{
    public class Entity
    {
        #region Properties
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Fecha de prestamo")]
        public DateTime? LoanDate { get; set; }
        [Display(Name = "Fecha de entrega")]
        public DateTime? Deadline { get; set; }
        #endregion
    }
}
