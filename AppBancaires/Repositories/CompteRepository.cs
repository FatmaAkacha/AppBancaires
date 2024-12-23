using AppBancaires.Data;
using AppBancaires.Model;
using AppBancaires.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppBancaires.Repositories
{
    public class CompteRepository : ICompteRepository
    {
        private readonly AppBancairesContext _context;

        public CompteRepository(AppBancairesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Compte>> GetAll()
        {
            return await _context.Compte
                .Include(c => c.Client)
                .ToListAsync();
        }

        public async Task<Compte> GetById(int CompteId)
        {
            return await _context.Compte
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == CompteId);
        }

        public async Task<Compte> Add(Compte c)
        {
            await _context.Compte.AddAsync(c);
            await _context.SaveChangesAsync();
            return c;
        }

        public async Task<Compte> Update(Compte c)
        {
            _context.Compte.Update(c);
            await _context.SaveChangesAsync();
            return c;
        }

        public async Task<Compte> Delete(int CompteId)
        {
            var compte = await _context.Compte.FindAsync(CompteId);
            if (compte != null)
            {
                _context.Compte.Remove(compte);
                await _context.SaveChangesAsync();
            }
            return compte;
        }

        public async Task<IEnumerable<Compte>> Search(int ClientId)
        {
            return await _context.Compte
                .Where(c => c.ClientID == ClientId)
                .Include(c => c.Client)
                .ToListAsync();
        }

        public async Task<Operation> Add(int CompteId, Operation o)
        {
            o.CompteID = CompteId;
            await _context.Operation.AddAsync(o);
            await _context.SaveChangesAsync();
            return o;
        }

        public async Task<Compte> Maj_solde(Compte c, Operation o)
        {
            if (o.TypeOperation == "retrait")
            {
                c.Solde -= o.Montant;
            }
            else if (o.TypeOperation == "versement")
            {
                c.Solde += o.Montant;
            }
            _context.Compte.Update(c);
            await _context.SaveChangesAsync();
            return c;
        }

        public async Task<IEnumerable<Operation>> Extrait_Compte(int numcompte, DateTime d)
        {
            return await _context.Operation
                .Where(o => o.CompteID == numcompte && o.DateOperation >= d)
                .OrderBy(o => o.DateOperation)
                .ToListAsync();
        }

        public async Task<double> Solde_Total(int ClientId)
        {
            return (double)await _context.Compte
                .Where(c => c.ClientID == ClientId)
                .SumAsync(c => c.Solde);
        }

        public async Task MAJVirement(string Compte1Id, string Compte2Id, double m)
        {
            var compte1 = await _context.Compte.FindAsync(int.Parse(Compte1Id));
            var compte2 = await _context.Compte.FindAsync(int.Parse(Compte2Id));

            if (compte1 != null && compte2 != null && compte1.Solde >= (decimal)m)
            {
                compte1.Solde -= (decimal)m;
                compte2.Solde += (decimal)m;

                _context.Compte.Update(compte1);
                _context.Compte.Update(compte2);
                await _context.SaveChangesAsync();
            }
        }


    }
}