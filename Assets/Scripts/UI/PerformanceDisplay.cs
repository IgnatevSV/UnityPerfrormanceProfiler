using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceDisplay : MonoBehaviour
{
    [SerializeField]
    Text textLine;

    [SerializeField]
    float _delayBetweenMessage = 1f;

    List<Profiler> _profilers = new List<Profiler>();

    bool _isProfilingActive;

    public bool IsProfilingActive
    {
        get { return _isProfilingActive; }
        set
        {
            _isProfilingActive = value;
            StartCoroutine(DisplayProfilersInfo());

            if(value)
            {
                Application.logMessageReceived += LogMessage;
            }
            else
            {
                Application.logMessageReceived -= LogMessage;
            }
        }
    }

    void Start()
    {
        FindProfilers();
    }

    void FindProfilers()
    {
        _profilers.AddRange(FindObjectsOfType<Profiler>());
    }

    void LogMessage(string message, string stackTrace, LogType type)
    {
        textLine.text = message;
    }

    IEnumerator DisplayProfilersInfo()
    {
        while (_isProfilingActive)
        {
            string profilersInfo = "";
            for (int i = 0; i < _profilers.Count; i++)
            {
                if (_profilers[i].IsCalculating)
                {
                    profilersInfo += _profilers[i].GetInfo() + " | ";
                }
            }

            Debug.Log(profilersInfo);

            yield return new WaitForSeconds(_delayBetweenMessage);
        }
    }
}