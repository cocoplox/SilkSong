using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.SceneManagement;

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
            
            

            if(database != null)
            {
                Debug.Log(database);
                connection.Open();
                Debug.Log("Conexión a MySQL establecida correctamente.");
                empezarPartida(connection);
            }
            
            
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
        //Antes de nada, limpiamos todo, en caso de que hubiese algo anteriormente, ya que es una nueva partida
        limpiarTodo(connection);

        string query = "CREATE TABLE IF NOT EXISTS personaje (" +
                       "nivel_espada INT, " +
                       "is_Garra BOOLEAN, " +
                       "is_Alas BOOLEAN, " +
                       "is_Dash BOOLEAN, " +
                       "currency INT, " +
                       "escena VARCHAR(255), " + 
                       "pos_X FLOAT, " + 
                       "pos_Y FLOAT, " +
                       "id INT AUTO_INCREMENT PRIMARY KEY);";

        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        Debug.Log("Tabla creada satisfactoriamente");
        //Hacemos el insert inicial
        initialInsert(connection);
    }
    private void empezarPartida(MySqlConnection connection)
    {
        if(connection != null)
        {
            SceneManager.LoadScene("Level1");

        }
    }
    private void initialInsert(MySqlConnection connection)
    {
        if (connection == null)
        {
            print("Connexion nula");
            return;
        }

        string query = "INSERT INTO personaje (nivel_espada,is_garra,is_Alas,is_Dash,currency,escena) values (@nivelEspada,@isGarras,@isAlas,@isDash,@currency,@escena)";

        int nivelEspada = 1;
        bool isGarras = false;
        bool isAlas = false;
        bool isDash = false;
        int currency = 0;
        string escena = "Level1";

        MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@nivelEspada", nivelEspada);
        command.Parameters.AddWithValue("@isGarras", isGarras);
        command.Parameters.AddWithValue("@isAlas", isAlas);
        command.Parameters.AddWithValue("@isDash", isDash);
        command.Parameters.AddWithValue("@currency", currency);
        command.Parameters.AddWithValue("@escena", escena);

        try
        {
            command.ExecuteNonQuery();
            Debug.Log("Insert inicial, insertado correctamente");
        }
        catch(MySqlException e)
        {
            Debug.Log("Error en el initialCommit" + e.ToString());
        }


    }
    private void limpiarTodo(MySqlConnection connection)
    {
        string query = "DROP TABLE personaje";

        MySqlCommand command = new MySqlCommand(query,connection);

        try
        {
            command.ExecuteNonQuery();
            Debug.Log("Tablas limpiadas inicialmente");
        }
        catch (MySqlException e)
        {
            Debug.Log("Error al limpiar la base de datos");
        }
        
    }
}
