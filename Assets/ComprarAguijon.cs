using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComprarAguijon : MonoBehaviour
{
    public void ComprarEspada()
    {
        if (Variables.currency >= 150)
        {
            Variables.nivelEspada++;
            Variables.currency -= 150;
            //Variables.currentDamage++;
        }
        
    }
    public void ComprarVidaMax()
    {
        if(Variables.currency >= 100)
        {
            Variables.vidaMaxima++;
            Variables.currency -= 100;
        }
    }
}
