using Finalni_Test.Interfaces;
using Finalni_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finalni_Test.Repository
{
    public class OrganizacionaJedinicaRepository : IDisposable, IOrganizacionaJedinicaRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public IQueryable<OrganizacionaJedinica> GetAll()
        {
            return db.Jedinice;
        }

        public IEnumerable<JedinicaPlataDTO> GetAllSaProsecnimPlatama(decimal granica)
        {
            List<JedinicaPlataDTO> list = new List<JedinicaPlataDTO>();

            foreach (var j in GetAll())
            {
                decimal prosecnaPlata = 0.00m;
                decimal sum = 0.00m;
                int brojPlata = 0;

                foreach (var z in db.Zaposleni)
                {
                    if (z.JedinicaId == j.Id)
                    {
                        sum += z.Plata;
                        brojPlata++;
                    }
                }

                prosecnaPlata = sum / brojPlata;
                if (prosecnaPlata > granica)
                    list.Add(new JedinicaPlataDTO() { Id = j.Id, Ime = j.Ime, GodinaOsnivanja = j.GodinaOsnivanja, ProsecnaPlata = prosecnaPlata });
               
            }

            return list.OrderBy(j => j.ProsecnaPlata);
        }

        public IEnumerable<JedinicaZaposleniDTO> GetBrojnost()
        {
            List<JedinicaZaposleniDTO> list = new List<JedinicaZaposleniDTO>();

            foreach (var j in GetAll())
            {
                int brojZaposlenih = 0;
                foreach (var z in db.Zaposleni)
                {
                    if (z.JedinicaId == j.Id)
                        brojZaposlenih++;
                }

                list.Add(new JedinicaZaposleniDTO() { Id = j.Id, Ime = j.Ime, GodinaOsnivanja = j.GodinaOsnivanja, BrojZaposlenih = brojZaposlenih });
            }

            return list.OrderByDescending(j => j.BrojZaposlenih);
        }

        public OrganizacionaJedinica GetById(int id)
        {
            return db.Jedinice.FirstOrDefault(j => j.Id == id);
        }

        public IEnumerable<OrganizacionaJedinica> GetTradicija()
        {
            IEnumerable<OrganizacionaJedinica> list = db.Jedinice;

            OrganizacionaJedinica najstarija = list.OrderBy(j => j.GodinaOsnivanja).First();
            OrganizacionaJedinica najmladja = list.OrderBy(j => j.GodinaOsnivanja).Last();

            List<OrganizacionaJedinica> retVal = new List<OrganizacionaJedinica>();
            retVal.Add(najmladja);
            retVal.Add(najstarija);

            return retVal.OrderBy(j => j.GodinaOsnivanja);
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