namespace AppBancaires.Model
{
    public class Compte
    {
        public int Id { get; set; }
        public string? NumeroCompte { get; set; }
        public DateTime DateCreation { get; set; }
        public decimal Solde { get; set; }
        public int? ClientID { get; set; }
        public string? TypeCompte { get; set; }

        public Client? Client { get; set; }
        public ICollection<Operation>? Operations { get; set; }
    }

}
