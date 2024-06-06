namespace ProductApi.Model
{
    public class Mysqlconnect
    {
        public string GetConnectionString()
        {
            string connString = "server=localhost; user id=root; password=Shreyapakhi@2005; database=product; ";
            return connString;
        }
    }
}
