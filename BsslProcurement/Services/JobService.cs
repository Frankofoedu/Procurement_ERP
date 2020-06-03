using BsslProcurement.Interfaces;
using DcProcurement;
using DcProcurement.Jobs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public class JobService : IJobService
    {
        protected readonly ProcurementDBContext _context;

        public JobService(ProcurementDBContext context)
        {
            _context = context;
        }
        public async Task<List<RequisitionJob>> GetApprovalJobsForRequisitionsAsync(List<Requisition> requisitions)
        {
            var approvingRequisitionJobs = new List<RequisitionJob>();

            foreach (var item in requisitions)
            {

                var approvingprj = await _context.RequisitionJobs.Include(n => n.Staff)
                    .Where(m => m.RequisitionId == item.Id && m.Workflow.WorkflowAction.Name == Constants.ApproverActionName)
                    .OrderByDescending(l => l.Id).FirstOrDefaultAsync();

                if (approvingprj != null)
                {
                    approvingRequisitionJobs.Add(approvingprj);
                }
                else
                {
                    var prj = await _context.RequisitionJobs.Include(n => n.Staff).Where(m => m.RequisitionId == item.Id)
                        .OrderByDescending(l => l.Id).FirstOrDefaultAsync();

                    if (prj != null) { approvingRequisitionJobs.Add(prj); }
                    else { approvingRequisitionJobs.Add(RequisitionJob.Empty()); }
                }
            }

            return approvingRequisitionJobs;
        }

        public async Task<List<RequisitionJob>> GetLastJobsForRequisitionsAsync(List<Requisition> requisitions)
        {
            var lastRequisitionJobs = new List<RequisitionJob>();

            foreach (var item in requisitions)
            {
                var prj = await _context.RequisitionJobs.Include(n => n.Staff).Where(m => m.RequisitionId == item.Id)
                    .OrderByDescending(l => l.Id).FirstOrDefaultAsync();

                if (prj != null) { lastRequisitionJobs.Add(prj); }
                else { lastRequisitionJobs.Add(RequisitionJob.Empty()); }
            }

            return lastRequisitionJobs;
        }
    }
}
