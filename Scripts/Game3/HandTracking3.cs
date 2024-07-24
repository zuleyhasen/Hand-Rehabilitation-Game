using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking3 : MonoBehaviour
{
    // Start is called before the first frame update

    public UDPReceive3 udpReceive3;
    public GameObject[] handPoints;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!string.IsNullOrEmpty(udpReceive3.data))
        {
            string data = udpReceive3.data;
            data = data.Trim('[', ']');
            data = data.Replace(" ", "");

            StartCoroutine(ProcessDataWithDelay(data));
        }
    }

    IEnumerator ProcessDataWithDelay(string data)
    {
        // Veriyi i�lemeden �nce 1 saniye bekleyin
        yield return new WaitForSeconds(0.1f);

        string[] points = data.Split(',');
        // Veriyi i�leme devam edin...
        for (int i = 0; i < 21; i++)
        {

            float x = 7 - float.Parse(points[i * 3]) / 100;
            float y = float.Parse(points[i * 3 + 1]) / 100;

            handPoints[i].transform.localPosition = new Vector3(x, y);

        }
    }
}

