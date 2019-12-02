using DcProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IRequisitionService
    {
        Task<List<Requisition>> GetRequisitionsForLoggedInUser(string userId);

    }
}
