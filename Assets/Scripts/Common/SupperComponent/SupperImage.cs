using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;
namespace Common.SupperComponent
{
    /// <summary>
    /// 圆形Image组件
    /// </summary>
    [AddComponentMenu("LFramework/UI/SupperImage", 11)]
    public class SupperImage : MaskableGraphic, ICanvasRaycastFilter
    {
        protected SupperImage ()
        {

        }

        /// <summary>
        /// 渲染类型
        /// </summary>
        public enum RenderType
        {
            Simple,
            Filled,
        }

        /// <summary>
        /// 填充类型
        /// </summary>
        public enum FilledType
        {
            Radial360,
        }

        /// <summary>
        /// 绘制起始点(填充类型-360度)
        /// </summary>
        public enum Origin360
        {
            Right,
            Top,
            Left,
            Bottom,
        }

        //Sprite图片
        [SerializeField]
        Sprite m_Sprite;
        public Sprite Sprite
        {
            get { return m_Sprite; }
            set { m_Sprite = value; }
        }

        //贴图
        public override Texture mainTexture
        {
            get
            {
                if (m_Sprite == null)
                {
                    if (material != null && material.mainTexture != null)
                    {
                        return material.mainTexture;
                    }
                    return s_WhiteTexture;
                }

                return m_Sprite.texture;
            }
        }

        //渲染类型
        [SerializeField]
        RenderType m_RenderType;

        //填充类型
        [SerializeField]
        FilledType m_FilledType;

        //绘制起始点(填充类型-360度)
        [SerializeField]
        Origin360 m_Origin360;

        //是否为顺时针绘制
        [SerializeField]
        bool m_Clockwise;

        //填充度
        [SerializeField]
        [Range(0, 1)]
        float m_FillAmount = 1;

        //边数(边数越多圆越平滑)
        [SerializeField]
        int m_SideCount = 100;
        public int SideCount
        {
            get
            {
                m_SideCount = Mathf.Clamp(m_SideCount, 3, 65000);
                return m_SideCount;
            }
        }

        //顶点位置列表
        readonly List<Vector3> m_VertexList = new List<Vector3>();

        protected override void OnPopulateMesh (VertexHelper vh)
        {
            vh.Clear();
            m_VertexList.Clear();

            switch (m_RenderType)
            {
                case RenderType.Simple:
                    GenerateSimpleSprite(vh);
                    break;
                case RenderType.Filled:
                    GenerateFilledSprite(vh);
                    break;
            }
        }

        /// <summary>
        /// 生成普通类型的圆
        /// </summary>
        void GenerateSimpleSprite (VertexHelper vh)
        {
            Vector4 uv = m_Sprite == null
                ? Vector4.zero
                : DataUtility.GetOuterUV(m_Sprite);
            float uvWidth = uv.z - uv.x;
            float uvHeight = uv.w - uv.y;
            float width = rectTransform.rect.width;
            float height = rectTransform.rect.height;
            float dia = width > height ? width : height;
            float r = dia * 0.5f;
            Vector2 uvCenter = new Vector2((uv.x + uv.z) * 0.5f, (uv.y + uv.w) * 0.5f);
            Vector3 posCenter = new Vector2((0.5f - rectTransform.pivot.x) * width, (0.5f - rectTransform.pivot.y) * height);
            float uvScaleX = uvWidth / width;
            float uvScaleY = uvHeight / height;
            float deltaRad = 2 * Mathf.PI / SideCount;

            float curRad = 0;
            int vertexCount = SideCount + 1;
            int triangleCount = SideCount;

            UIVertex vertex = new UIVertex();
            vertex.position = posCenter;
            vertex.color = color;
            vertex.uv0 = uvCenter;
            vh.AddVert(vertex);
            for (int i = 0; i < vertexCount - 1; i++)
            {
                Vector3 posOffset = new Vector3(r * Mathf.Cos(curRad), r * Mathf.Sin(curRad));
                vertex.position = posCenter + posOffset;
                vertex.color = color;
                vertex.uv0 = new Vector2(uvCenter.x + posOffset.x * uvScaleX, uvCenter.y + posOffset.y * uvScaleY);
                vh.AddVert(vertex);
                m_VertexList.Add(vertex.position);

                curRad += deltaRad;
            }

            for (int i = 0; i < triangleCount; i++)
            {
                vh.AddTriangle(0, i + 1, i + 2 >= vertexCount ? 1 : i + 2);
            }
        }

