using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject TutorialDialogBox, GamePadBackgroundImage, GamepadButtonImage, ButtonImage, textDialogGamepad;
    [SerializeField] private bool DisplayGamepad, isTutoXEvent, eventActive;
    private PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        if (isTutoXEvent)
        {
            player = FindObjectOfType<PlayerMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!other.gameObject.GetComponent<PlayerMovement>().LockAllInputs)
            {
                TutorialDialogBox.SetActive(true);
                if (DisplayGamepad)
                {
                    GamePadBackgroundImage.SetActive(true);
                    GamepadButtonImage.SetActive(true);
                    ButtonImage.SetActive(true);
                    textDialogGamepad.SetActive(true);
                    if (isTutoXEvent)
                    {
                        if (player.triggerStatueValue > 0 || eventActive)
                        {                          
                            GamePadBackgroundImage.SetActive(false);
                            GamepadButtonImage.SetActive(false);
                            ButtonImage.SetActive(false);
                            TutorialDialogBox.SetActive(false);                          
                            textDialogGamepad.SetActive(false);
                            this.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                if (isTutoXEvent)
                {
                    if (eventActive)
                    {
                        GamePadBackgroundImage.SetActive(false);
                        GamepadButtonImage.SetActive(false);
                        ButtonImage.SetActive(false);
                        TutorialDialogBox.SetActive(false);
                        textDialogGamepad.SetActive(false);
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TutorialDialogBox.SetActive(false);
            if (DisplayGamepad)
            {
                GamePadBackgroundImage.SetActive(false);
                GamepadButtonImage.SetActive(false);
                ButtonImage.SetActive(false);
                textDialogGamepad.SetActive(false);
            }
        }
    }

    public void SetEventActive(bool newValue)
    {
        eventActive = newValue;
    }
}
