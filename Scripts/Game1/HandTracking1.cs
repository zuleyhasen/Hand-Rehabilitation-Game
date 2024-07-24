using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking1 : MonoBehaviour
{
    public UDPReceive1 udpReceive1;
    public GameObject[] handPoints;
    private float detectionTime;
    private float updateTime;

    void Start()
    {
        detectionTime = 0f;
        updateTime = 0f;
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(udpReceive1.data))
        {
            string data = udpReceive1.data;
            data = data.Trim('[', ']');
            data = data.Replace(" ", "");

            // El verisi alýndýðýnda zaman damgasý al
            detectionTime = Time.time;

            StartCoroutine(ProcessDataWithDelay(data));
        }
    }

    IEnumerator ProcessDataWithDelay(string data)
    {

        yield return new WaitForSeconds(0.1f);

        string[] points = data.Split(',');

        for (int i = 0; i < 21; i++)
        {
            float x = 7 - float.Parse(points[i * 3]) / 100;
            float y = float.Parse(points[i * 3 + 1]) / 100;

            handPoints[i].transform.localPosition = new Vector3(x, y);
        }


        updateTime = Time.time;

        float latency = (updateTime - detectionTime) * 1000f; // Milisaniye cinsinden gecikme
        Debug.Log($"Latency: {latency} ms");

    }
}
