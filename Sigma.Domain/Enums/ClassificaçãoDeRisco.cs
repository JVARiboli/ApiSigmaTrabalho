using System.ComponentModel.DataAnnotations;

namespace Sigma.Domain.Enums
{
    public enum ClassificaçãoDeRisco
    {
        [Display(Name = "Baixo")]
        Baixo,

        [Display(Name = "Medio")]
        Medio,

        [Display(Name = "Alto")]
        Alto
    }
}
