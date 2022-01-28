using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorBehaviour : MonoBehaviour
{
    private PlayerActionControls playerCursorActions;
    private float xMovCursorAxis;
    private float yMovCursorAxis;
    private Mouse mouseToController;
    private Vector2 moveCursorLeft;
    private Vector2 moveCursorUp;
    private Vector2 currentCursorPosition;
    private Vector2 targetPosition;
    public float CursorSpeed = 10.0f;
    private bool warpToCenter;
    // Start is called before the first frame update
    void Awake()
    {
        playerCursorActions = new PlayerActionControls();
        mouseToController = Mouse.current;
        warpToCenter = true;
        Cursor.lockState = CursorLockMode.Confined;
        
    }

    private void OnEnable()
    {
        playerCursorActions.Enable();
    }

    private void OnDisable()
    {
        playerCursorActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        xMovCursorAxis = playerCursorActions.OnVirus.CursorHorizontalControl.ReadValue<float>();
        yMovCursorAxis = playerCursorActions.OnVirus.CursorVerticalControl.ReadValue<float>();

        moveCursorLeft = new Vector2(xMovCursorAxis * CursorSpeed, 0);
        moveCursorUp = new Vector2(0, yMovCursorAxis * CursorSpeed);
        currentCursorPosition = mouseToController.position.ReadValue();
        Debug.Log(currentCursorPosition);
        targetPosition = currentCursorPosition + moveCursorLeft + moveCursorUp;

        if (warpToCenter)
        {
            mouseToController.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
            warpToCenter = false;
        }

        else
        {
            mouseToController.WarpCursorPosition(targetPosition);
        }
        

    }
}
