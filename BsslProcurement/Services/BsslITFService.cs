using BsslProcurement.Interfaces;
using DcProcurement.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{

    public class BsslITFService : IBsslITFService
    {
        private readonly BSSLSYS_ITF_DEMOContext _bsslcontext;
        public BsslITFService(BSSLSYS_ITF_DEMOContext context)
        {
            _bsslcontext = context;
        }

        public async Task<Accust> GetLastSupplierAsync(string itemCode)
        {

            try
            {

                var arr = itemCode.Split('-');
                var lastSupply = await _bsslcontext.Gitab.Where(m => m.Groupno == arr[0] + arr[1] && m.Stockno == arr[2])
                    .OrderByDescending(m => m.Serno).FirstOrDefaultAsync();
                if (lastSupply != null)
                {
                    var lastSupplier = await _bsslcontext.Accusts.Select(n => new Accust
                    {
                        Keyid = n.Keyid,
                        Custcode = n.Custcode,
                        Accname = n.Accname
                    }).FirstOrDefaultAsync(m => m.Custcode == lastSupply.Suppcode);

                    if (lastSupplier != null)
                    {
                        return lastSupplier;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public async Task<List<Busline>> GetSubcategoriesAsync(string categoryCode)
        {
            var subcategories = await _bsslcontext.Busline.Where(m => m.CatCodes == categoryCode).ToListAsync();
            return subcategories;
        }
    }
}
