using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseGen : MonoBehaviour
{
  public int width = 100, height = 100;
  public Tilemap Terrain, Blockables, Unwalkable, LargeObjects, SmallDecals;
  public List<float> octaves, weights;
  private float[,] _data, _data2;
  public List<TileBase> tiles;
  public List<float> tileThresholds;
    // Start is called before the first frame update
    void Start()
    {
        if (octaves.Count > weights.Count) return;
        _data = new float[width, height];
        //Zero out all of our data
        for (int i = 0; i < width; i++)
        {
        for (int j = 0; j < height; j++)
        {
            _data[i, j] = 0;
        }
        }
        //For each octave
        for(int z = 0; z < octaves.Count; z++)
        {
        //Calculate a noise map
        _data2 = SimplexNoise.Noise.Calc2D(width, height, octaves[z]);
        //Then apply it to our terrain data by weight
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            _data[i,j] += _data2[i,j] * weights[z];
        }
        int tileIndex = 0;
        for (int i = 0; i < width; i++)
        {
        for (int j = 0; j < height; j++)
        {
            //_data[i,j] = 0-255
            for(int k = 0; k < tileThresholds.Count; k++)
            {
                if (_data[i, j] > tileThresholds[k]) continue;
                else { tileIndex = k; break; }
            }
            //if (tileIndex > 0)//todo: put in logic to let the user determine if a particular block is blockable or not
            {
                Terrain.SetTile(new Vector3Int(i-width/2, j-height/2, 0), tiles[tileIndex]);
                Terrain.SetTileFlags(new Vector3Int(i-width/2, j-height/2, 0), TileFlags.None);
                //Terrain.SetColor(new Vector3Int(i, j, 0), new Color((_data[i, j] / 255f), (_data[i, j] / 255f), (_data[i, j] / 255f)));
            }
            /*else
            {
                Unwalkable.SetTile(new Vector3Int(i, j, 0), tiles[tileIndex]);
                Unwalkable.SetTileFlags(new Vector3Int(i, j, 0), TileFlags.None);
                //Unwalkable.SetColor(new Vector3Int(i, j, 0), new Color((_data[i, j] / 255f), (_data[i, j] / 255f), (_data[i, j] / 255f)));
            }*/
        }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}