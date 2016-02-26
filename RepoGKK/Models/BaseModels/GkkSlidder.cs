using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoGKK.Models.BaseModels
{
   public class GkkSlidder
    {
        public int ID { get; set; }

        [Required]
        public string Billedenavn { get; set; }
    }
}
