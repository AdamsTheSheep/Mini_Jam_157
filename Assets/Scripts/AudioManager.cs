using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Header("AUDIOSOURCES")]
	[SerializeField] AudioSource nonSpatializedSFXAudioSource;
	[SerializeField] AudioSource musicAudioSource;

	[Space(5)]
	[Header("AMBIANCE")]
	[SerializeField] AudioClip[] ambianceLoop;
	[SerializeField] List<AudioClip> randomAmbiantSounds;
	[SerializeField] float minTimeBetweenRandomSounds = 5f;
	[SerializeField] float maxTimeBetweenRandomSounds = 20f;
	[SerializeField] float minRandomPitch = .9f;
	[SerializeField] float maxRandomPitch = 1.1f;


	public static AudioManager instance;

	float nextRandomSoundTime;
	float currentRandomSoundTime;

	Coroutine playlistCoroutine;

	private void Awake()
	{
		if (instance) return;

		instance = this;
		if(randomAmbiantSounds.Count > 0) StartCoroutine(PlayAmbianceAtRandomTime());
		PlayMusics(ambianceLoop);
	}

	public void PlayNonSpatializedSFX(AudioClip clip, bool randomPitch = false)
	{
		nonSpatializedSFXAudioSource.pitch = randomPitch ? Random.Range(minRandomPitch, maxRandomPitch) : 1f;
		nonSpatializedSFXAudioSource.PlayOneShot(clip);
	}

	IEnumerator PlayAmbianceAtRandomTime()
	{
		nextRandomSoundTime = Random.Range(minTimeBetweenRandomSounds, maxTimeBetweenRandomSounds);
		yield return new WaitForSeconds(nextRandomSoundTime);
		print("Play random ambiance sfx");
		PlayNonSpatializedSFX(randomAmbiantSounds[Random.Range(0, randomAmbiantSounds.Count)]);
		StartCoroutine(PlayAmbianceAtRandomTime());
	}

	public void PlayMusic(AudioClip clip, bool loop = true)
	{
		musicAudioSource.clip = clip;
		musicAudioSource.loop = loop;

		musicAudioSource.Play();
	}

	public void StopMusic()
	{
		musicAudioSource?.Stop();
	}

	public void PlayMusics(params AudioClip[] clips)
	{
		if (playlistCoroutine != null) StopCoroutine(playlistCoroutine);
		playlistCoroutine = StartCoroutine(InternalPlayList(clips));
	}

	public void StopMusics()
	{
		StopCoroutine(playlistCoroutine);
		musicAudioSource.Stop();
	}

	private IEnumerator InternalPlayList(AudioClip[] clips)
	{
		for (int i = 0; i < clips.Length; i++)
		{
			musicAudioSource.clip = clips[i];
			musicAudioSource.Play();

			while (musicAudioSource.isPlaying)
			{
				yield return null;
			}
		}
		bool loop = true;
		while (loop)
		{
			musicAudioSource.Play();

			while (musicAudioSource.isPlaying)
			{
				yield return null;
			}
		}
	}
}