        /// <summary>
        /// 生成填充类型的圆
        /// </summary>
        void GenerateFilledSprite (VertexHelper vh)
        {
            Vector4 uv = m_Sprite == null
                ? Vector4.zero
                : DataUtility.GetOuterUV(m_Sprite);
            float uvWidth = uv.z - uv.x;
            float uvHeight = uv.w - uv.y;
            float width = rectTransform.rect.width;
            float height = rectTransform.rect.height;
            float dia = width > height ? width : height;
            float r = dia * 0.5f;
            Vector2 uvCenter = new Vector2((uv.x + uv.z) * 0.5f, (uv.y + uv.w) * 0.5f);
            Vector3 posCenter = new Vector2((0.5f - rectTransform.pivot.x) * width, (0.5f - rectTransform.pivot.y) * height);
            float uvScaleX = uvWidth / width;
            float uvScaleY = uvHeight / height;
            float deltaRad = 2 * Mathf.PI / SideCount;

            switch (m_FilledType)
            {
                case FilledType.Radial360:
                    float quarterRad = 2 * Mathf.PI * 0.25f;
                    float curRad = quarterRad * (int)m_Origin360;
                    int vertexCount = m_FillAmount == 1
                        ? SideCount + 1
                        : Mathf.RoundToInt(SideCount * m_FillAmount) + 2;
                    int triangleCount = Mathf.RoundToInt(SideCount * m_FillAmount);

                    UIVertex vertex = new UIVertex();
                    vertex.position = posCenter;
                    vertex.color = color;
                    vertex.uv0 = uvCenter;
                    vh.AddVert(vertex);
                    for (int i = 0; i < vertexCount - 1; i++)
                    {
                        Vector3 posOffset = new Vector3(r * Mathf.Cos(curRad), r * Mathf.Sin(curRad));
                        vertex.position = posCenter + posOffset;
                        vertex.color = color;
                        vertex.uv0 = new Vector2(uvCenter.x + posOffset.x * uvScaleX, uvCenter.y + posOffset.y * uvScaleY);
                        vh.AddVert(vertex);
                        m_VertexList.Add(vertex.position);

                        curRad += m_Clockwise ? -deltaRad : deltaRad;
                    }

                    if (m_FillAmount == 1)
                    {
                        for (int i = 0; i < triangleCount; i++)
                        {
                            vh.AddTriangle(0, i + 1, i + 2 >= vertexCount ? 1 : i + 2);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < triangleCount; i++)
                        {
                            vh.AddTriangle(0, i + 1, i + 2);
                        }
                    }
                    break;
            }
        }

        public bool IsRaycastLocationValid (Vector2 sp, Camera eventCamera)
        {
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, sp, eventCamera, out localPos);
            int crossPointCount = GetCrossPointCount(localPos);
            return crossPointCount % 2 != 0;
        }

        /// <summary>
        /// 得到交点个数
        /// </summary>
        int GetCrossPointCount (Vector2 localPos)
        {
            int crossPointCount = 0;
            for (int i = 0; i < m_VertexList.Count; i++)
            {
                Vector3 p1 = m_VertexList[i];
                Vector3 p2 = m_VertexList[(i + 1) % m_VertexList.Count];

                if (localPos.y < Mathf.Min(p1.y, p2.y)
                    || localPos.y > Mathf.Max(p1.y, p2.y))
                {
                    continue;
                }
                //平行于x轴
                if (p1.y == p2.y)
                {
                    if (localPos.x <= p1.x
                        || localPos.x <= p2.x)
                    {
                        crossPointCount++;
                    }
                    continue;
                }
                //平行于y轴
                else if (p1.x == p2.x)
                {
                    if (localPos.x <= p1.x)
                    {
                        crossPointCount++;
                    }
                    continue;
                }

                if (GetCrossPointX(p1, p2, localPos) >= localPos.x)
                {
                    crossPointCount++;
                }
            }
            return crossPointCount;
        }

        /// <summary>
        /// 得到交点的x坐标
        /// </summary>
        float GetCrossPointX (Vector2 p1, Vector2 p2, Vector2 p)
        {
            float k = (p2.y - p1.y) / (p2.x - p1.x);
            float b = p1.y - k * p1.x;
            float x = (p.y - b) / k;
            return x;
        }
    }

}