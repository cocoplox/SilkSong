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
            string query = "SELECT vida_maxima, vida_actual, nivel_espada, currency, is_Garra, is_Alas, is_Magia, is_Dash, escena FROM personaje WHERE id = 1";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Variables.vidaMaxima = reader.GetInt32(0);
                Variables.vidaActual = reader.GetInt32(1);
                Variables.nivelEspada = reader.GetInt32(2);
                Variables.currency = reader.GetInt32(3);
                Variables.isGarras = reader.GetBoolean(4);
                Variables.isAlas = reader.GetBoolean(5);
                Variables.isMagia = reader.GetBoolean(6);
                Variables.isDash = reader.GetBoolean(7);
                Variables.escena = reader.GetString(8);
            }

            reader.Close();
            Debug.Log("Datos cargados exitosamente.");

            // Aquí puedes cargar la escena después de cargar los datos, si es necesario
            SceneManager.LoadScene(Variables.escena);
        }
        else
        {
            Debug.LogWarning("No se pudo cargar los datos: la conexión a la base de datos no está abierta.");
        }
    }
}
