using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace bursoto1
{
    public class SqlBaglanti
    {
        // Bağlantı cümlesini tek bir yerden yönetiyoruz.
        // İleride sunucu değişirse sadece burayı değiştireceğiz.
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bursOtoDeneme1;Integrated Security=True";

        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(connectionString);

            // Eğer bağlantı kapalıysa açıp gönderiyoruz
            if (baglan.State == System.Data.ConnectionState.Closed)
            {
                baglan.Open();
            }

            return baglan;
        }
    }
}