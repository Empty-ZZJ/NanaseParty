using Common;
using System.IO;
using UnityEngine;

public class GameInit : MonoBehaviour
{

    public void Start ()
    {
        //60FPS
        Application.targetFrameRate = 60;

        if (!File.Exists($"{Application.persistentDataPath}/User/Config.hoilai"))
            FileManager.CreatTextFile($"{Application.persistentDataPath}/User/Config.hoilai");


    }
}
