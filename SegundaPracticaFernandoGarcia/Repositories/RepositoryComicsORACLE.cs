using Oracle.ManagedDataAccess.Client;
using SegundaPracticaFernandoGarcia.Models;
using System.Data;

#region PROCEDURESORACLE
/*CREATE OR REPLACE PROCEDURE SP_INSERT_COMIC
(P_NOMBRE COMICS.NOMBRE%TYPE,P_IMAGEN COMICS.IMAGEN%TYPE,P_DESCRIPCION COMICS.DESCRIPCION%TYPE)
AS
BEGIN
  INSERT INTO COMICS VALUES ((SELECT MAX(IDCOMIC) FROM COMICS) + 1,P_NOMBRE,P_IMAGEN,P_DESCRIPCION);
END;*/
#endregion

namespace SegundaPracticaFernandoGarcia.Repositories
{
    public class RepositoryComicsORACLE : IRepositoryComics
    {
        private OracleConnection cn;
        private OracleCommand com;

        private OracleDataAdapter adapter;
        private DataTable tablaComics;

        public RepositoryComicsORACLE()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True;User Id=SYSTEM;Password=oracle";

            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;

            string sql = "select * from comics";
            this.adapter = new OracleDataAdapter(sql, connectionString);
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
            OracleParameter pamNom = new OracleParameter(":P_NOMBRE", nombre);
            OracleParameter pamIma = new OracleParameter(":P_IMAGEN", imagen);
            OracleParameter pamDesc = new OracleParameter(":P_DESCRIPCION", descripcion);

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
