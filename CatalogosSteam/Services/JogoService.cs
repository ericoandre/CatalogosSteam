using CatalogosSteam.Entities;
using CatalogosSteam.Exceptions;
using CatalogosSteam.InputModel;
using CatalogosSteam.Repositories;
using CatalogosSteam.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogosSteam.Services {
    public class JogoService : IJogoService {
        
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository) {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade) {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);
            return jogos.Select(jogo => new JogoViewModel {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id) {
            var jogo = await _jogoRepository.Obter(id);
            if (jogo == null)
                return null;

            return new JogoViewModel {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewModel> InserirJogo(JogoInputModel jogo) {
            var entidadeJogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);
            
            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();
            
            var jogoInsert = new Jogo {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
            
            await _jogoRepository.InserirJogo(jogoInsert);
            
            return new JogoViewModel {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }  
        
        public async Task AtualizarJogo(Guid id, JogoInputModel jogo) {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;
            
            await _jogoRepository.AtualizarJogo(entidadeJogo);
        }

        public async Task AtualizarJogo(Guid id, double preco) {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await _jogoRepository.AtualizarJogo(entidadeJogo);
        }

        public async Task ApagaJogo(Guid id) {
            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            await _jogoRepository.ApagaJogo(id);
        }

        public void Dispose() {
            _jogoRepository?.Dispose();
        }

    }
}