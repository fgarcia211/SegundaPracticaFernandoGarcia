using SegundaPracticaFernandoGarcia.Models;

namespace SegundaPracticaFernandoGarcia.Repositories
{
    public interface IRepositoryComics
    {
        List<Comic> GetComics();
        void InsertComic(string nombre, string imagen, string descripcion);
    }
}
