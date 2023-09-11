using UnityEngine;

public class DionUtil
{

    public static bool blocker;
    
    public static void setBlocker(bool value)
    {
        blocker = value;
    }
    
    public static bool isDion()
    {
        return true;
    }
    
    static bool WantsToQuit()
    {
        Debug.Log("Player prevented from quitting.");
        return blocker;
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.wantsToQuit += WantsToQuit;
    }
}