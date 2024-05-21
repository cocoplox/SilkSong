using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.SceneManagement;

public class CargarPartidaScript : MonoBehaviour
{
    public void CargarPartida()
    {
        // Conectar a la base de datos
        MySqlConnection connection = ConnectarBD();
        if (connection != null)
        {
            CargarVariablesDesdeDatabase(connection);
            connection.Close();
        }
        else
        {
            Debug.LogError("No se pudo conectar a la base de datos.");
        }
        SceneManager.LoadScene(Variables.escena);
    }

    private MySqlConnection ConnectarBD()
    {
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

        builder.Server = "localhost";
        builder.UserID = PlayerPrefs.GetString("Username");
        builder.Password = PlayerPrefs.GetString("Password");
        builder.Database = PlayerPrefs.GetString("Database");

        MySqlConnection connection = new MySqlConnection(builder.ToString());

        try
        {
            connection.Open();
            return connection;
        }
        catch (MySqlException e)
        {
            Debug.LogError("Error al conectar con la base de datos: " + e.Message);
            return null;
        }
    }

    private void CargarVariablesDesdeDatabase(MySqlConnection connection)
    {
        if (connection != null && connection.State == ConnectionState.Open)
        {
            string query = "SELECT vida_maxima, vida_actual, nivel_espada, currency, is_Garra, is_Alas, is_Magia, is_Dash, escena, current_damage FROM personaje WHERE id = 1";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Variables.vidaMaxima = reader.GetInt32(0);
                Variables.vidaActual = reader.GetInt32(1);
                Variables.nivelEspada = reader.GetInt32(2);
                Variables.currency = reader.GetInt32(3) + 100;
                Variables.isGarras = reader.GetBoolean(4);
                Variables.isAlas = reader.GetBoolean(5);
                Variables.isMagia = reader.GetBoolean(6);
                Variables.isDash = reader.GetBoolean(7);
                Variables.escena = reader.GetString(8);
                Variables.currentDamage = reader.GetInt32(9);
            }

            reader.Close();
            Debug.Log("Datos cargados exitosamente.");

            // Aquí puedes cargar la escena después de cargar los datos, si es necesario
            SceneManager.LoadScene(Variables.escena);

            query = "SELECT health,is_alive FROM Boss1";
            command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                Variables.vidaJefe1 = reader.GetInt32(0);
                Variables.isJefe1Alive = reader.GetBoolean(1);
            }

            reader.Close();

            query = "SELECT health,is_alive FROM Boss2";
            command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                Variables.vidaJefe2 = reader.GetInt32(0);
                Variables.isJefe2Alive = reader.GetBoolean(1);
            }

            reader.Close();

            query = "SELECT health,is_alive FROM Boss3";
            command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                Variables.vidaJefe3 = reader.GetInt32(0);
                Variables.isJefe3Alive = reader.GetBoolean(1);
            }

            reader.Close();

            query = "SELECT health,is_alive FROM Boss4";
            command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                Variables.vidaJefe4 = reader.GetInt32(0);
                Variables.isJefe4Alive = reader.GetBoolean(1);
            }

            reader.Close();
        }
        else
        {
            Debug.LogWarning("No se pudo cargar los datos: la conexión a la base de datos no está abierta.");
        }
    }
}
