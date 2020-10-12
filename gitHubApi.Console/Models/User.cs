using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text;


namespace GitHubApi.Models
{
    public class User
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
    }
}
