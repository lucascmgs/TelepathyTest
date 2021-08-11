using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Telepathy;
using System;

public class CommunicationManager : MonoBehaviour
{
    [SerializeField]
    private Text statusText;
    [SerializeField]
    private int defaultPort = 1223;
    [SerializeField]
    private Text targetIp;

    Telepathy.Client client;
    Telepathy.Server server;

    private void Awake()
    {
        Application.runInBackground = true;
        DontDestroyOnLoad(gameObject);
    }

    public void StartServer()
    {
        server = new Telepathy.Server(1000);
        Debug.Log("Servidor iniciado");
        server.OnConnected = (connectionId) => { statusText.text += $"{connectionId} conectou\n"; Debug.Log("Conectou"); };
        server.OnData = (connectionId, message) => statusText.text += BitConverter.ToString(message.Array, message.Offset, message.Count) + "\n";
        server.OnDisconnected = (connectionId) => statusText.text = "Fim da conexão";
    }

    public void StartClient()
    {
        client = new Telepathy.Client(1000);

        client.OnConnected = () => { statusText.text += "Conectado ao servidor\n"; Debug.Log("Conectou"); };
        client.OnDisconnected = () => statusText.text = "Desconectado do servidor";

    }

    public void ConnectToServer()
    {
        string ip;
        if(targetIp.text == "")
        {
            ip = "localhost";
        } else
        {
            ip = targetIp.text;
        }

        client.Connect(targetIp.text, defaultPort);
    }

    void OnApplicationQuit(){
        if(client != null)
        {
            client.Disconnect();
        }
        if(server != null)
        {
            server.Stop();
        }
        
    }
       
}
