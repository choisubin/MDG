using UnityEngine;
using System.Collections;

public class Cell
{
    //-------------------------------------------------------
    // Member variable, Property
    //-------------------------------------------------------
    protected CellType m_CellType;
    public CellType type
    {
        get { return m_CellType; }
        set { m_CellType = value; }
    }

    protected CellBehaviour m_CellBehaviour;
    public CellBehaviour cellBehaviour
    {
        get { return m_CellBehaviour; }
        set
        {
            m_CellBehaviour = value;
            m_CellBehaviour.SetCell(this);
        }
    }

    //-------------------------------------------------------
    // Constructor
    //-------------------------------------------------------
    public Cell(CellType cellType)
    {
        m_CellType = cellType;
    }

    //-------------------------------------------------------
    // Methods
    //-------------------------------------------------------

    /// <summary>
    /// 주어진 prefab을 이용해서 Cell GameObject를 생성(Instantiate)한다.
    /// </summary>
    /// <param name="cellPrefab">Cell Prefab</param>
    /// <param name="containerObj">생성된 GameObject의 부모(Board GameObject)</param>
    /// <returns></returns>
    public Cell InstantiateCellObj(string CellName,Transform containerObj)
    {
        //1. Cell 오브젝트를 생성한다.
        //GameObject newObj = Object.Instantiate(cellPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject newObj = PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameBoard, CellName, containerObj);

        //2. 컨테이너(Board)의 차일드로 Cell을 포함시킨다.
        //newObj.transform.parent = containerObj;

        //3. Cell 오브젝트에 적용된 CellBehaviour 컴포너트를 보관한다.
        this.cellBehaviour = newObj.GetComponent<CellBehaviour>();

        return this;
    }

    /// <summary>
    /// Cell에 연결된 GameObject 위치(position)를 이동시킨다
    /// </summary>
    /// <param name="x">이동할 x 위치</param>
    /// <param name="y">이동할 y 위치</param>
    public void Move(float x, float y)
    {
        cellBehaviour.transform.position = new Vector3(x, y);
    }

    public bool IsObstracle()
    {
        return type == CellType.EMPTY;
    }
}
