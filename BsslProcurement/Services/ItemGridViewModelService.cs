using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public class ItemGridViewModelService : IItemGridViewModelService
    {
        protected readonly ProcurementDBContext _procurementDBContext;

        public ItemGridViewModelService(ProcurementDBContext procurementDBContext)
        {
            _procurementDBContext = procurementDBContext;
        }
        public Task<List<ItemGridViewModel>> GetItemsInRequisition(int requisitionId)
        {
            return _procurementDBContext.RequisitionItems.Where(reItems => reItems.RequisitionId == requisitionId).Include(items => items.Attachment)
                .Select(x=> new ItemGridViewModel { RequisitionItem = x })
                .ToListAsync();
        }
    }
}
