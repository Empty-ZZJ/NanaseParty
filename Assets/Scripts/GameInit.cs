using UnityEngine;

public class GameInit : MonoBehaviour
{

    public void Start ()
    {
        //60FPS
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


    }
}
