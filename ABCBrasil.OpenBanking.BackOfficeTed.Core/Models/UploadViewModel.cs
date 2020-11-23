using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class UploadViewModel
    {
        [Required]
        public IFormFile Teds { get; set; }
    }
}
