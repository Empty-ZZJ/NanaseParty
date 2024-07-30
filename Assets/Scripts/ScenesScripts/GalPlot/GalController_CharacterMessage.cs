using UnityEngine;

namespace ScenesScripts.GalPlot
{

    public class GalController_CharacterMessage : MonoBehaviour
    {
        [SerializeField]
        public GalController_CharacterAnimate Gal_CharacterAnimate;
        public void HandleMessage (string MessageContent)
        {

            Gal_CharacterAnimate.Animate_type = MessageContent;
            Gal_CharacterAnimate.HandleMessgae();
        }
    }
}