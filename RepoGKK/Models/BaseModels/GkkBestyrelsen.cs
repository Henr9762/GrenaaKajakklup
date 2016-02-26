using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RepoGKK.Models.BaseModels
{
    public class GkkBestyrelsen
    {
        public int ID { get; set; }

        [Required]
        public string Billede { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Navn { get; set; }
    }
}
