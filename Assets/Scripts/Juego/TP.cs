using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    public Transform Target;
    public GameObject ThePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ThePlayer.transform.position = Target.transform.position;
    }
}
