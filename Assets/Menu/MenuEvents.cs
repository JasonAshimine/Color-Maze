using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variable;
public class MenuEvents : MonoBehaviour
{
    [SerializeField]
    private StateDataSet _stateData;
    public void GotoMain()
    {
        _stateData.Raise(GameStage.Gameplay);
    }
}
