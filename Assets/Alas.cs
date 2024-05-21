using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAlas : MonoBehaviour
{
    // Start is called before the first frame update
    public void DarGarras()
    {
        Variables.isAlas = true;

        Variables.vidaMaxima++;
        Variables.nivelEspada++;
    }
}
