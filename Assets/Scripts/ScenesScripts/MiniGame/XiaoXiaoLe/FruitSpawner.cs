using System.Collections;
using UnityEngine;
namespace ScenesScripts.MiniGame.XiaoXiaoLe
{
    public class FruitSpawner : MonoBehaviour
    {
        /// <summary>
        /// 水果预设
        /// </summary>
        public GameObject[] fruitPrefabs;

        /// <summary>
        /// 生成出来的水果二维数组
        /// </summary>
        public ArrayList fruitList;


        /// <summary>
        /// 水果的根节点
        /// </summary>
        private Transform m_fruitRoot;

        /// <summary>
        /// 被选中的水果
        /// </summary>
        private FruitItem m_curSelectFruit;

        /// <summary>
        /// 手指水平滑动量
        /// </summary>
        private float m_fingerMoveX;
        /// <summary>
        /// 手指竖直滑动量
        /// </summary>
        private float m_fingerMoveY;

        /// <summary>
        /// 需要消除掉的水果数组
        /// </summary>
        private ArrayList m_matchFruits;

        private void Awake ()
        {
            m_fruitRoot = transform;
            EventDispatcher.instance.Regist(EventDef.EVENT_FRUIT_SELECTED, OnFruitSelected);
        }

        private void OnDestroy ()
        {
            EventDispatcher.instance.UnRegist(EventDef.EVENT_FRUIT_SELECTED, OnFruitSelected);
        }

        /// <summary>
        /// 水果被点击
        /// </summary>
        private void OnFruitSelected (params object[] args)
        {
            // 把被点击的水果对象缓存起来
            m_curSelectFruit = args[0] as FruitItem;
        }

        private void Start ()
        {
            SpawnFruitArrayList();

            // 首次生成水果后，执行一次自动检测
            StartCoroutine(AutoMatchAgain());
        }

        private void Update ()
        {
            if (null == m_curSelectFruit) return;

            // 检查鼠标左键是否抬起
            if (Input.GetMouseButtonUp(0))
            {
                // 鼠标左键抬起，释放当前选中的水果对象
                m_curSelectFruit = null;
                return;
            }

            // 检查是否有触摸输入
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // 检查触摸输入是否为抬起（TouchPhase.Ended）
                if (touch.phase == TouchPhase.Ended)
                {
                    // 手指抬起，释放当前选中的水果对象
                    m_curSelectFruit = null;
                    return;
                }

                // 检查触摸输入是否为移动（TouchPhase.Moved）
                if (touch.phase == TouchPhase.Moved)
                {
                    m_fingerMoveX = touch.deltaPosition.x;
                    m_fingerMoveY = touch.deltaPosition.y;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                // 检查鼠标左键是否按下并且移动
                m_fingerMoveX = Input.GetAxis("Mouse X");
                m_fingerMoveY = Input.GetAxis("Mouse Y");
            }

            // 滑动量太小，不处理
            if (Mathf.Abs(m_fingerMoveX) < 0.3f && Mathf.Abs(m_fingerMoveY) < 0.3f)
                return;

            OnFruitMove();

            m_fingerMoveX = 0;
            m_fingerMoveY = 0;
        }
        /// <summary>
        /// 水果滑动响应
        /// </summary>
        private void OnFruitMove ()
        {
            if (Mathf.Abs(m_fingerMoveX) > Mathf.Abs(m_fingerMoveY))
            {
                //横向滑动
                var targetItem = GetFruitItem(m_curSelectFruit.rowIndex, m_curSelectFruit.columIndex + (m_fingerMoveX > 0 ? 1 : -1));
                if (null != targetItem)
                {
                    StartCoroutine(ExchangeAndMatch(m_curSelectFruit, targetItem));
                }
                else
                {
                    m_curSelectFruit = null;
                }
            }
            else if (Mathf.Abs(m_fingerMoveX) < Mathf.Abs(m_fingerMoveY))
            {
                //纵向滑动
                var targetItem = GetFruitItem(m_curSelectFruit.rowIndex + (m_fingerMoveY > 0 ? 1 : -1), m_curSelectFruit.columIndex);
                if (null != targetItem)
                {
                    StartCoroutine(ExchangeAndMatch(m_curSelectFruit, targetItem));
                }
                else
                {
                    m_curSelectFruit = null;
                }
            }
        }

