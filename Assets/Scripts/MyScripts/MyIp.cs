using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using UnityEngine;
using UnityEngine.UI;

public class MyIp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
        var externalIp = IPAddress.Parse(externalIpString);
        GetComponent<Text>().text = externalIp.ToString();
    }

    
}
