using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff
{
    public class ProPolicyModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public ProPolicyModel(ProcurementDBContext context)
        {
            _context = context;
        }

        public class inputItem
        {
            public int ItemId { get; set; }
            public bool Selected { get; set; }
        }

        [BindProperty]
        public List<inputItem> ItemList { get; set; }
        [BindProperty]
        public ProcurementGroup procurementGroup { get; set; }

        public List<ProcurementItem> ProcurementItems { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
            ProcurementItems = _context.ProcurementItems.Where(m=>m.ProcurementGroupId == 0).ToList();

            foreach (var item in ProcurementItems)
            {
                ItemList.Add(new inputItem() { ItemId = item.Id, Selected = false });
            }

        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return;
            }

            var selectedItems = ItemList.Where(m => m.Selected).ToList();
            if (selectedItems.Count<1)
            {
                Error = "No Item was Selected";
                return;
            }

            var pg = new ProcurementGroup() {
                ClosingDate = procurementGroup.ClosingDate,
                GroupName = procurementGroup.GroupName,
                NoOfCategory = procurementGroup.NoOfCategory,
                OpeningDate = procurementGroup.OpeningDate
            };

            _context.ProcurementGroups.Add(pg);
            _context.SaveChanges();

            foreach (var item in selectedItems)
            {
                var pItem = _context.ProcurementItems.FirstOrDefault(x => x.Id == item.ItemId);
                if (pItem !=null)
                {
                    pItem.ProcurementGroupId = pg.Id;
                }
            }
            _context.SaveChanges();

            ProcurementItems = _context.ProcurementItems.Where(m => m.ProcurementGroupId == 0).ToList();
            ItemList = new List<inputItem>();

            foreach (var item in ProcurementItems)
            {
                ItemList.Add(new inputItem() { ItemId = item.Id, Selected = false });
            }
            procurementGroup = new ProcurementGroup();

            Message = "Saved Successfully";
        }
    }
}