using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ScenesScripts.GalPlot
{
    public class GalController_Text : MonoBehaviour
    {
        public const float DefalutSpeed = 0.045f;
        public const float FastSpeend = 0.02f;
        /// <summary>
        /// 当前是否剧情加速
        /// </summary>
        public static bool IsFastMode;

        /// <summary>
        /// 当前是否正在发言
        /// 如果为假则可以开始下一句
        /// 当这个文本快结束的时候也为True
        /// </summary>
        public static bool IsSpeak;

        /// <summary>
        /// 文本内容打字机动画事件
        /// </summary>
        public static Tweener TextAnimateEvemt;

        /// <summary>
        /// 文本内容
        /// </summary>
        public Text Text_TextContent;

        /// <summary>
        /// 发言人
        /// </summary>
        public Text Text_CharacterName;

        /// <summary>
        ///是否可以跳过 
        /// </summary>
        public static bool IsCanJump = true;

        /// <summary>
        /// 对话框右下角的下一句提示
        /// </summary>
        public GameObject Button_Next;

        /// <summary>
        /// 对话框是否可见
        /// </summary>
        public void SetDialogHide (bool value = false)
        {
            this.gameObject.SetActive(value);

        }
        /// <summary>
        /// 设置对话内容
        /// </summary>
        /// <param name="TextContent"></param>
        public void SetText_Content (string TextContent)
        {
            Text_TextContent.text = TextContent;
        }
        /// <summary>
        /// 设置发言人的名称
        /// </summary>
        public void SetText_CharacterName (string CharacterName, string CharacterIdentity)
        {

            Text_CharacterName.text = $"<b>{CharacterName}</b><size=45>     <color=#F684EE>{CharacterIdentity}</color></size>";
        }
        /// <summary>
        /// 开始发言
        /// </summary>
        /// <param name="TextContent">文本内容</param>
        /// <param name="CharacterName">发言人名称</param>
        /// <param name="CharacterIdentity">发言人所属</param>
        /// <param name="CallBack">回调事件</param>
        /// <returns></returns>
        public Tweener StartTextContent (string TextContent, string CharacterName, string CharacterIdentity, UnityAction CallBack = null)
        {
            //100  60   40
            void Alwayls ()
            {

                SetText_CharacterName(CharacterName, CharacterIdentity);

            }
            if (IsSpeak && Text_TextContent.text.Length >= TextContent.Length * 0.75f && IsCanJump)//当前还正在发言
            {
                //但是 ，如果当前到了总文本的三分之二，也可以下一句
                SetText_Content(TextContent);
                IsSpeak = false;
                TextAnimateEvemt.Kill();
                Button_Next.SetActive(true);
                Alwayls();
                return TextAnimateEvemt;
            }
            else if (IsSpeak) return TextAnimateEvemt;
            IsSpeak = true;
            SetText_Content(string.Empty);//先清空内容
            Button_Next.SetActive(false);
            Alwayls();
            TextAnimateEvemt = Text_TextContent.DOText(TextContent, TextContent.Length * (IsFastMode ? FastSpeend : DefalutSpeed)).SetEase(Ease.Linear).OnComplete(() =>
            {

                IsSpeak = false;
                CallBack?.Invoke();
                Button_Next.SetActive(true);
            });
            return TextAnimateEvemt;

        }

    }
}
