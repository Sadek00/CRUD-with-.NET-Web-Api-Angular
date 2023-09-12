using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Gtr.Models.DTOs
{
    public class AuthResult
    {
        public string Token { get; set; } = string.Empty;
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
    }
}
