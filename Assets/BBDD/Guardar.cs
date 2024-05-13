using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.SceneManagement;

public class GuardarPartidaScript : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void GuardarPartida()
    {
        // Conectar a la base de datos
        MySqlConnection connection = ConnectarBD();
        if (connection != null)
        {
            GuardarVariablesEnDatabase(connection);
            connection.Close();
        }
        else
        {
            Debug.LogError("No se pudo conectar a la base de datos.");
        }
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

    private void GuardarVariablesEnDatabase(MySqlConnection connection)
    {
        if (connection != null && connection.State == ConnectionState.Open)
        {
            string query = "UPDATE personaje SET vida_maxima = @vidaMaxima, vida_actual = @vidaActual, nivel_espada = @nivelEspada, currency = @currency, is_Garra = @isGarras, is_Alas = @isAlas, is_Magia = @isMagia, is_Dash = @isDash, escena = @escena WHERE id = 1";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@vidaMaxima", Variables.vidaMaxima);
            command.Parameters.AddWithValue("@vidaActual", Variables.vidaActual);
            command.Parameters.AddWithValue("@nivelEspada", Variables.nivelEspada);
            command.Parameters.AddWithValue("@currency", Variables.currency);
            command.Parameters.AddWithValue("@isGarras", Variables.isGarras);
            command.Parameters.AddWithValue("@isAlas", Variables.isAlas);
            command.Parameters.AddWithValue("@isMagia", Variables.isMagia);
            command.Parameters.AddWithValue("@isDash", Variables.isDash);
            command.Parameters.AddWithValue("@escena", (SceneManager.GetActiveScene().name));

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                Debug.Log("Datos guardados exitosamente. Filas afectadas: " + rowsAffected);
            }
            catch (MySqlException e)
            {
                Debug.LogError("Error al guardar datos en la base de datos: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("No se pudieron guardar los datos: la conexión a la base de datos no está abierta.");
        }
    }
    private void OnApplicationQuit()
    {
        
        GuardarPartida();
    }
}
