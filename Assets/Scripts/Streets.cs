using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Streets : MonoBehaviour
{

    List<List<Vector3>> points = new List<List<Vector3>>();
    public GameObject[] buildings;
    public float[] buildWidths;
    public Material groundMaterial;
    public Transform player;
    public float width;
    public float length;
    public float numOfSectionsBetween;

    float roadLength;
    public float laneWidth;
    float laneSectionLength = 15;
    float intersectionLength = 30;

    public GameObject lane;
    public GameObject intersection;
    public GameObject end;

    void Start()
    {
        //Make sure that the length of the road is a multiple of prefab size
        roadLength = numOfSectionsBetween * laneSectionLength + intersectionLength;
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.GetComponent<Renderer>().material = groundMaterial;
        ground.transform.localScale = new Vector3(roadLength*width, 1, roadLength*length);
        ground.transform.position = new Vector3(roadLength * width / 2, -1, roadLength * length / 2);

        createPointsVector();
        instantiateStreets();
        instantiateIntersections();

        makeBlocks();
    }

    void Update()
    {
        if (player.transform.position.x > roadLength*width / 3.5 || player.transform.position.y > roadLength*width / 3.5)
        {
            length++;
            width++;

            //GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("destroy");
            //for (int i = 0; i < gameObjects.Length; i++)
            //    Destroy(gameObjects[i]);

            createPointsVector();
            instantiateStreets();
            instantiateIntersections();
            makeBlocksExtend();
        }
    }

    void createPointsVector()
    {
        for (int z = 0; z < length; z++)
        {
            points.Add(new List<Vector3>());
            for (int x = 0; x < width; x++)
            {
                points[z].Add(new Vector3(x * roadLength, 0, z * roadLength));
            }
        }
    }

    void makeBlocks()
    {
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float offset = roadLength / 2;
                Vector3 centerPoint = points[z][x] + new Vector3(offset, 0, offset);
                float laneLength = (roadLength - laneWidth) / 2;
                Block block = new Block(centerPoint, laneLength, buildings, buildWidths);
            }
        }
    }

    void makeBlocksExtend()
    {
        int z = (int)length-1;
        for (int x = 0; x < width-1; x++)
         {
             float offset = roadLength / 2;
             Vector3 centerPoint = points[z][x] + new Vector3(offset, 0, offset);
             float laneLength = (roadLength - laneWidth) / 2;
             Block block = new Block(centerPoint, laneLength, buildings, buildWidths);
         }

        int m = (int)width-1;
        for (z = 0; z < length-1; z++)
        {
            float offset = roadLength / 2;
            Vector3 centerPoint = points[z][m] + new Vector3(offset, 0, offset);
            float laneLength = (roadLength - laneWidth) / 2;
            Block block = new Block(centerPoint, laneLength, buildings, buildWidths);
        }
    }

    void instantiateStreets()
    {
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                //Instantiate streets
                if (0 <= z && z < length - 1 && 0 <= x && x < width - 1)
                {
                    Vector3 currPos = points[z][x];
                    currPos += new Vector3(0, 0, intersectionLength);
                    for (int i = 0; i < numOfSectionsBetween; i++)
                    {
                        Instantiate(lane, currPos, Quaternion.Euler(new Vector3(-90, 0, 0)));
                        currPos = new Vector3(currPos.x, currPos.y, currPos.z + laneSectionLength);
                    }

                    currPos = points[z][x];
                    currPos += new Vector3(intersectionLength, 0, 0);
                    for (int i = 0; i < numOfSectionsBetween; i++)
                    {
                        Instantiate(lane, currPos, Quaternion.Euler(new Vector3(-90, 90, 0)));
                        currPos = new Vector3(currPos.x + laneSectionLength, currPos.y, currPos.z);
                    }
                }
            }
        }
    }

    void instantiateIntersections()
    {
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                //Instantiate Intersections
                Vector3 myPos = new Vector3(points[z][x].x, points[z][x].y, points[z][x].z);
                Quaternion myRot = Quaternion.identity;
                myRot.eulerAngles.Set(-90, 0, 0);
                Instantiate(intersection, myPos, Quaternion.Euler(new Vector3(-90, 0, 0)));
            }
        }
    }

}
