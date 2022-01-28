using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMonolith : MonoBehaviour
{

    public float timerToShowMonolith;
    private Renderer renderer;
    public bool isHidden = false;
    // Start is called before the first frame update
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isHidden)
        {
            if (timerToShowMonolith > 0)
            {
                timerToShowMonolith -= Time.deltaTime;
            }
            if (timerToShowMonolith <= 0)
            {
                renderer.enabled = true;
                isHidden = false;
            }
        }
    }
}
