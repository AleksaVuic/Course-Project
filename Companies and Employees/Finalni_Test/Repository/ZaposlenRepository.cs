using Finalni_Test.Interfaces;
using Finalni_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Finalni_Test.Repository
{
    public class ZaposlenRepository : IDisposable, IZaposlenRepository
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        public void Add(Zaposlen z)
        {
            db.Zaposleni.Add(z);
            db.SaveChanges();
        }

        public void Delete(Zaposlen z)
        {
            db.Zaposleni.Remove(z);
            db.SaveChanges();
        }

        public IQueryable<Zaposlen> GetAll()
        {
            return db.Zaposleni
                .Include(z => z.Jedinica)
                .OrderBy(z => z.GodinaZaposlenja);
        }

        public IQueryable<Zaposlen> GetAllSaGodinomRodjenjaNakon(int godina)
        {
            var list = db.Zaposleni
                .Include(z => z.Jedinica)
                .Where(z => z.GodinaRodjenja > godina)
                .OrderBy(z => z.GodinaRodjenja);

            return list;
        }

        public IQueryable<Zaposlen> GetAllSaPlatomIzmedju(decimal najmanje, decimal najvise)
        {
            var list = db.Zaposleni
                 .Include(z => z.Jedinica)
                 .Where(z => z.Plata >= najmanje && z.Plata <= najvise)
                 .OrderByDescending(z => z.Plata);

            return list;
        }

        public Zaposlen GetById(int id)
        {
            return db.Zaposleni
                .Include(z => z.Jedinica)
                .FirstOrDefault(z => z.Id == id);
        }

        public void Update(Zaposlen z)
        {
           
            db.Entry(z).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}