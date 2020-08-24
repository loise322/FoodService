using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Filldb
{
    class Program
    {
        static void Main( string[] args )
        {
            var allData = GetData();

            var userData = allData
                .GroupBy( ud => ud.UserId )
                .ToList();

            List<Order> legalResult = new List<Order>();

            foreach ( var data in userData )
            {
                var currentLegalId = 0;
                var x = data.OrderBy( d => d.OrderDate );
                foreach ( var item in x )
                {
                    if ( item.LegalId != currentLegalId )
                    {
                        var temp = new Order();
                        temp.TransferReason = 5;
                        temp.LegalId = item.LegalId;
                        temp.OrderDate = item.OrderDate;
                        temp.UserId = item.UserId;
                        currentLegalId = item.LegalId;
                        legalResult.Add( temp );
                    }
                }
            }

            Console.WriteLine( "1-Fill db, 2-Update inactive users" );
            int choose = Convert.ToInt32( Console.ReadLine() );
            switch ( choose )
            {
                case 1:
                    InsertData( legalResult );
                    break;
                case 2:
                    UpdateInactiveUser( legalResult );
                    break;
            }

            Console.WriteLine( "Complete" );
            Console.ReadLine();
        }

        private static void UpdateInactiveUser( List<Order> orders )
        {
            List<Users> data = new List<Users>();
            string connectionString = GetConnectionStringOutput();
            using ( SqlConnection connection = new SqlConnection() )
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText = string.Format( "SELECT id_user, enabled FROM Users" );
                    using ( SqlDataReader reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            Users user = new Users();
                            user.UserId = ( int )reader[ 0 ];
                            user.Enabled = ( bool )reader[ 1 ];
                            data.Add( user );
                        }
                    }
                }
                connection.Close();
            }

            Console.WriteLine( "Users loaded" );

            var inactiveUsers = data.Where( u => u.Enabled == false ).ToList();
            var allOrders = orders;
            foreach ( var user in inactiveUsers )
            {
                var lastOrder = allOrders.LastOrDefault( lo => lo.UserId == user.UserId );
                if ( lastOrder != null )
                {
                    user.LastOrderDate = lastOrder.OrderDate;
                    user.LegalId = lastOrder.LegalId;
                }
            }
            string connectionStringInput = GetConnectionStringInput();
            using ( SqlConnection connection = new SqlConnection( connectionStringInput ) )
            {
                connection.Open();
                using ( SqlCommand command = new SqlCommand() )
                {
                    foreach ( var user in inactiveUsers )
                    {
                        if ( user.LegalId == 0 ) continue;
                        command.Connection = connection;
                        command.CommandText = "INSERT into UserLegals (user_id, legal_id, start_date, transfer_reason) VALUES (@user_id, @legal_id, @start_date, @transfer_reason)";
                        command.Parameters.AddWithValue( "@user_id", user.UserId );
                        command.Parameters.AddWithValue( "@legal_id", user.LegalId );
                        command.Parameters.AddWithValue( "@start_date", user.LastOrderDate.AddDays(1) );
                        command.Parameters.AddWithValue( "@transfer_reason", 4 );


                        try
                        {
                            int recordsAffected = command.ExecuteNonQuery();
                            command.Parameters.Clear();
                        }
                        catch ( Exception ex )
                        {

                        }
                    }
                }
                connection.Close();
            }

            //return data;
        }

        private static void InsertData( List<Order> data )
        {
            string connectionString = GetConnectionStringInput();
            using ( SqlConnection connection = new SqlConnection( connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = new SqlCommand() )
                {
                    foreach ( var item in data )
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT into UserLegals (user_id, legal_id, start_date, transfer_reason) VALUES (@user_id, @legal_id, @start_date, @transfer_reason)";
                        command.Parameters.AddWithValue( "@user_id", item.UserId );
                        command.Parameters.AddWithValue( "@legal_id", item.LegalId );
                        command.Parameters.AddWithValue( "@start_date", item.OrderDate );
                        command.Parameters.AddWithValue( "@transfer_reason", item.TransferReason );


                        try
                        {
                            int recordsAffected = command.ExecuteNonQuery();
                            command.Parameters.Clear();
                        }
                        catch ( Exception ex )
                        {

                        }
                    }
                }
                connection.Close();
            }
        }

        private static List<Order> GetData()
        {
            List<Order> data = new List<Order>();
            string connectionString = GetConnectionStringOutput();

            using ( SqlConnection connection = new SqlConnection() )
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText = string.Format( "SELECT order_date, id_user, id_legal FROM Orders" );
                    using ( SqlDataReader reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            Order ord = new Order();
                            ord.OrderDate = ( DateTime )reader[ 0 ];
                            ord.UserId = ( int )reader[ 1 ];
                            ord.LegalId = ( int )reader[ 2 ];
                            data.Add( ord );
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }

        static private string GetConnectionStringOutput()
        {
            return "Data Source=DEVSQL.travelline.lan;Initial Catalog=food_20200207;Pooling=true;Integrated Security=SSPI";
        }

        static private string GetConnectionStringInput()
        {
            return "Data Source=DEVSQL.travelline.lan;Initial Catalog=food_20200207;Pooling = true;Integrated Security=SSPI";
        }
    }
}
