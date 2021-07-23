using CatalogosSteam.Exceptions;
using CatalogosSteam.InputModel;
using CatalogosSteam.Services;
using CatalogosSteam.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogosSteam.Controllers.V1 {

    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosControllers: ControllerBase {

        private readonly IJogoService _jogoServerce;

        public JogosControllers(IJogoService jogoServerce){
            _jogoServerce = jogoServerce;
        }


        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>  
        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5){
            var jogos = await _jogoServerce.Obter(pagina, quantidade);
            if (jogos.Count() == 0)
                return NoContent();
            return Ok(jogos);
        }

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>  
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo){
            var jogo =  await _jogoServerce.Obter(idJogo);
            if (jogo == null)
                return NoContent();
            return Ok(jogo);
        }

        /// <summary>
        /// Inserir um jogo no catálogo
        /// </summary>
        /// <param name="jogoInputModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel){
            try {
                var jogo = await _jogoServerce.InserirJogo(jogoInputModel);
                return Ok(jogo);
            } catch(JogoJaCadastradoException ex){
                return UnprocessableEntity("Este jogo já faz parte do catalogo!");
            }
        }

        /// <summary>
        /// Atualizar um jogo no catálogo
        /// </summary>
        /// /// <param name="idJogo">Id do jogo a ser atualizado</param>
        /// <param name="jogoInputModel">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Cao o jogo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>  
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel){
            try {
                await _jogoServerce.AtualizarJogo(idJogo, jogoInputModel);
                return Ok();
            } catch (JogoNaoCadastradoException ex){
                return UnprocessableEntity("Não existe este jogo no catalogo!");
            }
        }

        /// <summary>
        /// Atualizar o preço de um jogo
        /// </summary>
        /// /// <param name="idJogo">Id do jogo a ser atualizado</param>
        /// <param name="preco">Novo preço do jogo</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute]  double preco){
            try {
                await _jogoServerce.AtualizarJogo(idJogo, preco);
                return Ok();
            } catch (JogoNaoCadastradoException ex){
                return UnprocessableEntity("Não existe este jogo no catalogo!");
            }
        }

        /// <summary>
        /// Excluir um jogo
        /// </summary>
        /// /// <param name="idJogo">Id do jogo a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response> 
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagaJogo([FromRoute] Guid idJogo){
            try {
                await _jogoServerce.ApagaJogo(idJogo);
                return Ok();
            } catch (JogoNaoCadastradoException ex) {
                return UnprocessableEntity("Não existe este jogo no catalogo!");
            }  
        }
    }
}