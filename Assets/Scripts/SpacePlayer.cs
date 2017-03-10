using UnityEngine;
using System.Collections;

public class SpacePlayer : MonoBehaviour {

    GameObject player;
    Vector3 position = new Vector3(0, 0, 0);
    float acc = 5;
	
	void Start ()
    {
        player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
	}
	
	
	void Update ()
    {
	    
	}
}
