using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Level_End : MonoBehaviour
{
    public Volume m_Volume;
    public float TimeToFade = 0.5f;
    public float SpeedAnimation = 1.0f;
    private float minimum = 1.0f;
    private float maximum = 0.0f;
    static float t = 0.0f;

    public Animator EndOfLevel;
    /*public GameObject[] MonolithList;
    public bool isStatic;*/

    void Start()
    {
        EndOfLevel.speed = SpeedAnimation;
    }

    void Update()
    {
        /*foreach (GameObject Monolith in MonolithList)
        {
            Monolith.isStatic = false;
        }*/

        m_Volume.weight = Mathf.Lerp(minimum, maximum, t);
        t += TimeToFade * Time.deltaTime;

        EndOfLevel.enabled = true;
        EndOfLevel.Play("Monolith_Disappear");
    }


    
}

