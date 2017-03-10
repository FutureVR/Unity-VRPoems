using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Orbital
{
    public GameObject planet;
    public GameObject text;
    public Transform player;
    public Material[] materials;
    public string[] lines;

    int maxLevel = 4;
    int textLevelCutoff = 2;
    static float minNumOfChildren = 0;
    static float maxNumOfChildren = 3;
    int numOfChildren = (int)Mathf.Ceil( Random.Range(minNumOfChildren, maxNumOfChildren) );

    float angle = Random.Range(0, 360);
    Vector3 upwards = Random.onUnitSphere;

    float rotRadius;
    float planetRadius;
    float speed;

    float planetRadiusScale = 10;
    float rotRadiusScale = 140;
    float speedScale = .5f;

    Orbital parent;
    List<Orbital> children = new List<Orbital>();
    int level;
    

    public Orbital(Orbital p, int l, Material[] m, string[] s, Transform pl)
    {
        parent = p;
        level = l;
        materials = m;
        lines = s;
        player = pl;

        float levelMultiplier = Mathf.Pow(maxLevel + 1 - level, 2);
        planetRadius = levelMultiplier * planetRadiusScale * Random.Range(.8f, 1.2f);
        rotRadius = levelMultiplier * rotRadiusScale * Random.Range(.8f, 1.2f);
        speed = (1 / levelMultiplier) * speedScale * Random.Range(.8f, 1.2f);

        createThisPlanet();
        if (level <= textLevelCutoff) createText();
        if (level < maxLevel) createOrbitals();
    }

    public void mainUpdate()
    {
        setOrbitalPos();
        checkCollision();

        foreach (Orbital o in children)
        {
            o.mainUpdate();
        }
    }

    void setOrbitalPos()
    {
        if (parent != null)
        {
            //ParentPos, rotRadius, speed, angle
            angle += speed;

            Vector3 parentPos = parent.planet.transform.position;
            Vector3 offset = rotRadius * (new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)));
            Vector3 newPos = parentPos + offset;
            planet.transform.position = newPos;
        }
    }
    
    //Destroys the text if the player is nearby
    void checkCollision()
    {
        Vector3 difference = player.position - planet.transform.position;
        float planetBounds = 2 * planet.transform.localScale.x;
        if ( Vector3.Magnitude( difference ) < planetBounds )
        {
            text.SetActive(false);
        }
    }

    void createThisPlanet()
    {
        planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        planet.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);

        Renderer rend = planet.GetComponent<Renderer>();
        int index = (int)Mathf.FloorToInt(Random.Range(0, 3));  //FIX THIS!
        rend.material = materials[index];
    }

    void createText()
    {
        text = new GameObject();
        text.AddComponent<MeshRenderer>();
        TextMesh textMesh = text.AddComponent<TextMesh>();

        int index = (int)Mathf.FloorToInt(Random.Range(0, lines.Length - 1));
        textMesh.alignment = TextAlignment.Center;
        textMesh.text = lines[index];
        textMesh.characterSize = 5;
        textMesh.color = Color.black;
        textMesh.anchor = TextAnchor.MiddleCenter;

        float lineLength = 4 * planet.transform.localScale.x;
        int totalChars = lines[index].Length;
        textMesh.fontSize = (int)lineLength / totalChars;

        text.transform.parent = planet.transform;
    }

    void createOrbitals()
    {
        for (int i = 0; i < numOfChildren; i++)
        {
            Orbital orb = new Orbital(this, level + 1, materials, lines, player);
            children.Add(orb);
        }
    }
    
}
