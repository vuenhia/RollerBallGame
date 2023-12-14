using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionExit(Collision collision)
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(
            Random.Range(0.1f, 1f),
            Random.Range(0.1f, 1f),
            Random.Range(0.1f, 1f)
        );

    }
}
