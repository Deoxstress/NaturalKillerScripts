using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    public bool objectiveCleared; // use for objectives completion
    public bool toMenu;
    public bool hasDied;
    public bool toLevel1;
    public bool toLevel2;
    public bool levelEndTriggered;
    [SerializeField] private Color colorFade;
    [SerializeField] private Level_End levelEnd;
    [SerializeField] private CinemachineCamSwitch cmSwitch;
    [SerializeField] private Animator CanvasScoringAnimator;
    [SerializeField] private EventSystem es;
    [SerializeField] private GameObject continueButton;
    private float timerToSwitchScene = 6.0f;
    private PlayerMovement pm;
    private PauseController pauseController;
    void Start()
    {
        if(FindObjectOfType<PlayerMovement>() != null)
        {
            pm = FindObjectOfType<PlayerMovement>();
        }
        if(FindObjectOfType<PauseController>() != null)
        {
            pauseController = FindObjectOfType<PauseController>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (toMenu)
        {
            FadeToLevel(0);
        }

        if (toLevel1)
        {
            FadeToLevel(1);
        }

        if (toLevel2)
        {
            FadeToLevel(2);
        }

        if (objectiveCleared)
        {
            timerToSwitchScene -= Time.deltaTime;
        }
        if(objectiveCleared && levelEndTriggered)
        {
            pauseController.SetInOptions(true);
            levelEnd.enabled = true;
            levelEndTriggered = false;
            cmSwitch.SwitchStateMenu("PlayerCamOutro");
            CanvasScoringAnimator.enabled = true;
            es.SetSelectedGameObject(continueButton);
            pm.LockAllInputs = true;
        }
        if(timerToSwitchScene <= 0)
        {
            
            //FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(hasDied)
        {
            FadeToLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void FadeToLevel(int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SetToLevel1(bool newValue)
    {
        if (GetComponent<CanvasGroup>() != null)
            GetComponent<CanvasGroup>().alpha = 1;
        toLevel1 = newValue;
        animator.SetFloat("SpeedMult", 0.25f);
    }

    public void SetToLevel2(bool newValue)
    {
        if(GetComponent<CanvasGroup>() != null)
            GetComponent<CanvasGroup>().alpha = 1;       
        toLevel2 = newValue;
        animator.SetFloat("SpeedMult", 0.25f);
    }

    public void SetToMenu(bool newValue)
    {
        if (GetComponent<CanvasGroup>() != null)
            GetComponent<CanvasGroup>().alpha = 1;
        toMenu = newValue;
        animator.SetFloat("SpeedMult", 0.25f);
    }

    public void SetLevel1Finished()
    {
        if (PlayerPrefs.GetInt("Level1Finished") == 0)
            PlayerPrefs.SetInt("Level1Finished", 1);
    }
}
