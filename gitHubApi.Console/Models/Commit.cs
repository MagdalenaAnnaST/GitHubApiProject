using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GitHubApi.Models
{
    public class Commit
    {
        [Key]
        public string Sha { get; set; }
        public string Message { get; set; }
        public User Commiter { get; set; }
        public Repository Repository { get; set; }
        public User User { get; set;}

    }
}
