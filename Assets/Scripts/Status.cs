using System.Collections;
using System.Collections.Generic;

public static class Status
{
    public static bool inDialogue;
    public static bool initiated = false;

    public static int[] remaining;
    public static bool wolfFreed;
    public static bool startAreaCleared;
    public static bool pulcinoForest;
    public static bool pulcinoVillage;
    public static int prevArea;

    public static void Init()
    {
        if(!initiated)
        {
            initiated = true;
            wolfFreed = false;
            startAreaCleared = false;
            pulcinoForest = false;
            pulcinoVillage = false;
            prevArea = 0;
            remaining = new int[10];
            remaining[0] = 4;
            remaining[1] = 2;
            remaining[2] = 1;
            remaining[3] = 1;
        } 
        
    }
}
