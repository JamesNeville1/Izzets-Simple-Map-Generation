using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;

public class SCR_generate_map : MonoBehaviour {

    [Header("Require Dev Input")]

    [Tooltip("Tile needed to generate")][SerializeField]
    private GameObject tilePrefab;

    [Tooltip("Height of grid")][SerializeField]
    private int height;

    [Tooltip("Width of grid")][SerializeField]
    private int width;

    [Tooltip("Number of iterations while generating")][SerializeField]
    private int iterations;

    [Tooltip("Should start in centre? If true begin in centre, if false begin with random tile")][SerializeField]
    private bool startInCentre;

    //Hold entire map in dictionary, easy way of finding near tiles from base tile
    private Dictionary<Vector2, GameObject> mapData = new Dictionary<Vector2, GameObject>();

    private void Start() {
        #region Generate
        //Set Up, just a blank grid
        for (int x = 1; x < width + 1; x++) {
            for (int y = 1; y < height + 1; y++) {
                Vector2 tilePos = new Vector2(x, y);
                GameObject tile = Instantiate(tilePrefab, tilePos, Quaternion.identity);
                tile.name = tilePrefab.name + tilePos;
                tile.transform.parent = transform;
                mapData.Add(tilePos, tile);
            }
        }

        //Generate Map, iterate until completed
        Vector2 currentPos;
        if(startInCentre) currentPos = returnCentre();
        else currentPos = returnRandomTile();

        mapData[currentPos].GetComponent<SpriteRenderer>().color = Color.red;
        for (int i = 0; i < iterations; i++) {
            Vector2 dir = returnRandomDir();
            if (mapData.ContainsKey(currentPos + dir))
                currentPos = currentPos + dir;
            else {
                currentPos = returnRandomTile();
            }
            mapData[currentPos].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        #endregion
        #region Set Camera Pos
        Camera.main.transform.position = new Vector3(
            mapData[returnCentre()].gameObject.transform.position.x,
            mapData[returnCentre()].gameObject.transform.position.y,
            -10
        );
        #endregion
    }

    private Vector2 returnCentre() {
        Vector2 v = mapData.Keys.Last();
        v.x = MathF.Round((v.x / 2));
        v.y = MathF.Round((v.y / 2));
        return v;
    }

    private Vector2 returnRandomTile() {
        Vector2 v = mapData.ElementAt(UnityEngine.Random.Range(0, mapData.Count)).Key;
        return v;
    }

    private Vector2 returnRandomDir() {
        int i = UnityEngine.Random.Range(1, 5);
        switch (i) {
            case 1: return Vector2.left;
            case 2: return Vector2.right;
            case 3: return Vector2.up;
            case 4: return Vector2.down;
        }
        return Vector2.left;
    }
}
