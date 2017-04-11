using UnityEngine;

public class CPU_Profiler : Profiler
{
    public float TotalMS
    {
        get;
        private set;
    }

    public float AvgMS
    {
        get;
        private set;
    }

    float _startTime = 0;
    float _nextOutputTime = 5;
    int _numFrames = 0;

    public override string GetInfo()
    {
        //string cpuTotalTime = "Total: " + TotalMS + " ms";

        string cpuAverageTime = "Average: " + AvgMS + " ms";

        string cpuInfo = "CPU: " + cpuAverageTime;

        return cpuInfo;
    }

    void Awake()
    {
        InitStartTime();
    }

    void Update()
    {
        if (IsCalculating)
        {
            CalculateCPU_Time();
        }
    }

    void CalculateCPU_Time()
    {
        _numFrames++;

        if (Time.time > _nextOutputTime)
        {
            TotalMS = (Time.time - _startTime) * 1000;
            AvgMS = (TotalMS / _numFrames);

            InitStartTime();
            _numFrames = 0;
            _nextOutputTime = Time.time + 5;
        }
    }

    void InitStartTime()
    {
        _startTime = Time.time;
    }
}