        /// <summary>
        /// 根据行号列号获取水果对象
        /// </summary>
        private FruitItem GetFruitItem (int rowIndex, int columIndex)
        {
            if (rowIndex < 0 || rowIndex >= fruitList.Count) return null;
            var temp = fruitList[rowIndex] as ArrayList;
            if (columIndex < 0 || columIndex >= temp.Count) return null;
            return temp[columIndex] as FruitItem;
        }

        /// <summary>
        /// 根据行号列号设置水果对象
        /// </summary>
        private void SetFruitItem (int rowIndex, int columIndex, FruitItem item)
        {
            var temp = fruitList[rowIndex] as ArrayList;
            temp[columIndex] = item;
        }


        /// <summary>
        /// 交换水果并检测是否可以消除
        /// </summary>
        IEnumerator ExchangeAndMatch (FruitItem item1, FruitItem item2)
        {
            Exchange(item1, item2);
            yield return new WaitForSeconds(0.3f);
            if (CheckHorizontalMatch() || CheckVerticalMatch())
            {
                // 消除匹配的水果
                RemoveMatchFruit();
                yield return new WaitForSeconds(0.2f);

                //上面的水果掉落下来，
                DropDownOtherFruit();

                m_matchFruits = new ArrayList();

                yield return new WaitForSeconds(0.6f);
                // 再次执行一次检测
                StartCoroutine(AutoMatchAgain());
            }
            else
            {
                // 没有任何水果匹配，交换回来
                Exchange(item1, item2);
            }
        }

        /// <summary>
        /// 自动递归检测水果消除
        /// </summary>
        /// <returns></returns>
        IEnumerator AutoMatchAgain ()
        {
            if (CheckHorizontalMatch() || CheckVerticalMatch())
            {
                RemoveMatchFruit();
                yield return new WaitForSeconds(0.2f);
                DropDownOtherFruit();

                m_matchFruits = new ArrayList();

                yield return new WaitForSeconds(0.6f);
                StartCoroutine(AutoMatchAgain());
            }
        }

        /// <summary>
        /// 交换水果
        /// </summary>
        private void Exchange (FruitItem item1, FruitItem item2)
        {

            SetFruitItem(item1.rowIndex, item1.columIndex, item2);
            SetFruitItem(item2.rowIndex, item2.columIndex, item1);

            int tmp = 0;
            tmp = item1.rowIndex;
            item1.rowIndex = item2.rowIndex;
            item2.rowIndex = tmp;

            tmp = item1.columIndex;
            item1.columIndex = item2.columIndex;
            item2.columIndex = tmp;

            item1.UpdatePosition(item1.rowIndex, item1.columIndex, true);
            item2.UpdatePosition(item2.rowIndex, item2.columIndex, true);

            m_curSelectFruit = null;
        }


        /// <summary>
        /// 横线检测水果
        /// </summary>
        private bool CheckHorizontalMatch ()
        {
            bool isMatch = false;
            for (int rowIndex = 0; rowIndex < GlobalDef.ROW_COUNT; ++rowIndex)
            {
                for (int columIndex = 0; columIndex < GlobalDef.COLUM_COUNT - 2; ++columIndex)
                {
                    var item1 = GetFruitItem(rowIndex, columIndex);
                    var item2 = GetFruitItem(rowIndex, columIndex + 1);
                    var item3 = GetFruitItem(rowIndex, columIndex + 2);
                    if (item1.fruitType == item2.fruitType && item2.fruitType == item3.fruitType)
                    {
                        isMatch = true;
                        AddMatchFruit(item1);
                        AddMatchFruit(item2);
                        AddMatchFruit(item3);
                    }
                }
            }
            return isMatch;
        }

