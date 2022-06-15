using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
* Block GameObject의 애니메이션을 담당한다.
* - Drop    
* - Focus, Landing    
*/
public class BlockActionBehaviour : MonoBehaviour
{
    //[SerializeField] BlockSetting m_BlockConfig = new BlockSetting();
    public bool isMoving { get; set; }

    Queue<Vector3> m_MovementQueue = new Queue<Vector3>();    //x, y, z = acceleration

    private Dictionary<int, UnitWrapperDefinition> _unitDefDic = new Dictionary<int, UnitWrapperDefinition>();
    public Dictionary<int, UnitWrapperDefinition> UnitDefDic
    {
        get
        {
            if (_unitDefDic == null)
            {
                _unitDefDic = DefinitionManager.Instance.GetDatas<UnitWrapperDefinition>();
            }
            return _unitDefDic;
        }
    }
    public float[] dropSpeed = { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f };
    

    /*
     * 아래쪽으로 주어진 거리만큼 이동한다.
     * fDropDistance : 이동할 스텝 수 즉, 거리 (unit)   
     */
    public void MoveDrop(Vector2 vtDropDistance)
    {
        m_MovementQueue.Enqueue(new Vector3(vtDropDistance.x, vtDropDistance.y, 1));

        if (!isMoving)
        {
            StartCoroutine(DoActionMoveDrop());
        }
    }

    IEnumerator DoActionMoveDrop(float acc = 1.0f)
    {
        isMoving = true;

        while (m_MovementQueue.Count > 0)
        {
            Vector2 vtDestination = m_MovementQueue.Dequeue();

            int dropIndex = System.Math.Min(9, System.Math.Max(1, (int)Mathf.Abs(vtDestination.y)));
            float duration = dropSpeed[dropIndex - 1];
            yield return CoStartDropSmooth(vtDestination, duration * acc);
        }

        isMoving = false;
        yield break;
    }

    IEnumerator CoStartDropSmooth(Vector2 vtDropDistance, float duration)
    {
        Vector2 to = new Vector3(transform.position.x + vtDropDistance.x, transform.position.y - vtDropDistance.y);
        yield return Action2D.MoveTo(transform, to, duration);
    }
}
