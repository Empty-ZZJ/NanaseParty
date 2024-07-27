using GameManager;
using System.Xml.Linq;
using UnityEngine;
namespace ScenesScripts.MiniGame.MusicGame
{
    public class MusicListManager : MonoBehaviour
    {
        public Transform ContentPanel;
        private void Start ()
        {

            var _data = Resources.Load<TextAsset>("Audio/MenheraMusic/info").text;
            XDocument doc = XDocument.Parse(_data);
            foreach (var song in doc.Root.Elements())
            {
                var _ = Instantiate(Resources.Load<GameObject>("GameObject/Scene/MiniGame/MusicGame/Item"), ContentPanel);

                foreach (var item in song.Elements())
                {

                    if (item.Name == "name")
                        _.GetComponent<ListItemController>().Title.text = item.Value;
                    if (item.Name == "id")
                    {
                        _.name = item.Value;
                        _.GetComponent<ListItemController>().id = item.Value;
                    }
                    Debug.Log($"{item.Name} : {item.Value}");
                }

            }

        }
        public void Button_Click_Close ()
        {
            var _ = new LoadingSceneManager<string>("Game-Lobby");
        }
    }


}