        /// <summary>
        /// 纵向检测水果
        /// </summary>
        /// <returns></returns>
        private bool CheckVerticalMatch ()
        {
            bool isMatch = false;
            for (int columIndex = 0; columIndex < GlobalDef.COLUM_COUNT; ++columIndex)
            {
                for (int rowIndex = 0; rowIndex < GlobalDef.ROW_COUNT - 2; ++rowIndex)
                {
                    var item1 = GetFruitItem(rowIndex, columIndex);
                    var item2 = GetFruitItem(rowIndex + 1, columIndex);
                    var item3 = GetFruitItem(rowIndex + 2, columIndex);
                    if (item1.fruitType == item2.fruitType && item2.fruitType == item3.fruitType)
                    {
                        isMatch = true;
                        AddMatchFruit(item1);
                        AddMatchFruit(item2);
                        AddMatchFruit(item3);
                    }
                }
            }
            return isMatch;
        }

        /// <summary>
        /// 添加匹配的水果到缓存中（匹配的水果会被消除掉)
        /// </summary>
        private void AddMatchFruit (FruitItem item)
        {
            if (null == m_matchFruits)
                m_matchFruits = new ArrayList();
            int index = m_matchFruits.IndexOf(item);
            if (-1 == index)
                m_matchFruits.Add(item);
        }

        /// <summary>
        /// 消除水果
        /// </summary>
        private void RemoveMatchFruit ()
        {
            for (int i = 0; i < m_matchFruits.Count; ++i)
            {
                var item = m_matchFruits[i] as FruitItem;
                item.DestroyFruitBg();

            }
        }

        /// <summary>
        /// 消除掉的水果上方的水果下落
        /// </summary>
        private void DropDownOtherFruit ()
        {
            for (int i = 0; i < m_matchFruits.Count; ++i)
            {
                var fruit = m_matchFruits[i] as FruitItem;
                for (int j = fruit.rowIndex + 1; j < GlobalDef.ROW_COUNT; ++j)
                {
                    var dropdownFruit = GetFruitItem(j, fruit.columIndex);
                    dropdownFruit.rowIndex--;
                    SetFruitItem(dropdownFruit.rowIndex, dropdownFruit.columIndex, dropdownFruit);
                    dropdownFruit.UpdatePosition(dropdownFruit.rowIndex, dropdownFruit.columIndex, true);
                }
                ReuseRemovedFruit(fruit);
            }
        }

        /// <summary>
        /// 复用被消除的水果，作为新水果放到顶部
        /// </summary>
        private void ReuseRemovedFruit (FruitItem fruit)
        {
            // 随机一个水果类型
            var fruitType = Random.Range(0, fruitPrefabs.Length);
            fruit.rowIndex = GlobalDef.ROW_COUNT;
            fruit.CreateFruitBg(fruitType, fruitPrefabs[fruitType]);
            fruit.CreateFruitBg(fruitType, fruitPrefabs[fruitType]);
            // 先放到最顶部外面
            fruit.UpdatePosition(fruit.rowIndex, fruit.columIndex);
            // 让其下落一格
            fruit.rowIndex--;
            SetFruitItem(fruit.rowIndex, fruit.columIndex, fruit);
            fruit.UpdatePosition(fruit.rowIndex, fruit.columIndex, true);
        }

        /// <summary>
        /// 生成多行多列水果
        /// </summary>
        private void SpawnFruitArrayList ()
        {
            fruitList = new ArrayList();
            for (int rowIndex = 0; rowIndex < GlobalDef.ROW_COUNT; ++rowIndex)
            {
                ArrayList temp = new ArrayList();
                for (int columIndex = 0; columIndex < GlobalDef.COLUM_COUNT; ++columIndex)
                {
                    var item = AddRandomFruitItem(rowIndex, columIndex);
                    temp.Add(item);
                }
                // 存到数组中
                fruitList.Add(temp);
            }
        }

        /// <summary>
        /// 随机一个水果
        /// </summary>
        private FruitItem AddRandomFruitItem (int rowIndex, int columIndex)
        {
            // 随机一个水果类型
            var fruitType = Random.Range(0, fruitPrefabs.Length);
            var item = new GameObject("item");
            item.transform.SetParent(m_fruitRoot, false);
            item.AddComponent<BoxCollider2D>().size = Vector2.one * GlobalDef.CELL_SIZE;
            var bhv = item.AddComponent<FruitItem>();
            bhv.UpdatePosition(rowIndex, columIndex);
            bhv.CreateFruitBg(fruitType, fruitPrefabs[fruitType]);
            return bhv;
        }
    }
}