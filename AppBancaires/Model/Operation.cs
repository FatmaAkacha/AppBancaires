namespace AppBancaires.Model
{
    public class Operation
    {
        public int Id { get; set; }
        public string? TypeOperation { get; set; }
        public decimal Montant { get; set; }
        public DateTime? DateOperation { get; set; }
        public int? CompteID { get; set; }

        public Compte? Compte { get; set; }
    }

}
