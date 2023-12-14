using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField]
    private CheckPointManager playerData = null;

    private void OnTriggerEnter(Collider other)
    {
        playerData.resetToLastFlag(other.gameObject);
    }
}
