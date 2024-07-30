using UnityEngine;

public class GameInit : MonoBehaviour
{

    public void Start ()
    {
        //120FPS
        Application.targetFrameRate = 120;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.runInBackground = true;

    }
}
