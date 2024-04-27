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

    public bool usingGenerator;
    bool inFillAnimation;
    [SerializeField] GameObject generatorUI;
    [SerializeField] RectTransform generatorUIRT;
    [SerializeField] Image generatorFill;
    [SerializeField] RectTransform generatorTriggerPosition;
    [SerializeField] float successGeneratorTriggerPosition;
    [SerializeField] float generatorFillProgressPerSuccess;
    [SerializeField] AnimationCurve generatorTriggerPositionCurve;
    [SerializeField] float generatorTriggerPositionCurveSpeed;
    float targetFill;
    float animationCurveProgress;
    float animationCurveValue;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winScreen;

	Timer timer;

    private void Awake()
    {
        if (instance) Destroy(instance.gameObject);
        instance = this;
	}

    private void Update()
    {
        if (usingGenerator)
		{
			//trigger position animation
			animationCurveProgress += Time.deltaTime;
            if (animationCurveProgress > 1/generatorTriggerPositionCurveSpeed) animationCurveProgress -= 1f / generatorTriggerPositionCurveSpeed;
			animationCurveValue = generatorTriggerPositionCurve.Evaluate(animationCurveProgress * generatorTriggerPositionCurveSpeed);
			generatorTriggerPosition.anchoredPosition = new Vector2(0f, animationCurveValue * generatorUIRT.rect.height);

            if (inFillAnimation)
            {
                if (animationCurveValue >= generatorFill.fillAmount)
                {
                    generatorFill.fillAmount = Mathf.Min(targetFill, animationCurveValue);
                    if (generatorFill.fillAmount >= .99f)
                    {
                        usingGenerator = false;
                        FixGenerator();
                    }
                    if (animationCurveValue > targetFill) inFillAnimation = false;
                }
                return;
            }

            //Press E to try booting generator when not in fill animation
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //Check if trigger is in correct zone
                if (animationCurveValue <= successGeneratorTriggerPosition)
                {
                    if (animationCurveProgress > 1 / generatorTriggerPositionCurveSpeed / 2) animationCurveProgress = 0f;
                    targetFill += generatorFillProgressPerSuccess;
                    inFillAnimation = true;
                }
                else
                {
                    generatorFill.fillAmount = 0f;
                    targetFill = 0f;
                }
            }
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			usingGenerator = false;
			generatorUI.SetActive(false);
		}
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

    public void StartGeneratorMinigame()
    {
        generatorUI.SetActive(true);
        usingGenerator = true;
        generatorFill.fillAmount = 0f;
        animationCurveProgress = 0f;
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
