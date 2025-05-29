using System.ComponentModel.DataAnnotations;

namespace Sigma.Domain.Enums
{
    public enum ClassificacaoDeRisco
    {
        [Display(Name = "Baixo")]
        Baixo,

        [Display(Name = "Medio")]
        Medio,

        [Display(Name = "Alto")]
        Alto
    }
}
