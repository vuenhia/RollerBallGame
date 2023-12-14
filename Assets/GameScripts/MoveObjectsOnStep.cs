using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectsOnStep : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToLeft;

    [SerializeField]
    private GameObject objectToRight;

    private bool playerOnTile = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTile = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTile = false;
        }
    }

    private void Update()
    {
        if (playerOnTile)
        {
            // Move the objects left and right
            objectToLeft.transform.Translate(Vector3.left * Time.deltaTime);
            objectToRight.transform.Translate(Vector3.right * Time.deltaTime);
        }
    }
}
