using Common;
using ScenesScripts.Lobby;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static ScenesScripts.GalPlot.GalManager.Struct_PlotData;

namespace ScenesScripts.GalPlot
{
    public class GalManager : MonoBehaviour
    {
        [Title("当前对话")]
        public GalController_Text Gal_Text;

        [Title("当前角色部分")]
        public GalController_CharacterImg Gal_CharacterImg;

        [Title("控制选项")]
        public GalController_Choice Gal_Choice;

        [Title("控制背景图片的组件")]
        public GalController_BackImg Gal_BackImg;

        [Title("游戏开场动画")]
        public GalController_StartTitile Gal_StartTitile;

        public GameObject TouchBack;

        /// <summary>
        /// 角色发言的AudioSource
        /// </summary>
        private AudioSource Gal_Voice;

        /// <summary>
        /// 当前场景角色数量
        /// </summary>
        [Title("当前场景角色数量")]
        public int CharacterNum;
        /// <summary>
        /// 存储整个剧本的XML文档
        /// </summary>
        private XDocument PlotxDoc;
        public class Struct_PlotData
        {

            public string Title;
            public string TitleImg;

            public string Synopsis;
            public List<XElement> BranchPlot = new();
            public Queue<XElement> BranchPlotInfo = new();
            public Queue<XElement> MainPlot = new();
            public class Struct_Choice
            {
                public Struct_Choice (string Title, string JumpID)
                {
                    this.Title = Title;
                    this.JumpID = JumpID;
                }
                public string Title;
                public string JumpID;
            }
            public class Struct_CharacterInfo
            {
                public string CharacterID;
                public string FromID;
                public GameObject CharacterGameObject;
                public string Name;
                public string Affiliation;
            }
            public List<Struct_CharacterInfo> CharacterInfo = new();
            public List<Struct_Choice> ChoiceText = new();
            /// <summary>
            /// 当前的剧情节点
            /// </summary>
            public XElement NowPlotDataNode;

