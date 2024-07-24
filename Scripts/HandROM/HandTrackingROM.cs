using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class HandTrackingROM : MonoBehaviour
{
    public UDPReceive4 udpReceive4;
    public GameObject[] handPoints;
    public Text textField;
    public InputField inputField;

    private string lastData = "";
    private List<string> romMessages = new List<string>(); 
    private List<string> allInputs = new List<string>(); 

    public void BackButtonOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PauseButtonOnClick()
    {
        textField.text = lastData;
        romMessages.Add(lastData);
        allInputs.Add(inputField.text);
        SaveDataToExcel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(udpReceive4.data))
        {
            string data = udpReceive4.data;
            data = data.Trim('[', ']');
            data = data.Replace(" ", "");

            lastData = GetLastFingerPositions(data);

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
            float z = float.Parse(points[i * 3 + 2]) / 100;

            handPoints[i].transform.localPosition = new Vector3(x, y, z);
        }
    }

    string GetLastFingerPositions(string data)
    {
        string[] points = data.Split(',');
        string lastFingerPositions = "";

        for (int i = 63; i < points.Length; i++)
        {
            string finger = "";
            if (i == 63) finger = "Thumb IP";
            else if (i == 64) finger = "Index DIP";
            else if (i == 65) finger = "Index PIP";
            else if (i == 66) finger = "Middle DIP";
            else if (i == 67) finger = "Middle PIP";
            else if (i == 68) finger = "Ring DIP";
            else if (i == 69) finger = "Ring PIP";
            else if (i == 70) finger = "Pinky DIP";
            else if (i == 71) finger = "Pinky PIP";

            string dataPortion = points[i];
            
            int indexOfDot = dataPortion.IndexOf('.');
            if (indexOfDot != -1)
            {
                dataPortion = dataPortion.Substring(0, indexOfDot);
            }

            lastFingerPositions += $"{finger}: {dataPortion}\n";
        }

        return lastFingerPositions;
    }

    void SaveDataToExcel()
    {
        // Dosya yolu oluþturma
        string filePath = Path.Combine(Application.persistentDataPath, "HandROM1.csv");

        // CSV dosyasýný oluþturma ve verileri yazma
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Input,Message"); // Baþlýk satýrý

            for (int i = 0; i < allInputs.Count; i++)
            {
                writer.WriteLine($"{allInputs[i]},{romMessages[i]}"); // Girdi ve mesajý yaz
            }
        }
    }
}
