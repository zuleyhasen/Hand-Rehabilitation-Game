using UnityEngine;
using System.Diagnostics;

public class RunPythonScript : MonoBehaviour
{
    private static Process pythonProcess; 
    private static bool pythonScriptStarted = false; 

    void Start()
    {
        if (!pythonScriptStarted)
        {
            pythonProcess = RunPython();
            pythonScriptStarted = true;
        }
    }

    Process RunPython()
    {
        // Start Python code 
        return Process.Start(@"C:\pythonProject3\.venv\Scripts\python.exe", "C:/pythonProject3/main.py");
    }

    public void StopPython()
    {
        if (pythonProcess != null && !pythonProcess.HasExited)
        {
            pythonProcess.Kill();
        }
    }
}
