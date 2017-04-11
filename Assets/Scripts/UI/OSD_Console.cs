using UnityEngine;
using UnityEngine.UI;

public class OSD_Console : MonoBehaviour
{
    [SerializeField]
    Text consoleLine;

    [SerializeField]
    int _maxDebugStringLength = 300;

    void OnEnable()
    {
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
    }

    public void LogMessage(string message, string stackTrace, LogType type)
    {
        if (consoleLine.text.Length > _maxDebugStringLength)
        {
            consoleLine.text = message + "\n";
        }
        else
        {
            consoleLine.text = message + "\n" + consoleLine.text + "\n";
        }
    }
}
