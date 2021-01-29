using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finalni_Test.Models
{
    public class ProsecnaGranica
    {
        [Required]
        public int Granica { get; set; }

    }
}