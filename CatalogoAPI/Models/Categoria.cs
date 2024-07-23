using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CatalogoAPI.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string? Name { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}