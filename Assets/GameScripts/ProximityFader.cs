using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProximityFader : MonoBehaviour
{
    [Tooltip("The trigger radius must be >= to start fade")] 
    [SerializeField] float startFadeAtRadius;
    [SerializeField] float endFadeAtRadius;

    private Vector3 origScale;
    private Transform fadeChild;


    // Start is called before the first frame update
    void Start()
    {
        fadeChild = transform.GetChild(0);
        origScale = fadeChild.localScale;
    }

    void OnTriggerStay(Collider other)
    {
        fadeChild.localScale = Vector3.Lerp(Vector3.zero, origScale, 
            Mathf.Clamp((Vector3.Distance(transform.position, other.transform.position) - endFadeAtRadius) / startFadeAtRadius, 0, 1)); 
    }
}
