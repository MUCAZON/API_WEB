namespace TiendaApi.Conexion
{
    public class Conexionbd
    {
        private string connectionString = string.Empty;
        public Conexionbd()
        {
            //contruimos la conexion para acceder
            var constructor = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).AddJsonFile
                ("appsettings.json").Build();
            connectionString = constructor.GetSection
                ("ConnectionStrings:conexionMaestra").Value;
        }
        public string cadenaSQL()
        {
            return connectionString;
        }
    }
}
