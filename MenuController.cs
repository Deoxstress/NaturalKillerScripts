using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
public class MenuController : MonoBehaviour
{  
    [SerializeField] private InputSystemUIInputModule inputSystem;
    [SerializeField] private bool isSelectLevel, isOptions;
    [SerializeField] private CinemachineCamSwitch cmCamSwitch;
    [SerializeField] private GameObject canvasMainMenu, canvasSelectLevel, canvasOptions;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject firstSelectedInMenu;
    private static readonly string hasFinishedLevel1 = "Level1Finished";
    private int firstPlayMenuInt;
    [SerializeField] private Button level2;
    [SerializeField] private GameObject textLevel2;
    private void Awake()
    {
        // Action that requires A button on gamepad to be held for half a second.
        canvasSelectLevel.GetComponent<Animator>().enabled = false;
        canvasOptions.GetComponent<Animator>().enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        firstPlayMenuInt = PlayerPrefs.GetInt("FirstPlayMenu");

        if(firstPlayMenuInt == 0)
        {
            PlayerPrefs.SetInt(hasFinishedLevel1, 0);
            PlayerPrefs.SetInt("FirstPlayMenu", 1);
        }

        if(PlayerPrefs.GetInt(hasFinishedLevel1) == 1)
        {
            level2.interactable = true;
            textLevel2.SetActive(true);
        }
        else if(PlayerPrefs.GetInt(hasFinishedLevel1) == 0)
        {
            level2.interactable = false;
            textLevel2.SetActive(false);
        }
        eventSystem = GetComponent<EventSystem>();

    }

    // Update is called once per frameDeox
    void Update()
    {
        
        if (inputSystem.cancel.action.triggered && !isSelectLevel)
        {
            Quit();
        }
        else if(inputSystem.cancel.action.triggered && isSelectLevel && !inputSystem.submit.action.triggered)
        {
            cmCamSwitch.SwitchStateMenu("MenuMainCamera");           
            canvasMainMenu.GetComponent<Animator>().Play("FadeMenuAlpha");
            canvasSelectLevel.GetComponent<Animator>().Play("FadeSelectAlphaOut");
            eventSystem.SetSelectedGameObject(firstSelectedInMenu);
        }
        if (inputSystem.cancel.action.triggered && isOptions && !inputSystem.submit.action.triggered)
        {
            cmCamSwitch.SwitchStateMenu("MenuMainCamera");
            canvasMainMenu.GetComponent<Animator>().Play("FadeMenuAlpha");
            canvasOptions.GetComponent<Animator>().Play("FadeSelectAlphaOut");
            eventSystem.SetSelectedGameObject(firstSelectedInMenu);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartLevel(int scenedIndex)
    {
        SceneManager.LoadScene(scenedIndex);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void SetIsSelecLevel(bool value)
    {
        isSelectLevel = value;
    }
    public void SetIsOptions(bool value)
    {
        isOptions = value;
    }

}
