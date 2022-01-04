using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _SIZE_X;
    [SerializeField] private int _SIZE_Y;
    
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Tile _tile;
    [SerializeField] private Color _color;
    [SerializeField] [Range(1, 10)] private int _speed = 2;

    private bool[,] _area;

    private void Start()
    {
        /*TileBase[] tileArray = new TileBase[_SIZE_X * _SIZE_Y];
        
        for (int index = 0; index < tileArray.Length; index++)
        {
            tileArray[index] = _tile;
        }

        _tilemap.SetTilesBlock(new BoundsInt(0, 0, 0, _SIZE_X, _SIZE_Y, 0), tileArray);*/

        _area = new bool[_SIZE_X, _SIZE_Y];

        for (var x = 0; x < _SIZE_X; x++)
        {
            for (var y = 0; y < _SIZE_Y; y++)
            {
                _area[x, y] = Random.Range(1, 5) == 1;
                
                var positions = new Vector3Int(x, y, 0);
                
                _tilemap.SetTile(positions, _tile);

                _tilemap.SetTileFlags(positions, TileFlags.None);
                
                _tilemap.SetColor(new Vector3Int(x, y, 0), _area[x, y] ? _color : Color.white);
            }
        }


        var stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        
        Debug.Log(Screen.width + " | " + Screen.height);
        
        Debug.Log(stageDimensions.x);
        Debug.Log(stageDimensions.y);
        
        _tilemap.transform.position = new Vector3(-12, -13, 0);
        
        StartCoroutine(Render());
    }

    private void Update()
    {
        /*if (timer)
        {
            
        }
        
        for (var x = 0; x < _SIZE_X; x++)
        {
            for (var y = 0; y < _SIZE_Y; y++)
            {
                _tilemap.SetColor(new Vector3Int(x, y, 0), _area[x, y] ? _color : Color.white);
            }
        }

        Recalculate();*/
        
        

    }

    private IEnumerator Render()
    {
        while (true)
        {
            Recalculate();

            for (var x = 0; x < _SIZE_X; x++)
            {
                for (var y = 0; y < _SIZE_Y; y++)
                {
                    _tilemap.SetColor(new Vector3Int(x, y, 0), _area[x, y] ? _color : Color.white);
                }
            }
            
            yield return new WaitForSeconds(_speed * .1f);
        }
    }

    private void Recalculate()
    {
        var oldArea = new bool[_SIZE_X, _SIZE_Y];

        System.Array.Copy(_area, oldArea, _area.Length);

        /*Debug.Log(test._area[0, 0]);        
        Debug.Log(_area[0, 0]);*/   
        
        for (var x = 0; x < _SIZE_X; x++)
        {
            for (var y = 0; y < _SIZE_Y; y++)
            {
                var numNeighbors = 0;
                
                /*
                 * [ ][x][ ]
                 * [ ][-][ ]
                 * [ ][ ][ ]
                 */
                if (y + 1 < _SIZE_Y)
                {
                    if (_area[x, y + 1])
                    {
                        numNeighbors++;
                    }
                }

                /*
                 * [ ][ ][x]
                 * [ ][-][ ]
                 * [ ][ ][ ]
                */
                if (x + 1 < _SIZE_X && y + 1 < _SIZE_Y)
                {
                    if (_area[x + 1, y + 1])
                    {
                        numNeighbors++;
                    }
                }
                
                /*
                 * [ ][ ][ ]
                 * [ ][-][x]
                 * [ ][ ][ ]
                */
                if (x + 1 < _SIZE_X)
                {
                    if (_area[x + 1, y])
                    {
                        numNeighbors++;
                    }
                }
                
                /*
                 * [ ][ ][ ]
                 * [ ][-][ ]
                 * [ ][ ][x]
                */
                if (x + 1 < _SIZE_X && y - 1 >= 0)
                {
                    if (_area[x + 1, y - 1])
                    {
                        numNeighbors++;
                    }
                }
                
                /*
                 * [ ][ ][ ]
                 * [ ][-][ ]
                 * [ ][x][ ]
                */
                if (y - 1 >= 0)
                {
                    if (_area[x, y - 1])
                    {
                        numNeighbors++;
                    }
                }
                
                /*
                 * [ ][ ][ ]
                 * [ ][-][ ]
                 * [x][ ][ ]
                */
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    if (_area[x - 1, y - 1])
                    {
                        numNeighbors++;
                    }
                }
                
                /*
                 * [ ][ ][ ]
                 * [x][-][ ]
                 * [ ][ ][ ]
                */
                if (x - 1 >= 0)
                {
                    if (_area[x - 1, y])
                    {
                        numNeighbors++;
                    }
                }
                
                /*
                 * [x][ ][ ]
                 * [ ][-][ ]
                 * [ ][ ][ ]
                */
                if (x - 1 >= 0 && y + 1 < _SIZE_Y)
                {
                    if (_area[x - 1, y + 1])
                    {
                        numNeighbors++;
                    }
                }
                
                if (_area[x, y])
                {
                    if (numNeighbors != 2 && numNeighbors != 3)
                    {
                        oldArea[x, y] = false;
                    }
                }
                else
                {
                    if (numNeighbors == 3)
                    {
                        oldArea[x, y] = true;
                    }
                }
                
                //Debug.Log(x + "x" + y + " - " + numNeighbors + " | " + oldArea[x, y]);
            }
        }

        _area = oldArea;
    }
}
