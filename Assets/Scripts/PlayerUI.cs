using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winScreen;

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

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }

	public static void ReloadScene()
	{
		SceneManager.LoadScene(2);
	}

    public void FixSwitch()
    {
        GameManager.playerHasFixedSwitch = true;
        CheckAllFixed();
	}

	public void FixWires()
    {
        GameManager.playerHasFixedWires = true;
        CheckAllFixed();
	}

	public void FixGenerator()
    {
        GameManager.playerHasFixedGenerator = true;
        CheckAllFixed();
    }

    void CheckAllFixed()
    {
        if(GameManager.playerHasFixedSwitch && GameManager.playerHasFixedWires && GameManager.playerHasFixedGenerator)
        {
			GameManager.playerHasFixedAll = true;
		}
    }
}
