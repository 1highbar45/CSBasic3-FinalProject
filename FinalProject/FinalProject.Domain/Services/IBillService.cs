using FinalProject.Domain.Models.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Services
{
    public interface IBillService
    {
        Task CreateBill(BillCreateViewModel model);
    }
}
