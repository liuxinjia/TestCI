using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GetAllFiles : MonoBehaviour
{
    public TextMeshProUGUI assetPathText;
    public TextMeshProUGUI curDirectoryText;
    public TextMeshProUGUI dataText;

    // Start is called before the first frame update
    void Update()
    {
        string path = Directory.GetCurrentDirectory();
        var rootCanvas = assetPathText.transform.root.transform as RectTransform;
        curDirectoryText.text = $"Canvas : {rootCanvas.sizeDelta.x} - {rootCanvas.sizeDelta.y}";

        dataText.text = $"{Screen.width} - {Screen.height}";

        assetPathText.text = Screen.currentResolution.ToString();

    }


}
