using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finalni_Test.Models
{
    public class JedinicaZaposleniDTO
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Ime { get; set; }

        [Range(2010, 2020)]
        public int GodinaOsnivanja { get; set; }

        public int BrojZaposlenih { get; set; }

        public JedinicaZaposleniDTO()
        {

        }

        public JedinicaZaposleniDTO(int id, string ime, int godinaOsnivanja, int brojZaposlenih)
        {
            Id = id;
            Ime = ime;
            GodinaOsnivanja = godinaOsnivanja;
            BrojZaposlenih = brojZaposlenih;
        }
    }
}