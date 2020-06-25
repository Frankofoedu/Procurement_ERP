using BsslProcurement.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IPRNumberService
    {
        Task<string> GetNewPRNumberAsync(string DashedPRno);
        Task<PRNumberServiceDataViewModel> GetDashedPRNumberAndDepartmentDataAsync(string userCode);
    }
}
