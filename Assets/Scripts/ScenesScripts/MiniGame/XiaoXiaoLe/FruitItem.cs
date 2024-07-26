using DG.Tweening;
using UnityEngine;
namespace ScenesScripts.MiniGame.XiaoXiaoLe
{
    /// <summary>
    /// 水果脚本
    /// </summary>
    public class FruitItem : MonoBehaviour
    {
        private static int AudioCount = 1;
        private AudioSource audioSource;
        /// <summary>
        /// 行号
        /// </summary>
        public int rowIndex;
        /// <summary>
        /// 列号
        /// </summary>
        public int columIndex;
        /// <summary>
        /// 水果类型
        /// </summary>
        public int fruitType;

        /// <summary>
        /// 水果图片
        /// </summary>
        public GameObject fruitSpriteObj;


        private Transform m_selfTransform;

        private static AudioClip Clip_Exchange;
        private static AudioClip[] Clip_Xiao;

        private void Awake ()
        {
            if (Clip_Xiao == null)
            {
                Clip_Xiao = new AudioClip[7];
                for (int i = 1; i <= 6; i++)
                {
                    Clip_Xiao[i] = Resources.Load<AudioClip>($"Audio/MiniGame/XiaoXiaoLe/xiao{i}");
                }
            }
            m_selfTransform = transform;
            audioSource = GameObject.Find("AudioSystem/Audio Source - Back").GetComponent<AudioSource>();
        }

        /// <summary>
        /// 创建水果图片
        /// </summary>
        /// <param name="fruitType">水果类型</param>
        /// <param name="prefab">水果图片预设</param>
        public void CreateFruitBg (int fruitType, GameObject prefab)
        {
            if (null != fruitSpriteObj) return;
            this.fruitType = fruitType;
            fruitSpriteObj = Instantiate(prefab);
            fruitSpriteObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * GlobalDef.CELL_SIZE;
            fruitSpriteObj.transform.SetParent(m_selfTransform, false);
        }

        /// <summary>
        /// 设置坐标
        /// </summary>
        public void UpdatePosition (int rowIndex, int columIndex, bool dotween = false)
        {
            this.rowIndex = rowIndex;
            this.columIndex = columIndex;
            var targetPos = new Vector3((columIndex - GlobalDef.COLUM_COUNT / 2f) * GlobalDef.CELL_SIZE + GlobalDef.CELL_SIZE / 2f, (rowIndex - GlobalDef.ROW_COUNT / 2f) * GlobalDef.CELL_SIZE + GlobalDef.CELL_SIZE / 2f, 0);
            if (dotween)
            {
                // 0.3秒移动到目标点
                m_selfTransform.DOLocalMove(targetPos, 0.3f);
            }
            else
            {
                m_selfTransform.localPosition = targetPos;
            }
        }
        void Update ()
        {
            // 检查是否有触摸输入
            if (Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0);

                // 检查触摸输入是否开始（TouchPhase.Began）并且触摸点位于水果上
                if (touch.phase == TouchPhase.Began && GetComponent<Collider2D>().bounds.Contains(touch.position))
                {
                    // 抛出事件
                    PlayExchangeAudio();
                    EventDispatcher.instance.DispatchEvent(EventDef.EVENT_FRUIT_SELECTED, this);
                }
            }
        }
        /// <summary>
        /// 水果被点击
        /// </summary>
        private void OnMouseDown ()
        {
            PlayExchangeAudio();
            EventDispatcher.instance.DispatchEvent(EventDef.EVENT_FRUIT_SELECTED, this);
            // 抛出事件

        }
        private void PlayExchangeAudio ()
        {
            if (Clip_Exchange == null)
                Clip_Exchange = Resources.Load<AudioClip>("Audio/MiniGame/XiaoXiaoLe/exchange");
            audioSource.PlayOneShot(Clip_Exchange);
        }

        /// <summary>
        /// 销毁水果图片
        /// </summary>
        public void DestroyFruitBg ()
        {
            GameObject.Find("EventSystem").GetComponent<XiaoLeManager>().ChangeMenhera();
            audioSource.PlayOneShot(Clip_Xiao[AudioCount]);
            AudioCount++;
            if (AudioCount >= 7) AudioCount = 1;
            Destroy(fruitSpriteObj);
            fruitSpriteObj = null;
            // 水果消失事件
            EventDispatcher.instance.DispatchEvent(EventDef.EVENT_FRUIT_DISAPPEAR, m_selfTransform.position);
            // 加分事件，每个水果10分
            EventDispatcher.instance.DispatchEvent(EventDef.EVENT_ADD_SCORE, 10);

        }
    }
}
