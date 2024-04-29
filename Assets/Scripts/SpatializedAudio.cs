using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpatializedAudio : MonoBehaviour
{
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip[] loopClips;
	[SerializeField] AudioClip[] soundBank1;
	[SerializeField] AudioClip[] soundBank2;

	Coroutine loopCoroutine;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void PlaySound(int soundBank = 1)
	{
		AudioClip[] bank;
		switch (soundBank)
		{
			case 1:
				bank = soundBank1;
				break;
			case 2:
				bank = soundBank2;
				break;
			default:
				bank = soundBank1;
				break;
		}
		if (bank.Length <= 0) return;
		AudioClip clip = bank[Random.Range(0, bank.Length)];
		audioSource.PlayOneShot(clip);
	}

	public void PlayLoop()
	{
		if (loopClips.Length <= 0) return;
		if (loopCoroutine != null) StopCoroutine(loopCoroutine);
		loopCoroutine = StartCoroutine(InternalPlayList(loopClips));
	}

	public void StopMusics()
	{
		StopCoroutine(loopCoroutine);
		audioSource.loop = false;
		audioSource.Stop();
	}

	private IEnumerator InternalPlayList(AudioClip[] clips)
	{
		for (int i = 0; i < clips.Length; i++)
		{
			audioSource.clip = clips[i];
			audioSource.Play();

			while (audioSource.isPlaying)
			{
				yield return null;
			}
		}
		bool loop = true;
		while (loop)
		{
			audioSource.Play();

			while (audioSource.isPlaying)
			{
				yield return null;
			}
		}
	}

}
