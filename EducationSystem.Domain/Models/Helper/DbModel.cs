using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Domain.Models.Helper
{
    public class DbModel
    {
        public DateTime DateCreate{ get; set; }
        public DateTime? DateUpdate { get; set; }
        public DateTime? DateHidden { get; set; }
        public bool IsHidden { get; set; } = false;
    }
}
