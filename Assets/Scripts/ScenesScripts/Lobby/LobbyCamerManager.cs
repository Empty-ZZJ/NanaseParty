using System.Collections.Generic;
using UnityEngine;
namespace ScenesScripts.Lobby
{
    public class LobbyCamerManager : MonoBehaviour
    {
        public AudioSource AudioPlayer_Effict;
        private Animator M_Animator;
        private List<string> CameraAnimates = new()
        {
            "ToAIChat",
            "ToPet"
        };

        /// <summary>
        /// 大厅摄像机位置索引
        /// </summary>
        private int CameraIndex;
        private void Start ()
        {

            M_Animator = this.gameObject.GetComponent<Animator>();
        }

        private void Update ()
        {

        }

        public void NextScene ()
        {
            AudioPlayer_Effict.PlayOneShot(Resources.Load<AudioClip>("Audio/Effict/LobbyCameraMove"));
            if (CameraIndex + 1 > CameraAnimates.Count - 1)
            {
                CameraIndex = 0;
            }
            else
                CameraIndex++;
            M_Animator.Play(CameraAnimates[CameraIndex]);
        }
        public void BackScene ()
        {
            AudioPlayer_Effict.PlayOneShot(Resources.Load<AudioClip>("Audio/Effict/LobbyCameraMove"));
            if (CameraIndex - 1 < 0)
            {
                CameraIndex = CameraAnimates.Count - 1;
            }
            else
                CameraIndex--;
            M_Animator.Play(CameraAnimates[CameraIndex]);
        }
        public void ToScene (string name)
        {
            M_Animator.Play(name);
        }
    }
}

