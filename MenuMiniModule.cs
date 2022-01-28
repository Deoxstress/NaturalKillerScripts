using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMiniModule : MonoBehaviour
{

    [SerializeField] private MenuController mc;

    public void SetIsSelectLevelInAnim(int value)
    {
        if (value > 0)
            mc.SetIsSelecLevel(true);
        else
            mc.SetIsSelecLevel(false);
    }
    
}
