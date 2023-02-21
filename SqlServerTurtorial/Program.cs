
using Microsoft.Data.SqlClient;

string connectionString = "server=localhost\\sqlexpress;" +
                            "database=SalesDB;" +
                            "trusted_connection=true;" +
                            "trustServerCertificate=true;";

SqlConnection sqlConn = new SqlConnection(connectionString);

sqlConn.Open();

if (sqlConn.State != System.Data.ConnectionState.Open) {
    throw new Exception("I screwed up my connection string");
}
Console.WriteLine("Connection opened successfully");

string sql = "SELECT * from Customers where sales > 90000 order by sales desc;";

SqlCommand cmd = new SqlCommand(sql, sqlConn);

SqlDataReader reader = cmd.ExecuteReader();

while (reader.Read()) {
    var id = Convert.ToInt32(reader["Id"]);
    var name = Convert.ToString(reader["Name"]);
    var city = Convert.ToString(reader["City"]);
    var state = Convert.ToString(reader["State"]);
    var sales = Convert.ToDecimal(reader["Sales"]);
    var active = Convert.ToBoolean(reader["Active"]);

    Console.WriteLine($"{id}|{name}|{city}, {state}|{sales}|{(active ? "Yes" : "No")}");
}
reader.Close();
//////////////////////////////////////////////////////////////////////////////////////////

string sqlOrder = "SELECT * from Orders;";

SqlCommand ord = new SqlCommand(sqlOrder, sqlConn);

reader = ord.ExecuteReader();


while(reader.Read()) {
    var id = Convert.ToInt32(reader["Id"]);
    var customerId = (reader["CustomerId"].Equals(System.DBNull.Value)) 
                    ? (int?)null
                    : Convert.ToInt32(reader["CustomerId"]);
    var date = Convert.ToDateTime(reader["Date"]);
    var description = Convert.ToString(reader["Description"]);

    Console.WriteLine($"{id} | {customerId} | {date} | {description}");
}
reader.Close();
//////////////////////////////////////////////////////////////////////////////////////////////

string sqls = "SELECT * from OrderLines;";

SqlCommand cmds = new SqlCommand(sqls, sqlConn);

reader = cmds.ExecuteReader();

while(reader.Read()) {
    var id = Convert.ToInt32(reader["Id"]);
    var orderId = Convert.ToInt32(reader["OrderId"]);
    var prod = Convert.ToString(reader["Product"]);
    var desc = (reader["Description"].Equals(System.DBNull.Value)) 
                                ? (string?)null
                               : Convert.ToString(reader["Description"]);
    var qty = Convert.ToInt32(reader["Quantity"]);
    var price = Convert.ToDecimal(reader["Price"]);
    Console.WriteLine($"{id}|{orderId}|{prod}|{desc}|{qty}|{price}");

  

}
reader.Close();


sqlConn.Close();