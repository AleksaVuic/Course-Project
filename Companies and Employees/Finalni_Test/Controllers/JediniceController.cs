using Finalni_Test.Interfaces;
using Finalni_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Finalni_Test.Controllers
{
    public class JediniceController : ApiController
    {
        IOrganizacionaJedinicaRepository _repository { get; set; }
        public JediniceController(IOrganizacionaJedinicaRepository repository)
        {
            _repository = repository;
        }

        
        public IQueryable<OrganizacionaJedinica> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var j = _repository.GetById(id);
            if (j == null)
                return NotFound();

            return Ok(j);
        }

        [Route("api/tradicija")]
        public IEnumerable<OrganizacionaJedinica> GetTradicija()
        {
            return _repository.GetTradicija();
        }

        [Route("api/brojnost")]
        public IEnumerable<JedinicaZaposleniDTO> GetBrojnost()
        {
            return _repository.GetBrojnost();
        }

        [Route("api/plate")]
        public IEnumerable<JedinicaPlataDTO> PostPlate(ProsecnaGranica g)
        {
            return _repository.GetAllSaProsecnimPlatama(g.Granica);
        }


    }
}
