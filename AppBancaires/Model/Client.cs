﻿namespace AppBancaires.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
        public string? Image { get; set; }
        public ICollection<Compte>? Comptes { get; set; }
    }
}
