using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
namespace ScenesScripts.Lobby.Pet
{
    public class PetSelectManager : MonoBehaviour
    {
        public int SelectIndex;
        public List<GameObject> Obj_Pets;
        public Transform PetsBack;
        private bool isOk = true;
        private GameObject LobbyCamera;
        private GameObject LobbyCanvas;
        public void Start ()
        {
            LobbyCamera = GameObject.Find("Main Camera");
            LobbyCanvas = GameObject.Find("MainCanvas");
            LobbyCamera.SetActive(false);
            LobbyCanvas.SetActive(false);
            foreach (var item in Obj_Pets)
            {
                item.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
        public void Button_Click_Change (int num)
        {
            if (!isOk) return;
            isOk = false;
            float _x = PetsBack.localPosition.x;
            if (num == 1)
            {

                SelectIndex++;
                _x -= 0.264f;
                if (SelectIndex > Obj_Pets.Count - 1)
                {
                    SelectIndex = 0;
                    _x = 0;
                }
            }
            else if (num == -1)
            {
                SelectIndex--;
                _x += 0.264f;
                if (SelectIndex < 0)
                {
                    SelectIndex = Obj_Pets.Count - 1;
                    _x = -1.32f;

                }
            }
            Obj_Pets[SelectIndex].transform.DOScale(1, 1f);
            for (int i = 0; i < Obj_Pets.Count - 1; i++)
            {
                if (i != SelectIndex)
                {
                    Obj_Pets[i].transform.DOScale(0.5f, 1f);
                }
            }
            PetsBack.DOLocalMove(new Vector3(_x, 0f, 0f), 1f).OnComplete(() =>
            {
                isOk = true;
            });
        }
        public void Button_Click_Select ()
        {

        }
        public void Button_Click_Close ()
        {
            LobbyCamera.SetActive(true);
            LobbyCanvas.SetActive(true);
            Destroy(this.gameObject);
        }

    }

}
