using DjiskstraKelurahan.Web.Models;
using Kruskal.Core;

namespace DjiskstraKelurahan.Web.Services;

public interface IKelurahanService
{
    List<Kelurahan> GetAll();
    Kelurahan? GetByName(string name);
    Graph<Kelurahan> GetGraph();
}
