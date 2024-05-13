using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CredentialManager : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_InputField databaseField;
    public DatabaseManager databaseManager;

    public void OnSaveButtonClick()
    {
        // Obtener los valores de los campos de entrada
        string username = usernameField.text;
        string password = passwordField.text;
        string database = databaseField.text;

        // Guardar los valores usando PlayerPrefs
        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.SetString("Password", password);
        PlayerPrefs.SetString("Database", database);
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString("Username"));
        Debug.Log(PlayerPrefs.GetString("Password"));
        Debug.Log(PlayerPrefs.GetString("Database"));

        if (string.IsNullOrEmpty(database))
        {
            Debug.Log("La database no puede ser nula");
        }
        else 
        {
            // Iniciar el DatabaseManager
            databaseManager.Iniciar();
        }

        
    }
}
