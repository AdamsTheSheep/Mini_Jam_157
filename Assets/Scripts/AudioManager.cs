using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Header("AUDIOSOURCES")]
	[SerializeField] AudioSource nonSpatializedSFXAudioSource;
	[SerializeField] AudioSource loopAudioSource;

	[Space(5)]
	[Header("RANDOM AMBIANCE")]
	[SerializeField] List<AudioClip> randomAmbiantSounds;
	[SerializeField] float minTimeBetweenRandomSounds = 5f;
	[SerializeField] float maxTimeBetweenRandomSounds = 20f;
	[SerializeField] float minRandomPitch = .9f;
	[SerializeField] float maxRandomPitch = 1.1f;

	public static AudioManager instance;

	float nextRandomSoundTime;
	float currentRandomSoundTime;

	private void Awake()
	{
		if (instance) Destroy(instance.gameObject);
		instance = this;

		if(randomAmbiantSounds.Count > 0)
		StartCoroutine(PlayAmbianceAtRandomTime());
	}

	public void PlayNonSpatializedSFX(AudioClip clip, bool randomPitch = false)
	{
		nonSpatializedSFXAudioSource.pitch = randomPitch ? Random.Range(minRandomPitch, maxRandomPitch) : 1f;
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

	IEnumerator PlayAmbianceAtRandomTime()
	{
		nextRandomSoundTime = Random.Range(minTimeBetweenRandomSounds, maxTimeBetweenRandomSounds);
		yield return new WaitForSeconds(nextRandomSoundTime);
		PlayNonSpatializedSFX(randomAmbiantSounds[Random.Range(0, randomAmbiantSounds.Count)]);
		StartCoroutine(PlayAmbianceAtRandomTime());
	}
}
