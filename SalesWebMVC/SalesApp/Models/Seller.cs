using System.ComponentModel.DataAnnotations;

namespace SalesApp.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60 , MinimumLength = 3 , ErrorMessage = "{0} Error size should be betewwn {2} and {1}")]//{0} ele referencia o atributo que será mencionado na mensagem
        //{1} e {2}, é referente ao parametro que será apresentando
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]//Definindo a semantica dos tipos de dados no display
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Display(Name = "Birth Date")] //Criando uma label customizada no display
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} required")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")] //Criando uma label customizada no display
        [DisplayFormat(DataFormatString ="{0:F2}")] // Configurando formato dos dados no display
        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }

        public Department ? Department { get; set; }

        public int DepartmentId { get; set; } //Criamos esse atributo para que o Framework consiga trabalhar com a possibilidade de um Id nullo 

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department?department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
