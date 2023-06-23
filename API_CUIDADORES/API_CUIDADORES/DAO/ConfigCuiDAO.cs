﻿using API_CUIDADORES.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CUIDADORES.DAO
{
    public class ConfigCuiDAO
    {
        public List<CuidadoresDTO> Listar(int id)
        {
            var cuidadores = new List<CuidadoresDTO>();

            using (var conexao = ConnectionFactory.Build())
            {
                var query = "SELECT ti.tipo, cui.id, cui.nome, cui.sobrenome, cui.data_de_nasc, cui.cpf, cui.celular, cui.endereco, " +
                    "cui.cep, cui.email, cui.preco, cui.descricao, cui.imagem, " +
                    "cui.link, sx.sexo, cui.cidade, cui.estado, cui.bairro, 'senha' AS senha " +
                    "FROM cuidadores AS cui " +
                    "JOIN sexos AS sx ON cui.sexos_id = sx.id " +
                    "JOIN tipos AS ti ON cui.tipos_id = ti.id " +
                    "WHERE cui.id = @id AND cui.tipos_id <> 2";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    conexao.Open();

                    using (var dataReader = comando.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var cuidador = new CuidadoresDTO();

                            cuidador.tipo = dataReader["tipo"].ToString();
                            cuidador.id = Convert.ToInt32(dataReader["id"]);
                            cuidador.nome = dataReader["nome"].ToString();
                            cuidador.sobrenome = dataReader["sobrenome"].ToString();
                            cuidador.cpf = dataReader["cpf"].ToString();
                            cuidador.celular = dataReader["celular"].ToString();
                            cuidador.endereco = dataReader["endereco"].ToString();
                            cuidador.cep = dataReader["cep"].ToString();
                            cuidador.email = dataReader["email"].ToString();
                            cuidador.preco = Convert.ToDouble(dataReader["preco"]);
                            cuidador.descricao = dataReader["descricao"].ToString();
                            cuidador.data_de_nasc = dataReader.GetDateTime(dataReader.GetOrdinal("data_de_nasc"));
                            cuidador.imagem = dataReader["imagem"].ToString();
                            cuidador.link = dataReader["link"].ToString();
                            cuidador.sexo = dataReader["sexo"].ToString();
                            cuidador.cidade = dataReader["cidade"].ToString();
                            cuidador.estado = dataReader["estado"].ToString();
                            cuidador.bairro = dataReader["bairro"].ToString();
                            cuidador.senha = dataReader["senha"].ToString();

                            cuidadores.Add(cuidador);
                        }
                    }
                }
            }

            return cuidadores;
        }

        public void Remover(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"
        DELETE FROM favoritoscuidadores WHERE usuario_id = @id;
        DELETE FROM estrelascuidador WHERE usuario_id = @id;
        DELETE FROM favoritosusuarios WHERE usuario_id = @id;
        DELETE FROM estrelasusuario WHERE usuario_id = @id;
        DELETE FROM cuidadores WHERE id = @id;
    ";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void Alterar(CuidadoresDTO cuidador)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"UPDATE cuidadores SET 
                        tipos_id = @tipos_id,
                        nome = @nome,
                        sobrenome = @sobrenome,
                        cidade = @cidade,
			            sexos_id = @sexos_id,
			            estado = @estado,
                        data_de_nasc = @data_de_nasc,
                        cpf = @cpf,
                        celular = @celular,
                        endereco = @endereco,
                        cep = @cep,
                        email = @email,
                        preco = @preco,
                        senha = @senha,
                        descricao = @descricao,
			            link = @link,
                        bairro = @bairro,
                        imagem = @imagem
                  WHERE id = @id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", cuidador.id);
            comando.Parameters.AddWithValue("@tipos_id", cuidador.tipo);
            comando.Parameters.AddWithValue("@cidade", cuidador.cidade);
            comando.Parameters.AddWithValue("@sexos_id", cuidador.sexo);
            comando.Parameters.AddWithValue("@estado", cuidador.estado);
            comando.Parameters.AddWithValue("@nome", cuidador.nome);
            comando.Parameters.AddWithValue("@sobrenome", cuidador.sobrenome);
            comando.Parameters.AddWithValue("@data_de_nasc", cuidador.data_de_nasc);
            comando.Parameters.AddWithValue("@cpf", cuidador.cpf);
            comando.Parameters.AddWithValue("@celular", cuidador.celular);
            comando.Parameters.AddWithValue("@endereco", cuidador.endereco);
            comando.Parameters.AddWithValue("@cep", cuidador.cep);
            comando.Parameters.AddWithValue("@email", cuidador.email);
            comando.Parameters.AddWithValue("@preco", cuidador.preco);
            comando.Parameters.AddWithValue("@senha", cuidador.senha);
            comando.Parameters.AddWithValue("@descricao", cuidador.descricao);
            comando.Parameters.AddWithValue("@link", cuidador.link);
            comando.Parameters.AddWithValue("@bairro", cuidador.bairro);
            comando.Parameters.AddWithValue("@imagem", cuidador.imagem);

            comando.ExecuteNonQuery();
            conexao.Close();

        }

    }
}
