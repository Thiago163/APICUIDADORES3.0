using API_CUIDADORES.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CUIDADORES.DAO
{
    public class RecentesCuidadorDAO
    {
        public List<RecentesCuidadorDTO> Listar()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var selectQuery = "SELECT re.id, c.*, ti.tipo, sx.sexo, re.usuario_id, re.cuidador_id " +
                             "FROM recentescuidadores AS re " +
                             "JOIN usuarios AS c ON re.usuario_id = c.id " +
                             "JOIN tipos AS ti ON c.tipos_id = ti.id " +
                             "JOIN sexos AS sx ON c.sexos_id = sx.id " +
                             "ORDER BY re.id DESC " +
                             "LIMIT 5;";

            var deleteQuery = "DELETE FROM recentescuidadores " +
                              "WHERE id NOT IN (SELECT id " +
                              "                 FROM (SELECT id " +
                              "                       FROM recentescuidadores " +
                              "                       ORDER BY id DESC " +
                              "                       LIMIT 5) AS sub);";

            var selectCommand = new MySqlCommand(selectQuery, conexao);
            var deleteCommand = new MySqlCommand(deleteQuery, conexao);

            var selectDataReader = selectCommand.ExecuteReader();
            var recentes = new List<RecentesCuidadorDTO>();

            while (selectDataReader.Read())
            {
                var recente = new RecentesCuidadorDTO();
                recente.id = Convert.ToInt32(selectDataReader["id"]);
                recente.usuario_id = Convert.ToInt32(selectDataReader["usuario_id"]);
                recente.cuidador_id = Convert.ToInt32(selectDataReader["cuidador_id"]);
                // dados externos
                recente.usuario = new UsuariosDTO();
                recente.usuario.tipo = selectDataReader["tipo"].ToString();
                recente.usuario.id = Convert.ToInt32(selectDataReader["usuario_id"]);
                recente.usuario.nome = selectDataReader["nome"].ToString();
                recente.usuario.sobrenome = selectDataReader["sobrenome"].ToString();
                recente.usuario.data_de_nasc = Convert.ToDateTime(selectDataReader["data_de_nasc"]);
                recente.usuario.cpf = selectDataReader["cpf"].ToString();
                recente.usuario.celular = selectDataReader["celular"].ToString();
                recente.usuario.endereco = selectDataReader["endereco"].ToString();
                recente.usuario.cep = selectDataReader["cep"].ToString();
                recente.usuario.email = selectDataReader["email"].ToString();
                recente.usuario.preco = Convert.ToDouble(selectDataReader["preco"]);
                recente.usuario.descricao = selectDataReader["descricao"].ToString();
                recente.usuario.imagem = selectDataReader["imagem"].ToString();
                recente.usuario.link = selectDataReader["link"].ToString();
                recente.usuario.sexo = selectDataReader["sexo"].ToString();
                recente.usuario.cidade = selectDataReader["cidade"].ToString();
                recente.usuario.estado = selectDataReader["estado"].ToString();
                recente.usuario.bairro = selectDataReader["bairro"].ToString();

                recentes.Add(recente);
            }

            selectDataReader.Close();

            deleteCommand.ExecuteNonQuery();

            conexao.Close();

            return recentes;
        }

        public void Cadastrar(RecentesCuidadorDTO recente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT IGNORE INTO recentescuidadores (usuario_id, cuidador_id)
                  VALUES (@usuario_id, @cuidador_id)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@usuario_id", recente.usuario_id);
            comando.Parameters.AddWithValue("@cuidador_id", recente.cuidador_id);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

        public void Remover(int usuario_id, int cuidador_id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "DELETE FROM recentescuidadores WHERE usuario_id = @usuario_id and cuidador_id = @cuidador_id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@usuario_id", usuario_id);
            comando.Parameters.AddWithValue("@cuidador_id", cuidador_id);

            comando.ExecuteNonQuery();
            conexao.Close();

        }

        public void Alterar(RecentesCuidadorDTO recente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"UPDATE recentescuidadores SET 
                        usuario_id = @usuario_id,
                        cuidador_id = @cuidador_id
                        WHERE id = @id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", recente.id);
            comando.Parameters.AddWithValue("@usuario_id", recente.usuario_id);
            comando.Parameters.AddWithValue("@cuidador_id", recente.cuidador_id);

            comando.ExecuteNonQuery();

            conexao.Close();
        }
    }
}
