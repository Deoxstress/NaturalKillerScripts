using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    public Slider HpSlider;
    public Slider XpSlider;
    public Gradient hpGradient;
    public Image hpFill;
    public Image cardioFill;
    public Image hpBarMockupTest;
    //public Image RadialObjectiveProgressBar;
    public TextMeshProUGUI tmpProgressText;
    public float objectivesTotal, objectivesCleared;
    [SerializeField] private PlayerValues PlayerLevelValues;
    [SerializeField] private GameObject levelUpVfxPrefab;
    [SerializeField] private GameObject[] totemsClear;
    //[SerializeField] private Image[] totemsPlaceholder;
    [SerializeField] private LevelChanger levelChanger;
    [SerializeField] private Animator screenHit;
    [SerializeField] private VisualEffect hpLowEffect;

    private PlayerMovement player;
    // Start is called before the first frame update    
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        levelChanger = FindObjectOfType<LevelChanger>();
        screenHit = GetComponent<Animator>();
        //hpLowEffect = GameObject.FindGameObjectWithTag("HPLow").GetComponent<VisualEffect>();
    }
    void Start()
    {        
        SetMaxXpValue(PlayerLevelValues.maxXpValue);
        SetMaxHpValue(PlayerLevelValues.maxHpValue, player.currentHpValue);
        SetXpValue(0);
        //RadialObjectiveProgressBar.fillAmount = 0.0f;
        //FillObjective();
    }


    void Update()
    {
        if(XpSlider.value >= XpSlider.maxValue)
        {
            PlayerLevelValues.LevelUp(PlayerLevelValues.playerLevel);
            //Quand level up, ne pas oublier de faire level +1 et set les variables ici.
            player.currentXpValue = (int)(XpSlider.value - XpSlider.maxValue);
            SetXpValue(player.currentXpValue);         
            SetMaxXpValue(PlayerLevelValues.maxXpValue);
            GameObject levelUpClone = Instantiate(levelUpVfxPrefab, player.transform);
        }
    }
    public void SetMaxHpValue(int hpValue, int currentHpValue)
    {
        HpSlider.maxValue = hpValue;
        HpSlider.value = currentHpValue;
        if (hpBarMockupTest != null)
        {
            hpBarMockupTest.fillAmount = HpSlider.normalizedValue;
            hpBarMockupTest.color = hpGradient.Evaluate(HpSlider.normalizedValue);
        }
        //hpBarMockupTest.fillAmount = HpSlider.normalizedValue;
        //hpBarMockupTest.color = hpGradient.Evaluate(HpSlider.normalizedValue);
        hpFill.color = hpGradient.Evaluate(HpSlider.normalizedValue);
        cardioFill.color = hpGradient.Evaluate(HpSlider.normalizedValue);
        hpLowEffect.SetFloat("Line_longueur", HpSlider.normalizedValue);
    }

    public void SetHpValue(int hpValue)
    {
        HpSlider.value = hpValue;
        if(hpBarMockupTest != null) 
        {
            hpBarMockupTest.fillAmount = HpSlider.normalizedValue;
            hpBarMockupTest.color = hpGradient.Evaluate(HpSlider.normalizedValue);
        }
        hpFill.color = hpGradient.Evaluate(HpSlider.normalizedValue);
        cardioFill.color = hpGradient.Evaluate(HpSlider.normalizedValue);
        hpLowEffect.SetFloat("Line_longueur", HpSlider.normalizedValue);
    }

    public void SetMaxXpValue(int xpValue)
    {
        XpSlider.maxValue = xpValue;
        XpSlider.value = 0;
    }

    public void SetXpValue(int xpValue)
    {
        XpSlider.value = xpValue;
    }

    private void FillObjective()
    {
        //RadialObjectiveProgressBar.fillAmount = objectivesCleared / objectivesTotal;
        //tmpProgressText.text = objectivesCleared + "/" + objectivesTotal;
    }

    public void FillTotemImage(int index)
    {
        totemsClear[index].SetActive(true);
    }

    public void CheckObjectiveClear()
    {
        if(objectivesCleared == objectivesTotal)
        {
            levelChanger.objectiveCleared = true;
            levelChanger.levelEndTriggered = true;
        }
    }

    public void TriggerScreenHit()
    {
        screenHit.SetTrigger("ScreenHit");
        Debug.Log("hit");
    }   
  
}
