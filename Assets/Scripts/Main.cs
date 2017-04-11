using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    PerformanceDisplay performanceDisplay;

    List<Profiler> _profilers = new List<Profiler>();

    private void Start()
    {
        InitProfilers();
        InitProfilersDisplay();
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
        performanceDisplay = FindObjectOfType<PerformanceDisplay>();

        performanceDisplay.IsProfilingActive = true;
    }
}