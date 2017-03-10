using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Galaxy : MonoBehaviour
{
    public Material[] materials;
    public string[] lines;
    public Transform player;
    List<Orbital> orbitals = new List<Orbital>();
    public int numOfOrbitals = 2;
    float spawnRange = 4000;
    float startTime;

    void Start()
    {
        startTime = Time.time;

        for (int i = 0; i < numOfOrbitals; i++)
        {
            Orbital orb = new Orbital(null, 0, materials, lines, player);
            Vector3 myPos = Random.onUnitSphere * spawnRange;
            orb.planet.transform.position = myPos;
            orbitals.Add(orb);
        }
    }

    void Update()
    {
        foreach (Orbital o in orbitals)
        {
            o.mainUpdate();
        }
        Debug.Log(Time.time - startTime);
        if (Time.time - startTime >= 30)
        {
            Application.LoadLevel("Menu");
        }
    }
}
