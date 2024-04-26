using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] AudioSource nonSpatializedSFXAudioSource;
	[SerializeField] AudioSource loopAudioSource;

	public static AudioManager instance;

	private void Awake()
	{
		if (instance) Destroy(instance.gameObject);
		instance = this;
	}

	public void PlayNonSpatializedSFX(AudioClip clip)
	{
		nonSpatializedSFXAudioSource.PlayOneShot(clip);
	}

	public void PlayOnLoop(AudioClip clip)
	{
		loopAudioSource.clip = clip;
		loopAudioSource.loop = true;
		loopAudioSource.Play();
	}

	public void StopLoop()
	{
		loopAudioSource.Stop();
	}
}
