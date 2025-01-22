using ClientesApp.API.Dtos;
using ClientesApp.API.Ennums;
using ClientesApp.API.Entities;
using ClientesApp.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase

    {
        [HttpPost]
        public IActionResult Post(ClienteRequestDto dto)

        {
            var cliente = new Cliente();

            cliente.Id = Guid.NewGuid();
            cliente.Nome = dto.Nome;
            cliente.Email = dto.Email;
            cliente.Cpf = dto.Cpf;
            cliente.Categoria = (Categoria) dto.Categoria;
            cliente.DataCriacao = DateTime.Now;
            cliente.DataUltimaAlteracao = DateTime.Now;
            cliente.Ativo = true;

            //Cadastrando o cliente no banco de dados.
            var clienteRepository = new ClienteRepository();
            clienteRepository.Inserir(cliente);



            return Ok(new { mensagem = "Cliente cadastrado com sucesso!" });

        }

        /// <summary>
        /// Serviço para atualização de cliente na API
        /// </summary>
       
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, ClienteRequestDto dto)
        {
            //Instanciando um objeto da classe Cliente
            var cliente = new Cliente();

            cliente.Id = id;
            cliente.Nome = dto.Nome;
            cliente.Email = dto.Email;
            cliente.Cpf = dto.Cpf;
            cliente.Categoria = (Categoria)dto.Categoria;

            //Atualizando o cliente no banco de dados

            var clienteRepository = new ClienteRepository();
            clienteRepository.Atualizar(cliente);

            return Ok( new {mensagem = "Cliente atualizado com sucesso"});
        }


        /// <summary>
        /// Serviço para exclusão de cliente na API
        /// </summary>
      
        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            //Excluindo o cliente no banco de dados
            var clienteRepository = new ClienteRepository();
            clienteRepository.Excluir(id);

            return Ok(new {mensagem = "Cliente excluido com sucesso"});
        }

        [HttpGet]

        public IActionResult Get()

        {
            //Consultando os clientes no banco de dados
            var clienteRepository = new ClienteRepository();
            var clientes = clienteRepository.Consultar();
            return Ok(clientes);

        }

        /// <summary>
        /// Serviço para consulta de cliente por Id na API
        /// </summary>
      
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            //Consultando o cliente no banco de dados

            var clienteRepository = new ClienteRepository();
            var cliente = clienteRepository.ObterPorId(id);
            return Ok(cliente);
        }

        [HttpGet]
        [Route("dashboard")]

        public IActionResult GetDashboard()
        {
            //Consultando os clientes ativos no banco de dados
            var clienteRepository = new ClienteRepository();
            var dados = clienteRepository.ConsultarDashboard();
            return Ok(dados);
        }
    }
}
