using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{

    public Texture2D cursorCrosshair;

    private void Awake()
    {
        ChangeCursorType(cursorCrosshair);
    }

    private void ChangeCursorType(Texture2D cursortexture)
    {
        Cursor.SetCursor(cursorCrosshair, new Vector2(cursortexture.width / 2, cursortexture.height / 2), CursorMode.Auto);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
