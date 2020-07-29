using DcProcurement.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IBsslITFService
    {
        Task<List<Busline>> GetSubcategoriesAsync(string categoryCode);
        Task<Accust> GetLastSupplierAsync(string itemCode);
    }
}
