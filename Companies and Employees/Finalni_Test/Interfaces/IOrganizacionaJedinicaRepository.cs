using Finalni_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finalni_Test.Interfaces
{
    public interface IOrganizacionaJedinicaRepository
    {

        IQueryable<OrganizacionaJedinica> GetAll();
        OrganizacionaJedinica GetById(int id);
        IEnumerable<OrganizacionaJedinica> GetTradicija();
        IEnumerable<JedinicaZaposleniDTO> GetBrojnost();
        IEnumerable<JedinicaPlataDTO> GetAllSaProsecnimPlatama(decimal granica);
    }
}
