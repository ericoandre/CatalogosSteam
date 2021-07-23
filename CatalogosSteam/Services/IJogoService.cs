using CatalogosSteam.InputModel;
using CatalogosSteam.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogosSteam.Services {
    public interface IJogoService : IDisposable {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter(Guid id);
        Task<JogoViewModel> InserirJogo(JogoInputModel jogo);
        Task AtualizarJogo(Guid id, JogoInputModel jogo);
        Task AtualizarJogo(Guid id, double preco);
        Task ApagaJogo(Guid id);
    }
}