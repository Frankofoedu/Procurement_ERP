using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DcProcurement
{
    /// <summary>
    /// The kind of action that should be taken on each step of workflow. e.g. Approval, recommend etc
    /// </summary>
    public class WorkflowAction
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }

        public ICollection<Workflow> Workflows { get; set; }
    }
}