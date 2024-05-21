using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventarioManager : MonoBehaviour
{
    [SerializeField] TMP_Text nivelEspada;
    [SerializeField] TMP_Text vidaMaxima;
    [SerializeField] GameObject garrasImagen;
    [SerializeField] GameObject alasMonarca;
    [SerializeField] TMP_Text currency;

    private void Start()

    {
        garrasImagen.SetActive(false);
        alasMonarca.SetActive(false);

        nivelEspada.SetText("Level  " + Variables.nivelEspada);
        vidaMaxima.SetText("X " + Variables.vidaMaxima);

    }

    void Update()
    {
        if (Variables.isGarras == true) { garrasImagen.SetActive(true); }
        if (Variables.isAlas == true) {  alasMonarca.SetActive(true);}

        nivelEspada.SetText("Level  " + Variables.nivelEspada);
        vidaMaxima.SetText("X " + Variables.vidaMaxima);
        currency.SetText("Criptos " + Variables.currency);



    }
}
    