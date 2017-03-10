using UnityEngine;
using System.Collections;

public class RotateLight : MonoBehaviour {

    Vector3 rotation;

	void Start ()
    {
           
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(.1f, .1f, Random.Range(.001f, .01f)));
	}
}
