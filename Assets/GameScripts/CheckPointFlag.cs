using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointFlag : MonoBehaviour
{
    [SerializeField]
    private bool checkPointEnabled = false;

    [SerializeField]
    private Transform spawnPoint = null;

    [SerializeField]
    private CheckPointManager playerData = null;

    private void OnTriggerEnter(Collider other)
    {
        if (!checkPointEnabled)
        {
            checkPointEnabled = true;

            // Turn on any visual ques
            SpinXYZ spinScript = gameObject.GetComponentInChildren<SpinXYZ>();
            // gameObject = this
            if (spinScript != null)
            {
                spinScript.enabled = true;
            }


            PingPongXYZ moveScript = gameObject.GetComponentInChildren<PingPongXYZ>();
            if (moveScript != null)
            {
                moveScript.enabled = true;
            }


            ParticleSystem particles = gameObject.GetComponentInChildren<ParticleSystem>();
            if (particles != null)
            {
                particles.Play();
            }

            // Place setLastFlag here if checkpoint can only be reached once
            //playerData.setLastFlag(this);
        }
        // Place setLastFlag here to allow repeat checkpoints at this site
        playerData.setLastFlag(this);
    }
    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }


}
