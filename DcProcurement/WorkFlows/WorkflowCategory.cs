using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    /// <summary>
    /// Category or type of workflow. e.g. prequalification etc
    /// </summary>
    public class WorkflowCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Workflow> Workflows { get; set; }
    }
}
