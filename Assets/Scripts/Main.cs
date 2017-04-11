using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    PerformanceDisplay _performanceDisplay;

    List<Profiler> _profilers = new List<Profiler>();

    void Start()
    {
        InitProfilers();
        InitProfilersDisplay();
    }

    
    void Update()
    {
        CPU_Profiler.Begin("Main:Update");
        CPU_Profiler.End("Main:Update");
    }
    

    void InitProfilers()
    {
        _profilers.AddRange(FindObjectsOfType<Profiler>());

        for (int i = 0; i < _profilers.Count; i++)
        {
            _profilers[i].IsCalculating = true;
        }
    }

    void InitProfilersDisplay()
    {
        _performanceDisplay = FindObjectOfType<PerformanceDisplay>();

        _performanceDisplay.IsProfilingActive = true;
    }
}