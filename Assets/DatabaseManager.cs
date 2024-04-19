using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;

public class DatabaseManager : MonoBehaviour
{
    [Header("Database Properties")]
    public string host = "localhost";
    public string user;
    public string password;
    public string database;

    public void Iniciar()
    {
        // Obtener las credenciales guardadas
        user = PlayerPrefs.GetString("Username");
        password = PlayerPrefs.GetString("Password");
        database = PlayerPrefs.GetString("Database");

        // Conectar a la base de datos y crear tablas
        MySqlConnection connection = Connect();
        if (connection != null)
        {
            CrearTablasIniciales(connection);

        }
    }

    private MySqlConnection Connect()
    {
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server = host;
        builder.UserID = user;
        builder.Password = password;
        builder.Database = database;

        MySqlConnection connection = new MySqlConnection(builder.ToString());

        try
        {
            connection.Open();
            Debug.Log("Conexión a MySQL establecida correctamente.");
        }
        catch (MySqlException e)
        {
            Debug.LogError("Error al conectar a MySQL: " + e.ToString());
            return null; // Devolver null si la conexión falla
        }

        return connection;
    }

    private void CrearTablasIniciales(MySqlConnection connection)
    {
        string query = "CREATE TABLE IF NOT EXISTS personaje (" +
                       "nivel_espada INT, " +
                       "is_Garra BOOLEAN, " +
                       "is_Alas BOOLEAN, " +
                       "is_Dash BOOLEAN, " +
                       "currency INT" +
                       ");";

        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();

        Debug.Log("Tabla creada satisfactoriamente");
    }
}
