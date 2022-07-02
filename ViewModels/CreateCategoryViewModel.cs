using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public record class CreateCategoryViewModel(
        [Required(ErrorMessage = "Campo Name e obrigatório")]
        string Name, 
        [Required(ErrorMessage = "Campo Slug e obrigatório")]
        string Slug
    );
    
    public record class UpdateCategoryViewModel(
        [Required(ErrorMessage = "Campo Name obrigatório")]
        string Name, 
        [Required(ErrorMessage = "Campo Slug e obrigatório")]
        string Slug
    );
}