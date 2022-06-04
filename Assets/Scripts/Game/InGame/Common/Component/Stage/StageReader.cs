using UnityEngine;
using System.Collections;

public static class StageReader
{
    /// <summary>
    /// 스테이지 구성을 위해서 구성정보를 로드한다. 
    /// </summary>
    /// <param name="nStage"> 스테이지 번호</param>
    /// <returns></returns>
    public static StageInfo LoadStage(int nStageKey)
    {
        //1. definitionManager에서 keynum으로 해당 stage정보를 받아온단.
        StageDetailBoardDefinition mapDef = DefinitionManager.Instance.GetData<StageDetailBoardDefinition>(nStageKey);
        if (mapDef != null)
        {
            //2. definition 클래스를 stageInfo 객체로 변환한다.
            StageInfo stageInfo = new StageInfo(mapDef.key, mapDef.row, mapDef.col, mapDef.cells);

            //3. 변환된 객체가 유효한지 체크한다(only Debugging)
            Debug.Assert(stageInfo.DoValidation());

            return stageInfo;
        }

        return null;
    }
}