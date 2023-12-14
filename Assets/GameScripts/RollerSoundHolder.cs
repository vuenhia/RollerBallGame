using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSoundHolder : MonoBehaviour {

    [Tooltip("Defines which sound profile should be used by the roller ball. Custom will use the AudioClip specified below")]
    [SerializeField]
    private RollerBallAudioPlayer.InteractionTypes interactionType = RollerBallAudioPlayer.InteractionTypes.VerySoft;
	[SerializeField]
	private AudioClip hitSound = null;
	[SerializeField]
	private AudioClip rollSound = null;

	public AudioClip getHitSound()
	{
		return hitSound;
	}

	public AudioClip getRollSound()
	{
		return rollSound;
	}

    public RollerBallAudioPlayer.InteractionTypes getInteractionType()
    {
        return interactionType;
    }
}
