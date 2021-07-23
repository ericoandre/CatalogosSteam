using CatalogosSteam.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogosSteam.Repositories {
    public interface IJogoRepository : IDisposable  {
        Task<List<Jogo>> Obter(int pagina, int quantidade);
        Task<List<Jogo>> Obter(string nome, string produtora);
        Task<Jogo> Obter(Guid id);
        Task InserirJogo(Jogo jogo);
        Task AtualizarJogo(Jogo jogo);
        Task ApagaJogo(Guid id);
    }
}