using UnityEngine;

public abstract class Profiler : MonoBehaviour {

    public bool IsCalculating
    {
        get;
        set;
    }

    public abstract string GetInfo();
}