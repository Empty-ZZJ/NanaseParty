using UnityEngine;

namespace TetraCreations.Attributes
{
    public class AttributesExample : MonoBehaviour
    {
        #pragma warning disable 0414
        /// DrawIf
        [Title("Nice title attribute", CustomColor.Yellow, CustomColor.Orange, 2f, 20f)]
        public bool DrawOtherFields = false;

        [DrawIf(nameof(DrawOtherFields), true)]
        public string Name;

        [DrawIf(nameof(DrawOtherFields), true)]
        public int Quantity;

        [DrawIf(nameof(DrawOtherFields), true)]
        public float Speed;

        [DrawIf(nameof(DrawOtherFields), true)]
        public int[] Array;

        /// ReadOnly & DrawIf : DisablingType.ReadOnly
        [Space(20f)]
        [Title("Read Only Variables", lineHeight:2f, spacing:20f)]
        [ReadOnly]
        public float ReadOnlyFloat;

        [DrawIf(nameof(ToggleToEdit), true, DisablingType.ReadOnly)] 
        public int DisabledByDefault;

        /// HelpBox
        [Space(20f)]
        [Title("Title on the left", alignTitleLeft:true)]
        [HelpBox("HelpBox attribute is useful to describe the usage of a field directly on the inspector window.", HelpBoxMessageType.Warning)]
        public bool ToggleToEdit = false;

        /// MinMaxSlider
        [Space(20f)]
        [Title("Vector2 MinMaxSlider")]
        [MinMaxSlider(0, 100)] 
        public Vector2 MinMaxSliderAttribute;

        /// SnappedSlider
        [Space(20f)]
        [Title("Snap float value")]
        [SnappedSlider(0.25f, 1f, 10f)]
        public float SnappedFloat;

        /// Path
        [Space(20f)]
        [Title("Path")]
        public PathReference PathReference;

        ///Required
        [Space(20f)]
        [Title("Required")]
        [Required] 
        public Collider Collider;

        /// Sprite Previw
        [Space(20f)]
        [Title("Sprite Preview")]
        [SpritePreview]
        public Sprite Sprite;
        #pragma warning restore 0414

        void Start()
        {
            Debug.Log("GUID : "+ PathReference);
        }

        [Button(nameof(ButtonCallback), "Click on me !", 100f, row: "first")]
        public void ButtonCallback()
        {
            Debug.Log("You clicked on a button, congrats.");
        }

        [Button(nameof(Test), "Another button", 100f, row:"first")]
        public void Test()
        {
            Debug.Log("This method is incredibly useful.");
        }
    }
}