            /// <summary>
            /// 当前是否为分支剧情节点
            /// </summary>
            public bool IsBranch = false;
            public string NowJumpID;

        }
        public static Struct_PlotData PlotData = new();
        private void Start ()
        {
            Gal_Voice = this.gameObject.GetComponent<AudioSource>();
            ResetPlotData();
            StartCoroutine(LoadPlot());
            return;
        }
        /// <summary>
        /// 重置
        /// </summary>
        private void ResetPlotData ()
        {
            PlotData = new Struct_PlotData();
            return;
        }
        /// <summary>
        /// 解析框架文本
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadPlot ()
        {
            yield return null;
            try
            {
                var _PlotText = Resources.Load<TextAsset>($"TextAsset/Plots/{PlotIStartterManager.ID}").text;

                Debug.Log($"游戏剧本：{_PlotText}");
                PlotxDoc = XDocument.Parse(_PlotText);

                //-----开始读取数据

                foreach (var item in PlotxDoc.Root.Elements())
                {
                    switch (item.Name.ToString())
                    {
                        case "title":
                        {
                            foreach (var item1 in item.Elements())
                            {
                                switch (item1.Name.ToString())
                                {
                                    case "text":
                                    {
                                        PlotData.Title = item1.Value;
                                        break;
                                    }
                                    case "img":
                                    {
                                        PlotData.TitleImg = item1.Value;
                                        break;
                                    }
                                    default:
                                    {
                                        PopupManager.PopMessage("错误", "剧情文本错误 \n 位置：title 标签");
                                        break;
                                    }
                                }

                            }
                            Gal_StartTitile.ShowTitle(PlotData.Title, Resources.Load<Sprite>($"Texture2D/Menhera/Plot/back/{PlotData.TitleImg}"));
                            break;
                        }
                        case "Synopsis":
                        {
                            PlotData.Synopsis = item.Value;
                            break;
                        }
                        case "BranchPlot":
                        {
                            foreach (var BranchItem in item.Elements())
                            {
                                PlotData.BranchPlot.Add(BranchItem);
                            }
                            break;
                        }
                        case "MainPlot":
                        {
                            foreach (var MainPlotItem in item.Elements())
                            {
                                PlotData.MainPlot.Enqueue(MainPlotItem);
                            }
                            break;
                        }
                        default:
                        {
                            throw new Exception("无法识别的根标签");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "无法识别的根标签")
                {
                    PopupManager.PopMessage("游戏加载失败！", ex.Message);

                }
            }


        }
        /// <summary>
        /// 点击屏幕 下一句
        /// </summary>
        public void Button_Click_NextPlot ()
        {

            if (PlotData.MainPlot.Count == 0)
            {
                print("游戏结束!");
                return;
            }

            //IsCanJump这里有问题，如果一直点击会为false，而不是说true，这是因为没有点击按钮 ，没有添加按钮
            if (GalController_Text.IsSpeak || !GalController_Text.IsCanJump) { return; }
            if (!PlotData.IsBranch)
            {
                PlotData.MainPlot.TryDequeue(out PlotData.NowPlotDataNode);//队列出队+内联 出一个temp节点
                PlotData.BranchPlotInfo.Clear();
            }
            else//当前为分支节点
            {
                //这块得妥善处理
                PlotData.NowPlotDataNode = GetBranchByID(PlotData.NowJumpID);
            }

            PlotData.ChoiceText.Clear();
            if (PlotData.NowPlotDataNode == null)
            {

                PopupManager.PopMessage("致命错误", "无效的剧情结点");
                return;
            }
            switch (PlotData.NowPlotDataNode.Name.ToString())
            {
                case "AddCharacter"://处理添加角色信息的东西
                {
                    var _ = new Struct_CharacterInfo();
                    var _From = PlotData.NowPlotDataNode.Attribute("From").Value;
                    var _CharacterId = PlotData.NowPlotDataNode.Attribute("CharacterID").Value;

                    _.Name = GameManager.ServerManager.Config.CharacterInfo.GetValue(_From, "Name");
                    _.CharacterID = _CharacterId;
                    _.Affiliation = GameManager.ServerManager.Config.Department.GetValue(GameManager.ServerManager.Config.CharacterInfo.GetValue(_From, "Department"), "Name");
                    _.FromID = _From;
                    var _CameObj = Resources.Load<GameObject>("Common/Gameobject/Galgame/Img-Character");
                    _CameObj.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Texture2D/Menhera/Plot/character/{GameManager.ServerManager.Config.CharacterInfo.GetValue(_From, "ResourcePath")}/{GameManager.ServerManager.Config.CharacterInfo.GetValue(_From, "Portrait-Normall")}");
                    _.CharacterGameObject = Instantiate(_CameObj, Gal_CharacterImg.gameObject.transform);

                    if (PlotData.NowPlotDataNode.Attributes("SendMessage").Count() != 0)
                    {

                        _.CharacterGameObject.GetComponent<GalController_CharacterAnimate>().Animate_StartOrOutside = PlotData.NowPlotDataNode.Attribute("SendMessage").Value;
                    }

                    PlotData.CharacterInfo.Add(_);

                    Button_Click_NextPlot();
                    break;
                }
                case "Speak":  //处理发言
                {
                    var _nodeinfo = GetCharacterObjectByName(PlotData.NowPlotDataNode.Attribute("CharacterID").Value);
                    if (PlotData.NowPlotDataNode.Elements().Count() != 0) //有选项，因为他有子节点数目了
                    {
                        GalController_Text.IsCanJump = false;
                        foreach (var ClildItem in PlotData.NowPlotDataNode.Elements())
                        {
                            if (ClildItem.Name.ToString() == "Choice")
                                PlotData.ChoiceText.Add(new Struct_Choice(ClildItem.Value, ClildItem.Attribute("JumpID").Value));

                        }
                        Gal_Text.StartTextContent(PlotData.NowPlotDataNode.Attribute("Content").Value, _nodeinfo.Name, _nodeinfo.Affiliation, () =>
                        {

                            foreach (var ClildItem in GalManager.PlotData.ChoiceText)
                            {
                                Gal_Choice.CreatNewChoice(ClildItem.JumpID, ClildItem.Title);
                            }
                        });
                    }
                    else Gal_Text.StartTextContent(PlotData.NowPlotDataNode.Attribute("Content").Value, _nodeinfo.Name, _nodeinfo.Affiliation);

                    //处理消息
                    if (PlotData.NowPlotDataNode.Attributes("SendMessage").Count() != 0)
                        SendCharMessage(_nodeinfo.CharacterID, PlotData.NowPlotDataNode.Attribute("SendMessage").Value);
                    if (PlotData.NowPlotDataNode.Attributes("AudioPath").Count() != 0)
                        StartCoroutine(PlayAudio(Gal_Voice, PlotData.NowPlotDataNode.Attribute("AudioPath").Value));
                    break;
                }
                case "ChangeBackImg"://更换背景图片
                {
                    var _Path = PlotData.NowPlotDataNode.Attribute("Path").Value;
                    Gal_BackImg.SetImage(Resources.Load<Sprite>($"Texture2D/Menhera/Plot/back/{_Path}"));
                    Button_Click_NextPlot();
                    break;
                }
                case "ChangeCharacterImg"://更换背景图片
                {
                    var _CharacterID = PlotData.NowPlotDataNode.Attribute("CharacterID").Value;
                    var _obj = GetCharacterObjectByName(_CharacterID);

                    _obj.CharacterGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Texture2D/Menhera/Plot/character/{GameManager.ServerManager.Config.CharacterInfo.GetValue(_obj.FromID, "ResourcePath")}/{GameManager.ServerManager.Config.CharacterInfo.GetValue(_obj.FromID, PlotData.NowPlotDataNode.Attribute("Path").Value)}");
                    Button_Click_NextPlot();
                    break;
                }
                case "DeleteCharacter":
                {
                    DestroyCharacterByID(PlotData.NowPlotDataNode.Attribute("CharacterID").Value);
                    break;
                }
                case "ExitGame":
                {
                    foreach (var item in PlotData.CharacterInfo)
                    {
                        DestroyCharacterByID(item.CharacterID);
                    }
                    PlotData.MainPlot.Clear();
                    PlotData.BranchPlot.Clear();
                    PlotData.IsBranch = false;
                    break;
                }
                case "SetDialogHide":
                {
                    Gal_Text.SetDialogHide(false);
                    break;
                }
                case "SetDialogShow":
                {
                    Gal_Text.SetDialogHide(true);
                    break;
                }
            }
            if (PlotData.BranchPlotInfo.Count == 0)
            {
                PlotData.IsBranch = false;
            }
            return;
        }
        public void Button_Click_FastMode ()
        {
            GalController_Text.IsFastMode = true;
            return;
        }
        public Struct_CharacterInfo GetCharacterObjectByName (string ID)
        {
            return PlotData.CharacterInfo.Find(t => t.CharacterID == ID);
        }
        public XElement GetBranchByID (string ID)
        {
            if (PlotData.BranchPlotInfo.Count == 0)
                foreach (var item in PlotData.BranchPlot.Find(t => t.Attribute("ID").Value == ID).Elements())
                {
                    PlotData.BranchPlotInfo.Enqueue(item);

                }
            PlotData.BranchPlotInfo.TryDequeue(out XElement t);
            return t;
        }
        /// <summary>
        /// 销毁一个角色
        /// </summary>
        /// <param name="ID"></param>
        public void DestroyCharacterByID (string ID)
        {
            var _ = PlotData.CharacterInfo.Find(t => t.CharacterID == ID);
            SendCharMessage(ID, "Quit");
            PlotData.CharacterInfo.Remove(_);
        }
        public void SendCharMessage (string CharacterID, string Message)
        {
            var _t = GetCharacterObjectByName(CharacterID);
            _t.CharacterGameObject.GetComponent<GalController_CharacterMessage>().HandleMessage(Message);
        }
        private IEnumerator PlayAudio (AudioSource audioSource, string fileName)
        {
            //获取.wav文件，并转成AudioClip
            print($"{GameAPI.GetWritePath()}/{fileName}");
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip($"{GameAPI.GetWritePath()}/Static/Audio/Plot/{fileName}", AudioType.MPEG);
            //等待转换完成
            yield return www.SendWebRequest();
            //获取AudioClip
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
            //设置当前AudioSource组件的AudioClip
            audioSource.clip = audioClip;
            //播放声音
            audioSource.Play();
        }
        private void FixedUpdate ()
        {
            CharacterNum = PlotData.CharacterInfo.Count;
        }
        public void CloseTitleAnimate_Recall ()
        {
            Button_Click_NextPlot();
            TouchBack.SetActive(true);
        }
    }
}