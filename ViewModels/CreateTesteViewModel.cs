using System.ComponentModel.DataAnnotations;

namespace MeuTeste.ViewModels
{
    public class CreateTesteViewModel
    {
        [Required] //Campo Obrigatório (DataAnnotation)
        public string Title { get; set; }
    }
}
