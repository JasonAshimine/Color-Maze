using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Variable
{
    [CreateAssetMenu(fileName = "MenuDataSet", menuName = "Data/Menu")]
    public class MenuDataSet : EventToggleScriptableObject<Menu>
    {


        public void Reset()
        {
            state = Menu.Main;
            toggle = false;
        }
    }
}
