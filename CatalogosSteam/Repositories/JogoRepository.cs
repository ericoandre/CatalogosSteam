using CatalogosSteam.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogosSteam.Repositories {
    public class JogoRepository : IJogoRepository {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>() {
            {Guid.Parse("39b846ed-05b8-4d7c-8ef9-6478106bd02c"), new Jogo{ Id = Guid.Parse("39b846ed-05b8-4d7c-8ef9-6478106bd02c"), Nome = "Minecraft", Produtora = "Mojang", Preco =  134.90} },
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Jogo{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Halo Ce", Produtora = "Microsoft", Preco = 49.99} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Jogo{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Half-Life Alyx", Produtora = "Valve", Preco = 109.99} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Jogo{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Lineage II", Produtora = "NCSoft", Preco = 100} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Jogo{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Assassin's Creed", Produtora = "Ubisoft", Preco = 170} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Jogo{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Resident Evil", Produtora = "Capcom", Preco = 80} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Jogo{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Preco = 190} }
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade) {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id) {
            if (!jogos.ContainsKey(id))
                return Task.FromResult<Jogo>(null);

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora) {
            var retorno = new List<Jogo>();
            foreach(var jogo in jogos.Values){
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                    retorno.Add(jogo);
            }
            return Task.FromResult(retorno);
        }

        public Task InserirJogo(Jogo jogo){
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task AtualizarJogo(Jogo jogo){
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task ApagaJogo(Guid id) {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose() {
            //Fechar conexão com o banco
        }
    }
}
