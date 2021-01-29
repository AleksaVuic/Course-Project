using Finalni_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finalni_Test.Interfaces
{
    public interface IZaposlenRepository
    {

        IQueryable<Zaposlen> GetAll();
        Zaposlen GetById(int id);
        IQueryable<Zaposlen> GetAllSaGodinomRodjenjaNakon(int godina);
        void Add(Zaposlen z);
        void Update(Zaposlen z);
        void Delete(Zaposlen z);
        IQueryable<Zaposlen> GetAllSaPlatomIzmedju(decimal najmanje, decimal najvise);

    }
}
