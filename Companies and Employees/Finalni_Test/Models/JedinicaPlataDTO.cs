﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finalni_Test.Models
{
    public class JedinicaPlataDTO
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Ime { get; set; }

        [Range(2010, 2020)]
        public int GodinaOsnivanja { get; set; }

        public decimal ProsecnaPlata { get; set; }

        public JedinicaPlataDTO()
        {

        }

        public JedinicaPlataDTO(int id, string ime, int godinaOsnivanja, decimal prosecnaPlata)
        {
            Id = id;
            Ime = ime;
            GodinaOsnivanja = godinaOsnivanja;
            ProsecnaPlata = prosecnaPlata;
        }
    }
}