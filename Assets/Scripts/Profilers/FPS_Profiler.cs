using UnityEngine;

public class FPS_Profiler : Profiler
{
    public int AvgFPS
    {
        get;
        private set;
    }

    public int MaxFPS
    {
        get;
        private set;
    }

    public int MinFPS
    {
        get;
        private set;
    }

    int[] _fpsBuffer;
    int _fpsBufferIndex;

    int _frameRange = 60;

    public override string GetInfo()
    {
        string fpsInfo =
            "FPS: " +
            "Max: " + MaxFPS.ToString() + " " +
            "Avg: " + AvgFPS.ToString() + " " +
            "Min: " + MinFPS.ToString();

        return fpsInfo;
    }

    void Update()
    {
        if (IsCalculating)
        {
            CalculateFPS();
        }
    }

    void CalculateFPS()
    {
        UpdateBuffer();

        int sum = 0;
        int highest = 0;
        int lowest = int.MaxValue;

        for (int i = 0; i < _frameRange; i++)
        {
            int fps = _fpsBuffer[i];
            sum += fps;
            if (fps > highest)
            {
                highest = fps;
            }
            if (fps < lowest)
            {
                lowest = fps;
            }
        }

        AvgFPS = sum / _frameRange;
        MaxFPS = highest;
        MinFPS = lowest;
    }

    void InitializeBuffer()
    {
        if (_frameRange <= 0)
        {
            _frameRange = 1;
        }
        _fpsBuffer = new int[_frameRange];
        _fpsBufferIndex = 0;
    }

    void UpdateBuffer()
    {
        if (_fpsBuffer == null || _fpsBuffer.Length != _frameRange)
        {
            InitializeBuffer();
        }

        _fpsBuffer[_fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if (_fpsBufferIndex >= _frameRange)
        {
            _fpsBufferIndex = 0;
        }
    }
}