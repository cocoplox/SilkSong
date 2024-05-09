using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

public class Variables : MonoBehaviour
{
    DatabaseManager databaseManager;
    //INTS
    [Header("Stats")]
     public static int vidaActual;
     public static int nivelEspada;
     public static int currency;
     public static int vidaMaxima;

    //BOOLS
     public static bool isGarras;
     public static bool isAlas;
     public static bool isMagia;
     public static bool isDash;

     public static string escena;

}
