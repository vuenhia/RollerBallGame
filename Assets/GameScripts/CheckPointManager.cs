using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    private CheckPointFlag lastFlag = null;

    [SerializeField]
    private bool stopObject = false;

    public new CheckPointFlag getLastFlag()
    {
        return lastFlag;
    }

    public void setLastFlag(CheckPointFlag newLocation)
    {
        lastFlag = newLocation;
    }
    
    public void resetToLastFlag(GameObject teleportMe)
    {
        if (lastFlag != null)
        {
            // Reset the object to checkpoint
            teleportMe.transform.position = lastFlag.GetSpawnPoint().position;

            if (stopObject && teleportMe.GetComponent<Rigidbody>() != null )
            {
                teleportMe.GetComponent<Rigidbody>().velocity = new Vector3();
            }

        }
    }
}
