using UnityEngine;

/// <summary>
/// 冰块生成器
/// </summary>
public class IceSpawner : MonoBehaviour
{
    public GameObject iceObj;

    void Start()
    {
        iceObj.transform.localScale = Vector3.one * GlobalDef.CELL_SIZE;
        // 生成冰块整列
        for (int rowIndex = 0; rowIndex < GlobalDef.ROW_COUNT; ++rowIndex)
        {
            for (int columIndex = 0; columIndex < GlobalDef.COLUM_COUNT; ++columIndex)
            {
                // 实例化冰块物体
                var obj = Instantiate(iceObj);
                obj.transform.SetParent(iceObj.transform.parent, false);
                obj.transform.localPosition = new Vector3((columIndex - GlobalDef.COLUM_COUNT / 2f) * GlobalDef.CELL_SIZE + GlobalDef.CELL_SIZE / 2f, (rowIndex - GlobalDef.ROW_COUNT / 2f) * GlobalDef.CELL_SIZE + GlobalDef.CELL_SIZE / 2f, 0);
            }
        }
        iceObj.SetActive(false);
    }
}
