using UnityEngine;
using System.Collections;

public class Block
{
    Vector3 centerPoint;
    GameObject[] buildings;
    float[] buildWidths;
    float radius;
    float height;
    float reduction = 10;

    float heightScale = .4f;

	public Block(Vector3 c, float r, GameObject[] b, float[] bw)
    {
        centerPoint = c;
        radius = r - reduction;
        buildings = b;
        height = radius * heightScale;
        buildWidths = bw;

        //makeBox();
        createBuildings();
    }

    void makeBox()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(2*radius, height, 2*radius);
        cube.transform.position = centerPoint + new Vector3(0, height / 2, 0);
    }

    void createBuildings()
    {
        createBuildingsBetweenPoints(centerPoint + new Vector3(-radius, 0, radius), centerPoint + new Vector3(radius, 0, radius), new Vector3(1, 0, 0), 
                                                                 Quaternion.Euler(0, 0, 0));
        createBuildingsBetweenPoints(centerPoint + new Vector3(radius, 0, radius), centerPoint + new Vector3(radius, 0, -radius), new Vector3(0, 0, -1), 
                                                                 Quaternion.Euler(0,90,0));
        createBuildingsBetweenPoints(centerPoint + new Vector3(-radius, 0, -radius), centerPoint + new Vector3(radius, 0, -radius), new Vector3(0, 0, 1), 
                                                                 Quaternion.Euler(0,-90,0));
        createBuildingsBetweenPoints(centerPoint + new Vector3(radius, 0, -radius), centerPoint + new Vector3(-radius, 0, -radius), new Vector3(-1, 0, 0),
                                                                 Quaternion.Euler(0, 180, 0));
    }

    void createBuildingsBetweenPoints(Vector3 start, Vector3 end, Vector3 direction, Quaternion normal)
    {
        Vector3 currPos = start;
        float totalDist = Vector3.Magnitude(start - end);
        
        while (Vector3.Magnitude(currPos - start) < totalDist)
        {
            int index = Random.Range(0, buildings.Length);
            GameObject build = GameObject.Instantiate(buildings[index], currPos, normal) as GameObject;
            currPos += new Vector3(buildWidths[index] * direction.x, 
                                   buildWidths[index] * direction.y, buildWidths[index] * direction.z);
        }
    }
}
