using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;


namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class UploadViewModel
    {
        [Required]
        public IFormFile Teds { get; set; }
        
    }
}
