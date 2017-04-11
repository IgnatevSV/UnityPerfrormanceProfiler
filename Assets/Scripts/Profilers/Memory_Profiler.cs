public class Memory_Profiler : Profiler
{
    public float MemoryUsed
    {
        get;
        private set;
    }

    public float MemoryTotal
    {
        get;
        private set;
    }

    public override string GetInfo()
    {
        string memoryInfo = "Memory: " + MemoryUsed.ToString() + " / " + MemoryTotal.ToString() + " MB";

        return memoryInfo;
    }

    void Update()
    {
        if(IsCalculating)
        {
            CalculateMemoryUsage();
        }
    }

    void CalculateMemoryUsage()
    {
        MemoryUsed = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemory() / (1024 * 1024);

        MemoryTotal = UnityEngine.Profiling.Profiler.GetTotalReservedMemory() / (1024 * 1024);
    }
}