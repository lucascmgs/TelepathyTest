using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyIp : MonoBehaviour
{
    // Start is called before the first frame update
    public void CopyIpToClipboard()
    {
        var text = GetComponentInParent<Text>().text;
        GUIUtility.systemCopyBuffer = text;

    }
}
