using API_CUIDADORES.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CUIDADORES.DAO
{
    public class FavoritosCuidadorDAO
    {
        public List<FavoritosCuidadorDTO> Listar()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM favoritoscuidadores";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var favoritos = new List<FavoritosCuidadorDTO>();

            while (dataReader.Read())
            {
                var favorito = new FavoritosCuidadorDTO();
                favorito.id = Convert.ToInt32(dataReader["id"]);
                favorito.usuario_id = Convert.ToInt32(dataReader["usuario_id"]);
                favorito.cuidador_id = Convert.ToInt32(dataReader["cuidador_id"]);

                favoritos.Add(favorito);
            }

            conexao.Close();

            return favoritos;
        }

        public void Cadastrar(FavoritosCuidadorDTO favorito)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO favoritoscuidadores (usuario_id, cuidador_id)
                          VALUES (@usuario_id, @cuidador_id)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@usuario_id", favorito.usuario_id);
            comando.Parameters.AddWithValue("@cuidador_id", favorito.cuidador_id);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

        public void Remover(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"Delete from favoritoscuidadores where id=@id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();
            conexao.Close();

        }

        public void Alterar(FavoritosCuidadorDTO favorito)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"UPDATE favoritoscuidadores SET 
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
