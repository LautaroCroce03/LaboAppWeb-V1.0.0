using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LaboAppWebV1._0._0.ModelsDto
{
    public class LoginDto
    {
        [DefaultValue("")]
        [Required]
        public string Usuario { get; set; }

        [DefaultValue("")]
        [Required]
        public string Password { get; set; }
    }
}
