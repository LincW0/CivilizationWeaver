using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject tilePrefab;
    public Sprite[] sprites;
    public int mapHeight,mapWidth;
    private GameObject[,] tiles;
    public int renderLayer;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[mapWidth*2+1,mapHeight*2+1];
        GenerateMap();    
    }

    void GenerateMap()
    {
        for (int x = -mapWidth; x <= mapWidth; x++)
        {
            for (int y = -mapHeight; y <= mapHeight; y++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, renderLayer), Quaternion.identity);
                tile.GetComponent<SpriteRenderer>().sprite = sprites[0];
                tiles[x+mapWidth,y+mapHeight]=tile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
