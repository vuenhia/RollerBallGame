using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]

public class RollerBallAudioPlayer : MonoBehaviour
{

    public enum InteractionTypes { Metal, Wood, Hard, Soft, VerySoft, Custom };

    [Tooltip("Array size should be 5, to match the InteractionTypes enumeration")]
    [SerializeField]
    private AudioClip[] hitAudioClips = null;

    [Tooltip("Array size should be 5, to match the InteractionTypes enumeration")]
    [SerializeField]
    private AudioClip[] rollAudioClips = null;

    [SerializeField]
	private float lowSpeedMagnitude = 0;

	[SerializeField]
	private float fullSpeedMagnitude = 0;

	private AudioSource audioRollPlayer;
    private AudioSource audioHitPlayer;
    private RollerSoundHolder defaultFX;
    private Rigidbody rb;

    private void Start()
    {
        AudioSource[] audioPlayers = GetComponents<AudioSource>();
        if (audioPlayers.Length > 1)
        {    
            audioRollPlayer = audioPlayers[0];
            audioRollPlayer.loop = true;
            audioRollPlayer.playOnAwake = false;

            audioHitPlayer = audioPlayers[1];
            audioRollPlayer.loop = false;
            audioHitPlayer.playOnAwake = false;
        } else
        {
            Debug.Log("Audio sources for " + gameObject.name + " not properly define, needs 2 AudioSource components.");
        }
		rb = GetComponent<Rigidbody>();
		defaultFX = GetComponent<RollerSoundHolder>();
		
		if (fullSpeedMagnitude <= lowSpeedMagnitude)
		{
			fullSpeedMagnitude = lowSpeedMagnitude + 1f;
		}

        if (rollAudioClips.Length != Enum.GetNames(typeof(InteractionTypes)).Length-1) {
            Debug.Log("Audio clips for rolling sounds not properly define for " + gameObject.name + " roller.");
            enabled = false;
        }

        if (hitAudioClips.Length != Enum.GetNames(typeof(InteractionTypes)).Length-1)
        {
            Debug.Log("Audio clips for hit sounds not properly define for " + gameObject.name + " roller.");
            enabled = false;
        }
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (!this.enabled) return;
        audioHitPlayer.PlayOneShot(selectHitClipByType(findSounds(collision)), calcAudioVolume(collision.impulse.magnitude * 4));
    }

	private void OnCollisionStay(Collision collision)
	{
        if (!this.enabled) return;

        audioRollPlayer.volume = calcAudioVolume(rb.velocity.magnitude);
        
        if (!audioRollPlayer.isPlaying)
		{
            audioRollPlayer.clip = selectRollClipByType(findSounds(collision));
            audioRollPlayer.Play();
		}
	}

	private void OnCollisionExit(Collision collision)
	{
        if (!this.enabled) return;
        audioRollPlayer.Stop();
    }

	private RollerSoundHolder findSounds(Collision collision)
	{
		if (collision.gameObject.GetComponent<RollerSoundHolder>() != null)
		{
			return collision.gameObject.GetComponent<RollerSoundHolder>();
		}
		else if (collision.gameObject.transform.GetComponentInParent<RollerSoundHolder>() != null)
		{
			return collision.gameObject.transform.GetComponentInParent<RollerSoundHolder>();
		}

		return defaultFX;
	}

    private AudioClip selectHitClipByType(RollerSoundHolder soundHolder)
    {
        if (soundHolder.getInteractionType() == InteractionTypes.Custom)
        {
            return soundHolder.getHitSound();
        }
        return selectClipByType(soundHolder.getInteractionType(), hitAudioClips);
    }

    private AudioClip selectRollClipByType(RollerSoundHolder soundHolder)
    {
        if (soundHolder.getInteractionType() == InteractionTypes.Custom)
        {
            return soundHolder.getRollSound();
        }
        return selectClipByType(soundHolder.getInteractionType(), rollAudioClips);
    }

    private AudioClip selectClipByType(InteractionTypes interType, AudioClip[] clipsList)
    {
        return clipsList[(int)interType];
    }

    private float calcAudioVolume(float magnitude)
	{
		return (magnitude - lowSpeedMagnitude) / (fullSpeedMagnitude - lowSpeedMagnitude);
	}
}
