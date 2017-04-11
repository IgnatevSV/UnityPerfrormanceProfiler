using UnityEngine;
using System.Collections.Generic;

public class CPU_Profiler : Profiler
{
    public float AvgMS
    {
        get;
        private set;
    }

    float _totalMS;
    float _startTime = 0f;
    int _numFrames = 0;
    static Dictionary<string, ProfilerRecording> _recordings = new Dictionary<string, ProfilerRecording>();



    public static void Begin(string id)
    {
        if (!_recordings.ContainsKey(id))
        {
            _recordings[id] = new ProfilerRecording(id);
        }

        _recordings[id].Start();
    }

    public static void End(string id)
    {
        _recordings[id].Stop();
    }

    public override string GetInfo()
    {
        string cpuOverallInfo = "Average:" + AvgMS + " ms";

        string cpuDetailedInfo = "";

        foreach (var entry in _recordings)
        {
            ProfilerRecording recording = entry.Value;

            cpuDetailedInfo += "\r\n" +
                "ID: " + recording.ID + " " +
                "Percent: " + recording.Percent.ToString("0.0") + " % " +
                "ms Per Frame: " + recording.MS_PerFrame.ToString("0.0") + " ms " +
                "ms Per Call: " + recording.MS_PerCall.ToString("0.0") + " ms " +
                "Times Per Frame: " + recording.TimesPerFrame;
        }

        string totalInfo = "CPU:\r\nOverall: " + cpuOverallInfo + "\r\nDetailed: " + cpuDetailedInfo;

        return totalInfo;
    }

    void Awake()
    {
        InitStartTime();
    }

    void InitStartTime()
    {
        _startTime = Time.time;
    }

    void Update()
    {
        if (IsCalculating)
        {
            CreateCalculations();
        }
    }

    void CreateCalculations()
    {
        _numFrames++;

        CalculateOverallInfo();

        CalculateDetailedInfo();

        _numFrames = 0;

        InitStartTime();
    }

    void CalculateOverallInfo()
    {
        _totalMS = (Time.time - _startTime) * 1000;
        AvgMS = (_totalMS / _numFrames);
    }

    void CalculateDetailedInfo()
    {
        foreach (var entry in _recordings)
        {
            ProfilerRecording recording = entry.Value;

            float recordedMS = (recording.Seconds * 1000);

            recording.Percent = (recordedMS * 100) / _totalMS;
            recording.MS_PerFrame = recordedMS / _numFrames;
            recording.MS_PerCall = recordedMS / recording.Count;
            recording.TimesPerFrame = recording.Count / (float)_numFrames;

            recording.Reset();
        }
    }

    private class ProfilerRecording
    {
        public string ID
        {
            get;
            private set;
        }

        int _count = 0;
        public int Count
        {
            get { return _count; }
        }

        float _accumulatedTime = 0;
        public float Seconds
        {
            get { return _accumulatedTime; }
        }

        public float Percent
        {
            get;
            set;
        }

        public float MS_PerFrame
        {
            get;
            set;
        }

        public float MS_PerCall
        {
            get;
            set;
        }

        public float TimesPerFrame
        {
            get;
            set;
        }

        float _startTime = 0;
        bool _started = false;

        public ProfilerRecording(string id)
        {
            this.ID = id;
        }

        public void Start()
        {
            if (_started) { BalanceError(); }
            _count++;
            _started = true;
            _startTime = Time.realtimeSinceStartup;
        }

        public void Stop()
        {
            float endTime = Time.realtimeSinceStartup;
            if (!_started) { BalanceError(); }
            _started = false;
            float elapsedTime = (endTime - _startTime);
            _accumulatedTime += elapsedTime;
        }

        public void Reset()
        {
            _accumulatedTime = 0;
            _count = 0;
            _started = false;
        }

        void BalanceError()
        {
            Debug.LogError("ProfilerRecording start/stops not balanced for '" + ID + "'");
        }
    }
}