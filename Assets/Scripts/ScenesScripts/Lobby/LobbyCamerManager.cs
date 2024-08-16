using System.Collections.Generic;
using UnityEngine;
namespace ScenesScripts.Lobby
{
    public class LobbyCamerManager : MonoBehaviour
    {

        private Animator M_Animator;
        private List<string> CameraAnimates = new() { "" };

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
            if (CameraIndex - 1 < 0)
            {
                CameraIndex = CameraAnimates.Count - 1;
            }
            else
                CameraIndex--;
            M_Animator.Play(CameraAnimates[CameraIndex]);
        }
    }
}

