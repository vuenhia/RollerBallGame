using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinXYZ : MonoBehaviour {

    [SerializeField]
    private Vector3 spinSpeed;

    // Use this for initializtion
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () 
    {
        transform.Rotate(spinSpeed.x * Time.deltaTime , spinSpeed.y * Time.deltaTime , spinSpeed.z * Time.deltaTime);
	}
}
