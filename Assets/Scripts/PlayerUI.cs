using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] float timeLeft;
    bool lastMinuteSounsPlaying = false;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] AudioClip[] playerDeathSounds;
	public GameManager.Event GameLost;
    Timer timer;

    private void Awake()
    {
		DontDestroyOnLoad(gameObject);
        if (instance) Destroy(instance.gameObject);
        instance = this;
        GameManager.ResetStaticValues();

        Time.timeScale = 1f;
        Cursor.visible = false;
		GameLost += GameOver;
	}

	private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            DisplayTime(timeLeft);
            if (timeLeft < 60 && !lastMinuteSounsPlaying)
            {
                lastMinuteSounsPlaying = true;
                foreach (var item in FindObjectsByType<EmergencyLight>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    item.StartSound();
                }
            }
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverScreen.SetActive(true);
    }

    public void ShowWinScreen()
	{
		Time.timeScale = 0f;
		Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
		winScreen.SetActive(true);
	}

    void DisplayTime(float time)
    {
        time++;
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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

    public static void ReloadScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
		if (instance) Destroy(instance.gameObject);
        instance = GameObject.FindAnyObjectByType<PlayerUI>();
    }

    public static void CheckAllFixed()
    {
        if (GameManager.objectiveCount <= 0)
        {
            // Open doors
        }
    }

    [ContextMenu("KillPlayer")]
    public void KillPlayer()
    {
        //Change here if player should not be one hit death

        AudioManager.instance.PlayNonSpatializedSFX(playerDeathSounds[Random.Range(0, playerDeathSounds.Length)]);
        GameOver();
    }

	public static bool CanSendSound = true;
	public static void PlaySound(GameObject gameObject, Vector3 pos)
	{
		if (!CanSendSound) return;
		GlobalState.TriggerSuspicion(2, pos);
		var timer = Timer.CreateTimer(gameObject, 3,false, true);
		timer.OnTimerEnded += OnTimerEnded;
		CanSendSound = false;
	}

	public static void OnTimerEnded()
	{
		CanSendSound = true;
	}
}
