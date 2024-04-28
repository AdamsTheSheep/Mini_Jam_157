using UnityEngine;
using UnityEngine.UI;

public class InteractableObjectiveBoxGenerator : MonoBehaviour, IInteractable
{
	[SerializeField] SpatializedAudio audio;

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

	public void Interact()
	{
		if(!GameManager.playerHasFixedGenerator)
			StartGeneratorMinigame();
	}

	public void StopInteract()
	{
	}

	private void Update()
	{
		if (GameManager.usingGenerator)
		{
			//trigger position animation
			animationCurveProgress += Time.deltaTime;
			if (animationCurveProgress > 1 / generatorTriggerPositionCurveSpeed) animationCurveProgress -= 1f / generatorTriggerPositionCurveSpeed;
			animationCurveValue = generatorTriggerPositionCurve.Evaluate(animationCurveProgress * generatorTriggerPositionCurveSpeed);
			generatorTriggerPosition.anchoredPosition = new Vector2(0f, animationCurveValue * generatorUIRT.rect.height);

			if (inFillAnimation)
			{
				if (animationCurveValue >= generatorFill.fillAmount)
				{
					generatorFill.fillAmount = Mathf.Min(targetFill, animationCurveValue);
					if (generatorFill.fillAmount >= .99f)
					{
						GameManager.usingGenerator = false;
						PlayerUI.instance.FixGenerator();
						audio.PlayLoop();
					}
					if (animationCurveValue > targetFill) inFillAnimation = false;
				}
				return;
			}

			//Press E to try booting generator when not in fill animation
			else if (Input.GetKeyDown(KeyCode.E))
			{
				audio.PlaySound(1);
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
			GameManager.usingGenerator = false;
			generatorUI.SetActive(false);
		}
	}

	public void StartGeneratorMinigame()
	{
		generatorUI.SetActive(true);
		GameManager.usingGenerator = true;
		generatorFill.fillAmount = 0f;
		animationCurveProgress = 0f;
	}
}
