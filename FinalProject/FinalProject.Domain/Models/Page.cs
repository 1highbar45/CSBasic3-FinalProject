using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Models
{
    public class Page
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int SkipNumber
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }
    }
}
