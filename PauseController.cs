using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{

    [SerializeField] private GameObject canvasPause, canvasOptions;
    [SerializeField] private bool inOptions;
    [SerializeField] private PlayerMovement pm;
    
    // Start is called before the first frame update
    void Awake()
    {
        pm = FindObjectOfType<PlayerMovement>();
        canvasPause.GetComponent<Animator>().enabled = false;
        canvasOptions.GetComponent<Animator>().enabled = false;
    }
    void Start()
    {
        pm.playerActions.OnVirus.TriggerMenu.performed += ctx => { canvasPause.GetComponent<Animator>().enabled = true; canvasPause.GetComponent<Animator>().Play("FadePauseAlphaIn"); /*Cursor.lockState = CursorLockMode.Confined; Cursor.visible = true;*/ PauseGame(0); };
    }

    void Update()
    {
        //Pause disabled in scene reduction 1 after swithcing back & forth with options & pause.
        if(!inOptions)
        {
            pm.playerActions.OnVirus.TriggerMenu.Enable();
                       
        }
        if (inOptions)
        {
            pm.playerActions.OnVirus.TriggerMenu.Disable();
        }
    }


    // Update is called once per frame

    public void PauseGame(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenOptions()
    {
        canvasPause.GetComponent<Animator>().Play("FadePauseAlphaOut");
        canvasOptions.GetComponent<Animator>().enabled = true;
        canvasOptions.GetComponent<Animator>().Play("FadeOptionsAlphaIn");
        inOptions = true;
    }

    public void SetInOptions(bool value)
    {
        inOptions = value;
    }
}