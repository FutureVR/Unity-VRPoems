using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    public string levelName;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Application.LoadLevel(levelName);
        }
    }
}
