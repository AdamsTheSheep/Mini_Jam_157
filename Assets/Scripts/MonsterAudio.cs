using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MonsterAudio : MonoBehaviour
{
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioSource loopAudioSource;

	[SerializeField] AudioClip[] steps;
	[SerializeField] AudioClip[] rage;
	[SerializeField] AudioClip[] idle;
	[SerializeField] AudioClip[] attacks;
	[SerializeField] AudioClip[] breathing;

	Coroutine loopCoroutine;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void PlaySteps(bool running = false)
	{
		PlaySound(steps);
		if (running) PlayLoop();
	}

	public void PlayRage()
	{
		PlaySound(rage);
	}

	public void PlayIdle()
	{
		PlaySound(idle);
	}

	public void PlayAttack()
	{
		PlaySound(attacks);
	}

	void PlaySound(AudioClip[] clips)
	{
		if (clips.Length <= 0) return;
		AudioClip clip = clips[Random.Range(0, clips.Length)];
		audioSource.PlayOneShot(clip);
	}

	public void PlayLoop()
	{
		if (loopCoroutine != null) StopCoroutine(loopCoroutine);
		if (breathing.Length <= 0) return;

		loopCoroutine = StartCoroutine(InternalPlayList(breathing));
	}

	public void StopLoop()
	{
		if(loopCoroutine != null) StopCoroutine(loopCoroutine);
		loopAudioSource.loop = false;
		loopAudioSource.Stop();
	}

	private IEnumerator InternalPlayList(AudioClip[] clips)
	{
		while(true)
		{
			loopAudioSource.clip = clips[Random.Range(0,clips.Length)];
			loopAudioSource.Play();

			while (loopAudioSource.isPlaying)
			{
				yield return null;
			}
		}
	}

	private void OnDestroy()
	{
		StopLoop();
	}
}
