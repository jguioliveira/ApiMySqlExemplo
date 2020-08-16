using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace ApiExemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        [HttpGet]
        public List<Pessoa> Get()
        {
            //MySql.Data.MySqlClient.MySql
            string stringDeConexao = "Server=192.168.56.101;Database=Exemplo;Uid=root;Pwd=su_inventory#20;";
            MySql.Data.MySqlClient.MySqlConnection mySqlconnection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            string comando = "Select * from Pessoa";
            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand();

            mySqlCommand.Connection = mySqlconnection;
            mySqlCommand.CommandText = comando;

            //string comando = "Nome_Procedure";
            //MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand();
            //mySqlCommand.Connection = mySqlconnection;
            //mySqlCommand.CommandText = comando;
            //mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            mySqlconnection.Open();

            MySqlDataReader tabela = mySqlCommand.ExecuteReader();            

            bool existeDados = tabela.Read();

            List<Pessoa> pessoas = new List<Pessoa>();

            while (existeDados) //a condicao for verdadeira, vai executar o bloco de código
            {
                // depois de ler tdos os dados
                Pessoa pessoa = new Pessoa();
                pessoa.Id = tabela.GetString("Id");
                pessoa.Nome = tabela.GetString("Nome");
                pessoa.Email = tabela.GetString("Email");
                pessoa.Documento = tabela.GetString("Documento");
                pessoa.DataNascimento = tabela.GetDateTime("DataNascimento");
                pessoa.Idade = tabela.GetInt32("Idade");

                pessoas.Add(pessoa);

                existeDados = tabela.Read();
            }

            mySqlconnection.Close();

            return pessoas;
        }

        [HttpGet]
        [Route("{id}")]
        public Pessoa Get(string id)
        {
            //MySql.Data.MySqlClient.MySql
            string stringDeConexao = "Server=192.168.56.101;Database=Exemplo;Uid=root;Pwd=su_inventory#20;";
            MySql.Data.MySqlClient.MySqlConnection mySqlconnection = new MySql.Data.MySqlClient.MySqlConnection(stringDeConexao);

            //Select * from Pessoa where Id = 'BBB'
            string comando = $"Select * from Pessoa where Id = '{id}'";

            MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand();

            mySqlCommand.Connection = mySqlconnection;
            mySqlCommand.CommandText = comando;

            mySqlconnection.Open();

            MySqlDataReader tabela = mySqlCommand.ExecuteReader();

            bool existeDados = tabela.Read();
            Pessoa pessoa = new Pessoa();

            if (existeDados) //a condicao for verdadeira, vai executar o bloco de código
            {             
                pessoa.Id = tabela.GetString("Id");
                pessoa.Nome = tabela.GetString("Nome");
                pessoa.Email = tabela.GetString("Email");
                pessoa.Documento = tabela.GetString("Documento");
                pessoa.DataNascimento = tabela.GetDateTime("DataNascimento");
                pessoa.Idade = tabela.GetInt32("Idade");
            }

            mySqlconnection.Close();

            return pessoa;
        }

        [HttpGet]
        [Route("de/{dataInicial}/ate/{dataFinal}")]
        public object Get(string dataInicial, string dataFinal)
        {
            

            return Ok();
        }

    }

    public class Pessoa
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }

    }
}
