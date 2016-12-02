using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HOTBOXWebsite.Models
{
    public class DropdownModel
    {
        public int Id { get; set; }
        public StoredProject project { get; set; }
    }
}