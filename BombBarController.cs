using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombBarController : MonoBehaviour
{

    public Slider slider;
    // Start is called before the first frame update    

    public void SetMaxBombValue(int bombvalue)
    {
        slider.maxValue = bombvalue;
        slider.value = bombvalue;
    }

    public void SetBombValue(int bombvalue)
    {
        slider.value = bombvalue;
    }
}
