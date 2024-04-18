using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class DatabaseManager : MonoBehaviour
{
    #region VARIABLES
    [Header("Database Properties")]
    public string Host = "localhost";
    public string User = "root";
    public string Password = "Chochoplox1";
    public string Database = "test";
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        Connect();
    }
    #endregion

    #region
    private void Connect()
    {
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server = Host;
        builder.UserID = User;
        builder.Password = Password;
        builder.Database = Database;
        try
        {
            using (MySqlConnection connection = new MySqlConnection(builder.ToString()))
            {
                connection.Open();
                Debug.Log("MySQL - Oppened Connection");
            }
        }
        catch (MySqlException e)
        {
            Debug.LogException(e);
        }
    }
    #endregion
}
