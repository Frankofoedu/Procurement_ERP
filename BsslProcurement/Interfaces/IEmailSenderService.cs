using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendInvitationEmailToListAsync(List<string> emails, string subject, string companyName, string eRFxNo, string projectTitle, DateTime erFxEndDate, string assignedStaffName, string assignedStaffDesignation);
    }
}
