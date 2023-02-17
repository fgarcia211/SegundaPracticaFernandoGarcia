using SegundaPracticaFernandoGarcia.Models;
using System.Data;
using System.Data.SqlClient;

#region PROCEDURESSQL
/*CREATE OR ALTER PROCEDURE SP_INSERT_COMIC (@NOM NVARCHAR(150), @IMA NVARCHAR(600), @DESC NVARCHAR(500))
AS
	INSERT INTO COMICS VALUES ((SELECT MAX(IDCOMIC) FROM COMICS) + 1,@NOM,@IMA,@DESC)
GO*/
#endregion

namespace SegundaPracticaFernandoGarcia.Repositories
{
    public class RepositoryComicsSQL : IRepositoryComics
    {
        private SqlConnection cn;
        private SqlCommand com;

        private SqlDataAdapter adapter;
        private DataTable tablaComics;

        public RepositoryComicsSQL()
        {
            string connectionString = @"Data Source=LOCALHOST\DESAROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";

            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;

            string sql = "select * from comics";
            this.adapter = new SqlDataAdapter(sql,connectionString);
            this.tablaComics = new DataTable();
            this.adapter.Fill(this.tablaComics);

        }

        public List<Comic> GetComics()
        {
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           select new Comic
                           {
                               idComic = datos.Field<int>("IDCOMIC"),
                               Nombre = datos.Field<string>("NOMBRE"),
                               Imagen = datos.Field<string>("IMAGEN"),
                               Descripcion = datos.Field<string>("DESCRIPCION"),
                           };

            return consulta.ToList();
        }

        public void InsertComic(string nombre, string imagen, string descripcion)
        {
            SqlParameter pamNom = new SqlParameter("@NOM",nombre);
            SqlParameter pamIma = new SqlParameter("@IMA", imagen);
            SqlParameter pamDesc = new SqlParameter("@DESC", descripcion);

            this.com.Parameters.Add(pamNom);
            this.com.Parameters.Add(pamIma);
            this.com.Parameters.Add(pamDesc);

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";

            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();

            this.com.Parameters.Clear();
        }
    }
}
