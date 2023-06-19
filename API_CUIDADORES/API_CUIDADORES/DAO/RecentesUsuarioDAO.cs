using API_CUIDADORES.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CUIDADORES.DAO
{
    public class RecentesUsuarioDAO
    {
        public List<RecentesUsuarioDTO> Listar()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var selectQuery = "SELECT re.id, c.*, ti.tipo, sx.sexo, re.usuario_id, re.cuidador_id " +
                             "FROM recentesusuarios AS re " +
                             "JOIN cuidadores AS c ON re.cuidador_id = c.id " +
                             "JOIN tipos AS ti ON c.tipos_id = ti.id " +
                             "JOIN sexos AS sx ON c.sexos_id = sx.id " +
                             "ORDER BY re.id DESC " +
                             "LIMIT 5;";

            var deleteQuery = "DELETE FROM recentesusuarios " +
                              "WHERE id NOT IN (SELECT id " +
                              "                 FROM (SELECT id " +
                              "                       FROM recentesusuarios " +
                              "                       ORDER BY id DESC " +
                              "                       LIMIT 5) AS sub);";

            var selectCommand = new MySqlCommand(selectQuery, conexao);
            var deleteCommand = new MySqlCommand(deleteQuery, conexao);

            var selectDataReader = selectCommand.ExecuteReader();
            var recentes = new List<RecentesUsuarioDTO>();

            while (selectDataReader.Read())
            {
                var recente = new RecentesUsuarioDTO();
                recente.id = Convert.ToInt32(selectDataReader["id"]);
                recente.usuario_id = Convert.ToInt32(selectDataReader["usuario_id"]);
                recente.cuidador_id = Convert.ToInt32(selectDataReader["cuidador_id"]);
                // dados externos
                recente.cuidador = new CuidadoresDTO();
                recente.cuidador.tipo = selectDataReader["tipo"].ToString();
                recente.cuidador.id = Convert.ToInt32(selectDataReader["usuario_id"]);
                recente.cuidador.nome = selectDataReader["nome"].ToString();
                recente.cuidador.sobrenome = selectDataReader["sobrenome"].ToString();
                recente.cuidador.data_de_nasc = Convert.ToDateTime(selectDataReader["data_de_nasc"]);
                recente.cuidador.cpf = selectDataReader["cpf"].ToString();
                recente.cuidador.celular = selectDataReader["celular"].ToString();
                recente.cuidador.endereco = selectDataReader["endereco"].ToString();
                recente.cuidador.cep = selectDataReader["cep"].ToString();
                recente.cuidador.email = selectDataReader["email"].ToString();
                recente.cuidador.preco = Convert.ToDouble(selectDataReader["preco"]);
                recente.cuidador.descricao = selectDataReader["descricao"].ToString();
                recente.cuidador.imagem = selectDataReader["imagem"].ToString();
                recente.cuidador.link = selectDataReader["link"].ToString();
                recente.cuidador.sexo = selectDataReader["sexo"].ToString();
                recente.cuidador.cidade = selectDataReader["cidade"].ToString();
                recente.cuidador.estado = selectDataReader["estado"].ToString();
                recente.cuidador.bairro = selectDataReader["bairro"].ToString();

                recentes.Add(recente);
            }

            selectDataReader.Close();

            deleteCommand.ExecuteNonQuery();

            conexao.Close();

            return recentes;
        }

        public void Cadastrar(RecentesUsuarioDTO recente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT IGNORE INTO recentesusuarios (usuario_id, cuidador_id)
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

            var query = "DELETE FROM recentesusuarios WHERE usuario_id = @usuario_id and cuidador_id = @cuidador_id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@usuario_id", usuario_id);
            comando.Parameters.AddWithValue("@cuidador_id", cuidador_id);

            comando.ExecuteNonQuery();
            conexao.Close();

        }

        public void Alterar(RecentesUsuarioDTO recente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"UPDATE recentesusuarios SET 
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
