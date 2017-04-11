using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceDisplay : MonoBehaviour
{
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