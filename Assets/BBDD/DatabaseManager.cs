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
            SceneManager.LoadScene("Level1");

        }
    }

    public MySqlConnection Connect()
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
                       "vida_maxima INT, " +
                       "vida_actual INT, " +
                       "is_Garra BOOLEAN, " +
                       "is_Alas BOOLEAN, " +
                       "is_Dash BOOLEAN, " +
                       "is_Magia BOOLEAN," + 
                       "currency INT, " +
                       "escena VARCHAR(255), " + 
                       "pos_X FLOAT, " + 
                       "pos_Y FLOAT, " +
                       "current_damage INT, " +
                       "id INT AUTO_INCREMENT PRIMARY KEY);";

        MySqlCommand command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        Debug.Log("Tabla creada satisfactoriamente");
        //Hacemos el insert inicial
        initialInsert(connection);

        query = "create table tienda1(\r\nmax_health int,\r\nespada_lvl int,\r\nmax_health_price int,\r\nespada_lvl_price int\r\n);";
        command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();
        Debug.Log("La tabla tienda1 seha creado correctamente");
        InsertTienda1(connection);

        CrearTablasJefes(connection);
        InsertJefes(connection);


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

        string query = "INSERT INTO personaje (nivel_espada,is_garra,is_Alas,is_Dash,currency,escena,vida_maxima,vida_actual,is_Magia,current_damage) values (@nivelEspada,@isGarras,@isAlas,@isDash,@currency,@escena,@vidaMaxima,@vidaActual,@isMagia,@currentDamage)";

        int nivelEspada = 1;
        bool isGarras = false;
        bool isAlas = false;
        bool isDash = false;
        int currency = 0;
        string escena = "Level1";
        int vidaMaxima = 4;
        int vidaActual = 4;
        bool isMagia = false;
        int currentDamage = 1;

        MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@nivelEspada", nivelEspada);
        command.Parameters.AddWithValue("@isGarras", isGarras);
        command.Parameters.AddWithValue("@isAlas", isAlas);
        command.Parameters.AddWithValue("@isDash", isDash);
        command.Parameters.AddWithValue("@currency", currency);
        command.Parameters.AddWithValue("@escena", escena);
        command.Parameters.AddWithValue("@vidaMaxima", vidaMaxima);
        command.Parameters.AddWithValue("@vidaActual", vidaActual);
        command.Parameters.AddWithValue("@isMagia", isMagia);
        command.Parameters.AddWithValue("@currentDamage", currentDamage);

        try
        {
            command.ExecuteNonQuery();
            Debug.Log("Insert inicial, insertado correctamente");
        }
        catch(MySqlException e)
        {
            Debug.Log("Error en el initialCommit" + e.ToString());
        }

        //TIENDA 1

        
    }
    private void InsertTienda1(MySqlConnection connection)
    {
        string query = "INSERT INTO tienda1 (max_health,espada_lvl,max_health_price,espada_lvl_price) VALUES (@vidaMaxima,@mineralExtraño,@precioVida,@precioEspada)";

        int vidaMaxima = 1;
        int mineralExtraño = 1;
        int precioVida = 100;
        int precioEspada = 150;

        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@vidaMaxima", vidaMaxima);
        command.Parameters.AddWithValue("@mineralExtraño", mineralExtraño);
        command.Parameters.AddWithValue("@precioVida", precioVida);
        command.Parameters.AddWithValue("@precioEspada", precioEspada);

        try
        {
            command.ExecuteNonQuery();
        }
        catch(MySqlException e)
        {
            Debug.Log("Ha ocurrido un error al insertar los datos en tienda1" + e.ToString());
        }
    }
    private void limpiarTodo(MySqlConnection connection)
    {
        string query = "DROP TABLE personaje, tienda1, boss1,boss2,boss3,boss4";

        MySqlCommand command = new MySqlCommand(query,connection);

        try
        {
            command.ExecuteNonQuery();
            Debug.Log("Tablas limpiadas inicialmente");
        }
        catch (MySqlException)
        {
            Debug.Log("Error al limpiar la base de datos");
        }
        
    }
    public void CloseConnection(MySqlConnection connection)
    {
        if(connection != null && connection.State != ConnectionState.Closed)
        {
            connection.Close();
        }
        
    }
    public void CrearTablasJefes(MySqlConnection connection)
    {
        string query = "create table Boss1(\r\nhealth int,\r\nis_alive boolean\r\n);";

        MySqlCommand command = new MySqlCommand( query,connection);
        command.ExecuteNonQuery();

        query = "create table Boss2(\r\nhealth int,\r\nis_alive boolean\r\n);";
        command = new MySqlCommand( query,connection);
        command.ExecuteNonQuery();

        query = "create table Boss3(\r\nhealth int,\r\nis_alive boolean\r\n);";
        command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();

        query = "create table Boss4(\r\nhealth int,\r\nis_alive boolean\r\n);";
        command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery();



    }
    public void InsertJefes(MySqlConnection connection)
    {
        string query = "INSERT into Boss1 (health,is_alive) VALUES(@health,@isAlive)";
        int health = 15;
        bool isAlive = true;
        
        MySqlCommand command = new MySqlCommand(query,connection);
        command.Parameters.AddWithValue("@health", health);
        command.Parameters.AddWithValue("@isAlive",isAlive);

        command.ExecuteNonQuery();

        query = "INSERT into Boss2 (health,is_alive) VALUES(@health,@isAlive)";
        command = new MySqlCommand(query,connection);
        command.Parameters.AddWithValue("@health", health);
        command.Parameters.AddWithValue("@isAlive", isAlive);

        command.ExecuteNonQuery();

        query = "INSERT into Boss3 (health,is_alive) VALUES(@health,@isAlive)";
        command = new MySqlCommand (query,connection);
        command.Parameters.AddWithValue("@health", health);
        command.Parameters.AddWithValue("@isAlive", isAlive);

        command.ExecuteNonQuery();

        query = "INSERT into Boss4 (health,is_alive) VALUES(@health,@isAlive)";
        command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@health", health);
        command.Parameters.AddWithValue("@isAlive", isAlive);

        command.ExecuteNonQuery();



    }
}
