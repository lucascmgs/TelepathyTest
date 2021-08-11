using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Telepathy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour
{
    [SerializeField]
    private Text statusText;
    [SerializeField]
    private InputField targetIp;
    [SerializeField]
    private int defaultPort;
    [SerializeField]
    private GameObject[] objects;

    private Client client;
    private void Awake()
    {
        Application.runInBackground = true;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        client = new Client(1000);
        Debug.Log("Cliente iniciado");
        client.OnConnected = () => {
            SceneManager.LoadScene("ClientStartScene"); 
        };
        client.OnData = (message) => Debug.Log(" Data: " + BitConverter.ToString(message.Array, message.Offset, message.Count));
        client.OnDisconnected = () => statusText.text = "Desconectado do servidor";
    }


    private void Update()
    {
        client.Tick(100);
    }

    public void Send(string message)
    {

        client.Send(new ArraySegment<byte>( Encoding.UTF8.GetBytes(message)));
    }


    public void ConnectToServer()
    {
        string ip;
        if (targetIp.text == "")
        {
            ip = "localhost";
        }
        else
        {
            ip = targetIp.text;
        }
        Debug.Log(targetIp.text);
        client.Connect(targetIp.text, defaultPort);
    }

    void OnApplicationQuit()
    {

        if (client != null)
        {
            client.Disconnect();
        }

    }
}
