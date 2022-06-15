using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockBehaviour : MonoBehaviour
{
    Block m_Block;
    SpriteRenderer m_SpriteRenderer;
    //[SerializeField] BlockSetting m_BlockConfig = new BlockSetting();
    #region blocksetting

    private Dictionary<int, UnitWrapperDefinition> _unitDefDic = new Dictionary<int, UnitWrapperDefinition>();
    public Dictionary<int, UnitWrapperDefinition> UnitDefDic
    {
        get
        {
            return _unitDefDic;
        }
    }
    public float[] dropSpeed = { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f };
    private Sprite[] _basicBlockSprites;
    public Sprite[] basicBlockSprites
    {
        get
        {
            return _basicBlockSprites;
        }
    }
    private Color[] _blockColors;
    public Color[] blockColors
    {
        get
        {
            return _blockColors;
        }
    }

    public GameObject GetExplosionObject(BlockQuestType questType)
    {
        switch (questType)
        {
            case BlockQuestType.CLEAR_SIMPLE:
                return PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameMatchEffect, "FX_BLOCK_EXPLOSION_NORMAL");
            default:
                return PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameMatchEffect, "FX_BLOCK_EXPLOSION_NORMAL"); ;
        }
    }

    public Color GetBlockColor(int unitKey)
    {
        return blockColors[(int)unitKey];
    }
    #endregion
    //void Start()
    //{
    //    m_SpriteRenderer = GetComponent<SpriteRenderer>();

    //    UpdateView(false);
    //}
    public void Set()
    {
        _unitDefDic = DefinitionManager.Instance.GetDatas<UnitWrapperDefinition>();
        _basicBlockSprites = new Sprite[_unitDefDic.Count + 1];
        _blockColors = new Color[_unitDefDic.Count + 1];
        foreach (var key in _unitDefDic.Keys)
        {
            basicBlockSprites[key] = Resources.Load<Sprite>("Sprites/Unit/" + _unitDefDic[key].UnitImageStr);
            blockColors[key] = new Color(1f, 1f, 1f);
        }
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        UpdateView(false);
    }

    internal void SetBlock(Block block)
    {
        m_Block = block;
    }

    /// <summary>
    /// 참조하고 있는 Block의 보를 반영하여 Block GameObject에 반영한다
    /// ex) Block 종류에 따른 Sprite 종류 업데이트
    /// 생성자 또는 플레이도중에 Block Type이 변경될 때 호출된다.
    /// </summary>
    /// <param name="bValueChanged">플레이 도중에 Type이 변경되는 경우 true, 그렇지 않은 경우 false</param>
    public void UpdateView(bool bValueChanged)
    {
        if (m_Block.type == BlockType.EMPTY)
        {
            m_SpriteRenderer.sprite = null;
        }
        else if (m_Block.type == BlockType.BASIC)
        {
            m_SpriteRenderer.sprite = basicBlockSprites[(int)m_Block.unitKey];
        }
    }

    /*
     * 블럭을 제거한다.  
     */
    public void DoActionClear()
    {
        StartCoroutine(CoStartSimpleExplosion(true));
    }

    /*
     * 블럭이 폭발한 후, GameObject를 삭제한다.
     */
    IEnumerator CoStartSimpleExplosion(bool bDestroy = true)
    {
        ////1. 크기가 줄어드는 액션 실행한다 : 폭파되면서 자연스럽게 소멸되는 모양 연출, 1 -> 0.3으로 줄어든다.
        yield return Action2D.Scale(transform, Constants.BLOCK_DESTROY_SCALE, 4f);

        //2. 폭파시키는 효과 연출 : 블럭 자체의 Clear 효과를 연출한다 (모든 블럭 동일)
        GameObject explosionObj = GetExplosionObject(BlockQuestType.CLEAR_SIMPLE);
        ParticleSystem.MainModule newModule = explosionObj.GetComponent<ParticleSystem>().main;
        newModule.startColor = GetBlockColor(m_Block.unitKey);

        explosionObj.transform.position = this.transform.position;
        explosionObj.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        //3. 블럭 GameObject 객체 삭제 or make size zero
        if (bDestroy)
            PoolManager.Instance.DespawnObject(EPrefabsType.InGameBlock, gameObject);//Destroy(gameObject);
        else
        {
            Debug.Assert(false, "Unknown Action : GameObject No Destory After Particle");
        }
    }

}
