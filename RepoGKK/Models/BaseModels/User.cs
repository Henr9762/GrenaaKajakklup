using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoGKK.Models.BaseModels
{
   public class User
    {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public  string UserPassword { get; set; }
    }
}
