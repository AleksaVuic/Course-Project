using Finalni_Test.Interfaces;
using Finalni_Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Finalni_Test.Controllers
{
    public class ZaposleniController : ApiController
    {
        IZaposlenRepository _repository { get; set; }
        public ZaposleniController(IZaposlenRepository repository)
        {
            _repository = repository;
        }


        public IQueryable<Zaposlen> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var z = _repository.GetById(id);
            if (z == null)
                return NotFound();

            return Ok(z);
        }

        public IQueryable<Zaposlen> GetAllSaGodinomRodjenjaNakon(int rodjenje)
        {
            return _repository.GetAllSaGodinomRodjenjaNakon(rodjenje);
        }

        //[Authorize]
        public IHttpActionResult Post(Zaposlen z)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(z);

            return CreatedAtRoute("DefaultApi", new { id = z.Id }, z);
        }

        //[Authorize]
        public IHttpActionResult Delete(int id)
        {
            var z = _repository.GetById(id);
            if (z == null)
                return NotFound();

            _repository.Delete(z);

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Put(int id, Zaposlen z)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != z.Id)
                return BadRequest();

            try
            {
                _repository.Update(z);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok(z);
        }

        //[Authorize]
        [Route("api/pretraga")]
        public IQueryable<Zaposlen> PostPretraga(GranicaPlate gp)
        {
            return _repository.GetAllSaPlatomIzmedju(gp.Najmanje, gp.Najvise);
        }

    }
}
