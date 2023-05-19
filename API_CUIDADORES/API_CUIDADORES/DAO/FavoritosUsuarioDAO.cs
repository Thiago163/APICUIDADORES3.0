using API_CUIDADORES.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CUIDADORES.DAO
{
    public class FavoritosUsuarioDAO
    {
        public List<FavoritosUsuarioDTO> Listar()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT fu.id, c.*, ti.tipo, sx.sexo, fu.usuario_id, fu.cuidador_id " +
            "FROM favoritosusuarios AS fu " +
            "JOIN cuidadores AS c ON fu.cuidador_id = c.id " +
            "JOIN tipos AS ti ON c.tipos_id = ti.id " +
            "JOIN sexos AS sx ON c.sexos_id = sx.id " +
            "WHERE c.tipos_id <> 2;";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var favoritos = new List<FavoritosUsuarioDTO>();

            while (dataReader.Read())
            {
                var favorito = new FavoritosUsuarioDTO();
                favorito.id = Convert.ToInt32(dataReader["id"]);
                favorito.usuario_id = Convert.ToInt32(dataReader["usuario_id"]);
                favorito.cuidador_id = Convert.ToInt32(dataReader["cuidador_id"]);
                // dados externos
                favorito.cuidador = new CuidadoresDTO();
                favorito.cuidador.tipo = dataReader["tipo"].ToString();
                favorito.cuidador.id = Convert.ToInt32(dataReader["id"]);
                favorito.cuidador.nome = dataReader["nome"].ToString();
                favorito.cuidador.sobrenome = dataReader["sobrenome"].ToString();
                favorito.cuidador.data_de_nasc = Convert.ToDateTime(dataReader["data_de_nasc"]);
                favorito.cuidador.cpf = dataReader["cpf"].ToString();
                favorito.cuidador.celular = dataReader["celular"].ToString();
                favorito.cuidador.endereco = dataReader["endereco"].ToString();
                favorito.cuidador.cep = dataReader["cep"].ToString();
                favorito.cuidador.email = dataReader["email"].ToString();
                favorito.cuidador.preco = Convert.ToDouble(dataReader["preco"]);
                favorito.cuidador.descricao = dataReader["descricao"].ToString();
                favorito.cuidador.imagem = dataReader["imagem"].ToString();
                favorito.cuidador.link = dataReader["link"].ToString();
                favorito.cuidador.sexo = dataReader["sexo"].ToString();
                favorito.cuidador.cidade = dataReader["cidade"].ToString();
                favorito.cuidador.estado = dataReader["estado"].ToString();
                favorito.cuidador.bairro = dataReader["bairro"].ToString();
                favorito.cuidador.senha = dataReader["senha"].ToString();

                favoritos.Add(favorito);
            }

            conexao.Close();

            return favoritos;
        }


        public void Cadastrar(FavoritosUsuarioDTO favorito)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO favoritosusuarios (usuario_id, cuidador_id)
                          VALUES (@usuario_id, @cuidador_id)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@usuario_id", favorito.usuario_id);
            comando.Parameters.AddWithValue("@cuidador_id", favorito.cuidador_id);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

        public void Remover(int id)
        {
            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = "DELETE FROM favoritosusuarios WHERE id = @id";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void Alterar(FavoritosUsuarioDTO favorito)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"UPDATE favoritosusuarios SET 
                        usuario_id = @usuario_id,
                        cuidador_id = @cuidador_id
                        WHERE id = @id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", favorito.id);
            comando.Parameters.AddWithValue("@usuario_id", favorito.usuario_id);
            comando.Parameters.AddWithValue("@cuidador_id", favorito.cuidador_id);

            comando.ExecuteNonQuery();

            conexao.Close();
        }
    }
}
