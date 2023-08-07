using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public static event EventHandler OnPlayerTriggerEnter; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerTriggerEnter?.Invoke(this, EventArgs.Empty);
        }
    }
}
