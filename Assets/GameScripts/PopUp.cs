using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField]
    private float popUpForce;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PopUpHit");
        if (collision.gameObject.GetComponent<RollerBallMover>())
        {
            if (collision.gameObject.GetComponent<Rigidbody>())
            {
                Vector3 localRotation = transform.up.normalized;
                collision.gameObject.GetComponent<Rigidbody>().AddForce(localRotation * popUpForce);
            }
        }


    }
}
