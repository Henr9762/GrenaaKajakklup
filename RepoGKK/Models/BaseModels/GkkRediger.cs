using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoGKK.Models.BaseModels
{
   public class GkkRediger
    {

        public int ID { get; set; }

   
        public string Overskrift { get; set; }

        [Required]
        public string Tekst { get; set; }
    }
}
