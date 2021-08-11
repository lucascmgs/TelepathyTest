using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telepathy;
using UnityEngine.UI;
using System;
using System.Text;

public class ServerManager : MonoBehaviour
{
    private Server server;
    [SerializeField]
    private int defaultPort;
    [SerializeField]
    private Text statusText;
    [SerializeField]
    private GameObject[] objects;
    // Start is called before the first frame update

    private void Awake()
    {
        Application.runInBackground = true;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        server = new Server(1000);
        Debug.Log("Servidor iniciado");
        server.OnConnected = (connectionId) => { statusText.text += $"{connectionId} conectou\n"; Debug.Log("Conectou"); };
        server.OnData = (connectionId, message) =>
        {
            string mensagemRecebida = Encoding.UTF8.GetString(message.Array);
            if (mensagemRecebida.Length > 1)
            {
                Debug.Log(mensagemRecebida);
                var partes = mensagemRecebida.Split(':');
                Spawn(Int32.Parse(partes[1]));
            }
        };
            server.OnDisconnected = (connectionId) => statusText.text = "Fim da conexão";
        server.Start(defaultPort);
    }

    private void Update()
    {
        server.Tick(100);
    }

    private void Spawn(int indice)
    {
        var newObject = Instantiate(objects[indice]);
        newObject.transform.position = new Vector3(Mathf.Cos(Time.time)*2, Mathf.Sin(Time.time)*2, 0);
    }
    void OnApplicationQuit()
    {
        
        if (server != null)
        {
            server.Stop();
        }

    }
}
