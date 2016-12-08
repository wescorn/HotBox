using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HOTBOXWebsite.Models
{
    public class ProjectOverviewViewModel
    {
        public List<DropdownModel> projects { get; set; }
        public DropdownModel SelectedProject { get; set; }
    }
}