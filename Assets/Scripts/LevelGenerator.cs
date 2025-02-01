using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    private const int PYRAMID_COUNT = 4;
    private int pyramidHeight;
    private int pyramidBase;

    void Start()
    {
       pyramidHeight = 7;
       pyramidBase = pyramidHeight * 2 - 1;

        for (int x = 0; x < PYRAMID_COUNT; x++)
        {
            for (int z = 0; z < PYRAMID_COUNT; z++)
            {


                CreatePyramid(new Vector3(x * pyramidBase, 0,z * pyramidBase));
            }
        }
    }

    void CreatePyramid(Vector3 pos)
    {
        int ox = 0, oz = 0;

        for (int y = 0; y < pyramidHeight; y++)
        {
            for (int x = 0 + ox; x < pyramidBase - ox; x++)
            {
                for (int z = 0 + oz; z < pyramidBase - oz; z++)
                {
                    Vector3 cubePosition = new Vector3(pos.x + x, y, pos.z + z);
                    GameObject tempObj = Instantiate(cube, cubePosition, Quaternion.identity);

                    Color cl = Random.ColorHSV();
                    tempObj.GetComponent<MeshRenderer>().material.color = cl;


                }
            }

            ox++;
            oz++;
        }
    }


}
