using System.Collections.ObjectModel;

namespace CatalogoAPI.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImagemUrl { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}