using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorVidas : MonoBehaviour

{
    [SerializeField] TMP_Text contadorVida;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        contadorVida.SetText("x " + Variables.vidaActual);
    }
}
