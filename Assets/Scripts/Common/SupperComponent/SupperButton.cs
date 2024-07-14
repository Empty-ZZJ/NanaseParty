using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class SupperButton : Button
{
    private static AudioClip AudioEffict_Click;
    private AudioSource AudioPlayer;
    public bool Has_ClickAudio = true;
    protected override void Awake ()
    {
        if (AudioEffict_Click == null)
            AudioEffict_Click = Resources.Load<AudioClip>("Audio/Effict/click");
        if (AudioPlayer == null)
            AudioPlayer = this.gameObject.AddComponent<AudioSource>();
        base.Awake();
    }
    protected override void DoStateTransition (SelectionState state, bool instant)
    {

        base.DoStateTransition(state, instant);
        switch (state)
        {
            case SelectionState.Disabled:
                // Debug.LogError("Button失效！");
                break;

            case SelectionState.Highlighted:

                // Debug.LogError("鼠标移到button上！");

                break;

            case SelectionState.Normal:

                // Debug.LogError("鼠标离开Button！");
                break;

            case SelectionState.Pressed:
                // Debug.LogError("鼠标按下Button！");
                if (Has_ClickAudio)
                    AudioPlayer.PlayOneShot(AudioEffict_Click);
                break;

            default:

                break;
        }
        AppLogger.Log($"Click Button:{this.gameObject.name}", "operate");
    }
    [Button(nameof(CloseClickAudio), "关闭点击音效")]
    public void CloseClickAudio ()
    {

    }
    [Button(nameof(OpenClickAudio), "开启点击音效")]
    public void OpenClickAudio ()
    {

    }


}
