using System.Data.SqlClient;

namespace bursoto1
{
    public class SqlBaglanti
    {
        public SqlConnection baglanti()
        {
            // Bağlantı adresini buraya bir kez yazdık, bitti!
            SqlConnection baglan = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");

            // Eğer bağlantı kapalıysa açıp öyle gönderiyoruz ki formlarda uğraşmayalım
            if (baglan.State == System.Data.ConnectionState.Closed)
            {
                baglan.Open();
            }
            return baglan;
        }
    }
}