﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff
{
    public class CCSGModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public CCSGModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContractSubcategory ContractSubcategory { get; set; }
        [BindProperty]
        public int categoryId { get; set; }
        public ContractCategory ContractCategory { get; set; }
        public List<ContractSubcategory> ContractSubcategories { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public IActionResult OnGet(int? id, int? delId)
        {
            if (id==null)
            { return LocalRedirect("~/Staff/CoC"); }

            if (delId != null)
            {
                var subToDel = _context.ContractSubcategories.FirstOrDefault(k => k.Id == delId);
                if (subToDel != null)
                {
                    _context.ContractSubcategories.Remove(subToDel);
                    _context.SaveChanges();
                }
            }


            var category = _context.ContractCategories.Include(y => y.ContractSubcategories).FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return LocalRedirect("~/Staff/CoC");
            }

            ContractSubcategories = category.ContractSubcategories.ToList();

            ContractCategory = category;
            categoryId = id.Value;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return Page();
            }

            var check = _context.ContractSubcategories.FirstOrDefault(x => x.ContractCategoryId == 
                categoryId && x.SubcategoryName == ContractSubcategory.SubcategoryName);

            if (check != null)
            {
                Error = "Subcategory with this name already exist.";
                return Page();
            }

            ContractSubcategory.ContractCategoryId = categoryId;

            _context.ContractSubcategories.Add(ContractSubcategory);
            _context.SaveChanges();

            var category = _context.ContractCategories.Include(y => y.ContractSubcategories).FirstOrDefault(x => x.Id == categoryId);

            if (category == null)
            {
                return LocalRedirect("~/Staff/CoC");
            }

            ContractSubcategories = category.ContractSubcategories.ToList();

            ContractCategory = category;
            categoryId = category.Id;
            ContractSubcategory = new ContractSubcategory();

            Message = "Saved Successfully";
            return Page();
        }
    }
}