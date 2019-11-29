using BsslProcurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IItemGridViewModelService
    {
        Task<List<ItemGridViewModel>> GetItemsInRequisition(int requisitionId);
    }
}
