using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    public Image interactableHoldProgressImage;
    public Image pointerImage;
    public TextMeshProUGUI holdInteractionText;
	[SerializeField] float missingWireTapeMessageDuration;
	[SerializeField] string missingWireTapeMessage;
    [SerializeField] Color defaultColor;
    [SerializeField] Color errorColor;
    [SerializeField] TextMeshProUGUI inventoryText;

	Timer timer;

    private void Awake()
    {
        if (instance) Destroy(instance.gameObject);
        instance = this;
    }

    public void ShowWireTapeMissingMessage()
    {
        if (timer)
        {
            timer.OnTimerEnded?.Invoke();
        }

        timer = Timer.CreateTimer(gameObject, missingWireTapeMessageDuration, false, true);
		holdInteractionText.gameObject.SetActive(true);
        holdInteractionText.color = errorColor;
        holdInteractionText.text = missingWireTapeMessage;
        timer.OnTimerEnded += () =>
        {
            holdInteractionText.gameObject.SetActive(false);
            holdInteractionText.color = defaultColor;
        };
    }

    public void CollectWireTape()
    {
        GameManager.PlayerHasWireTape = true;
    }

    public void UpdateWireTapeUI()
    {
		inventoryText.gameObject.SetActive(GameManager.PlayerHasWireTape);
    }
}
