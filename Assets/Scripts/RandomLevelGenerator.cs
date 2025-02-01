using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelGenerator : MonoBehaviour
{
    public GameObject grass, ground;

    private int baseHeight = 2,
            maxBlockY = 10,
            chunkSize = 16,
            chunkCount = 10;

    private int seedX, seedY;

    private void Start()
    {
        seedX = Random.Range(0, 20);
        seedY = Random.Range(0, 20);
        for(int x = 0; x < chunkCount; x++)
        {
            for (int z = 0; z < chunkCount; z++)
            {
                createChunk(x, z);
            }
        }
    }

    void createChunk(int cx, int cz)
    {
        GameObject chunk = new GameObject();
        float X = cx * chunkSize + chunkSize/2;
        float Z = cz * chunkSize + chunkSize / 2;
        chunk.transform.position = new Vector3(X, 0f, Z);
        chunk.name = "Chunk " + Z;
        chunk.AddComponent<Chunk>();
        chunk.AddComponent<MeshRenderer>();


        for (int x = cx * chunkSize;
            x< cx * chunkSize + chunkSize; x++)
        {
            for (int z = cz * chunkSize;
            z < cz * chunkSize + chunkSize; z++)
            {

                float xp = seedX + (float)x / 30;
                float yp = seedY + (float)z / 30;

                float perlin = Mathf.PerlinNoise(xp, yp);

                int h = baseHeight + (int)(perlin * maxBlockY);

                for(int y=0; y< h ; y++)
                {
                    GameObject temp =  Instantiate(ground, new Vector3(x, y, z),Quaternion.identity);

                    temp.transform.SetParent(chunk.transform);
                }
                GameObject t = Instantiate(grass, new Vector3(x, h, z), Quaternion.identity);

                t.transform.SetParent(chunk.transform);
            }
        }







    }

}
