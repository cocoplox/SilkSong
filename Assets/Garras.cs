using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garras : MonoBehaviour
{
    // Start is called before the first frame update
    public void DarGarras()
    {
        Variables.isGarras = true;

        Variables.vidaMaxima++;
        Variables.nivelEspada++;
    }
}
