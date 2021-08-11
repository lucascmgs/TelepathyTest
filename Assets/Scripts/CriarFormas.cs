using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarFormas : MonoBehaviour
{
    ClientManager clientManager;
    void Start()
    {
        clientManager = FindObjectOfType<ClientManager>();
    }

    public void CriarForma(string indice)
    {
        clientManager.Send($"Spawn:{indice}");
    }
}
