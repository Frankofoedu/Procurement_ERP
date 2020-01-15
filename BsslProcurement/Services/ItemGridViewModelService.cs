using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public class ItemGridViewModelService : IItemGridViewModelService
    {
        protected readonly ProcurementDBContext _procurementDBContext;
        private readonly IWebHostEnvironment _Env;

        public ItemGridViewModelService(ProcurementDBContext procurementDBContext, IWebHostEnvironment Env)
        {
            _procurementDBContext = procurementDBContext;
            _Env = Env;
        }
        public Task<List<ItemGridViewModel>> GetItemsInRequisition(int requisitionId)
        {
          var reqItems =  _procurementDBContext.RequisitionItems.Include(items => items.Attachment).Where(reItems => reItems.RequisitionId == requisitionId);
              var items = reqItems.Select(x => new ItemGridViewModel { RequisitionItem = x, Attachment = x.Attachment == null ? null: GetFormFile(_Env.WebRootPath,x.Attachment.FilePath)})
                .ToListAsync();

            return items;
        }

        private static FormFile GetFormFile(string rootPath,string filePath)
        {
            using var stream = File.OpenRead(Path.Combine(rootPath, filePath));
            return new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };
        }
    }
}
