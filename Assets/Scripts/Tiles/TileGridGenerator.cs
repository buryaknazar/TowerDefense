using System.Collections.Generic;
using Enemy;
using Player;
using Tiles;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TileGridGenerator : EditorWindow
{
    [SerializeField] private int _sizeX = 5;
    [SerializeField] private int _sizeZ = 5;
    [SerializeField] private TileToPlace _tileToPlace;
    [SerializeField] private Road _roadTileCorner;
    [SerializeField] private Road _roadTile;
    [SerializeField] private EnemyBase _enemyBase;
    [SerializeField] private PlayerBase _playerBase;

    [MenuItem("Tools/Tile Grid Generator")]
    public static void ShowWindow()
    {
        GetWindow<TileGridGenerator>("Tile Grid Generator");
    }

    private void OnGUI()
    {
        _sizeX = EditorGUILayout.IntField("Size X", _sizeX);
        _sizeZ = EditorGUILayout.IntField("Size Z", _sizeZ);
        _tileToPlace = EditorGUILayout.ObjectField("Tile To Place", _tileToPlace, typeof(TileToPlace), false) as TileToPlace;
        _roadTileCorner = EditorGUILayout.ObjectField("Road Tile Corner", _roadTileCorner, typeof(Road), false) as Road;
        _roadTile = EditorGUILayout.ObjectField("Road Tile", _roadTile, typeof(Road), false) as Road;
        _enemyBase = EditorGUILayout.ObjectField("Enemy Base", _enemyBase, typeof(EnemyBase), false) as EnemyBase;
        _playerBase = EditorGUILayout.ObjectField("Player Base", _playerBase, typeof(PlayerBase), false) as PlayerBase;

        if (GUILayout.Button("Generate Grid"))
        {
            PlaceTiles();
        }
    }

    private void PlaceTiles()
    {
        if (_tileToPlace == null || _roadTile == null)
        {
            Debug.LogError("Tile to place or road tile is null");
            return;
        }

        var parent = new GameObject("Tile Grid");
        var innerZ = _sizeZ - 1;
        
        var startZ = Random.Range(-innerZ, innerZ + 1);
        var startPos = new Vector3(-_sizeX, 0, startZ);
        var startRoad = (Road)PrefabUtility.InstantiatePrefab(_roadTile, parent.transform);
        startRoad.name = "StartRoad";
        startRoad.transform.position = startPos;
        startRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
        
        var enemyBase = (EnemyBase)PrefabUtility.InstantiatePrefab(_enemyBase);
        enemyBase.transform.position = new Vector3(-_sizeX - 1, 0, startZ);
        
        var finishZ = Random.Range(-innerZ, innerZ + 1);
        var finishPos = new Vector3(_sizeX, 0, finishZ);
        var finishRoad = (Road)PrefabUtility.InstantiatePrefab(_roadTile, parent.transform);
        finishRoad.name = "FinishRoad";
        finishRoad.transform.position = finishPos;
        finishRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
        
        var playerBase = (PlayerBase)PrefabUtility.InstantiatePrefab(_playerBase);
        playerBase.transform.position = new Vector3(_sizeX + 1, 0, finishZ);
        
        HashSet<int> usedTilesZ = new HashSet<int>();
        
        var innerHalfX = _sizeX - 1 / 2;

        Vector3 leftRandomPos;
        do
        {
            var randX = Random.Range(-_sizeX + 1, -_sizeX + 1 + innerHalfX);
            var randZ = Random.Range(-innerZ, innerZ + 1);
            leftRandomPos = new Vector3(randX, 0, randZ);
        } while (!usedTilesZ.Add((int)leftRandomPos.z));
        
        Vector3 rightRandomPos;
        do
        {
            var randX = Random.Range(-_sizeX + 1 + innerHalfX, _sizeX);
            var randZ = Random.Range(-innerZ, innerZ + 1);
            rightRandomPos = new Vector3(randX, 0, randZ);
        } while (!usedTilesZ.Add((int)rightRandomPos.z));
        
        ConnectTiles(startPos, leftRandomPos, parent);
        ConnectTiles(leftRandomPos, rightRandomPos, parent);
        
        var preFinish = new Vector3(_sizeX - 1, 0, finishZ);
        
        ConnectTiles(rightRandomPos, preFinish, parent);
    }

    private void ConnectTiles(Vector3 from, Vector3 to, GameObject parent)
    {
        var current = from;

        while (Mathf.Abs(current.x - to.x) > 0.01f)
        {
            current.x += current.x < to.x ? 1 : -1;
            
            if (current.x == to.x && current.z != to.z)
            {
                var connectionRoadCorner = (Road)PrefabUtility.InstantiatePrefab(_roadTileCorner, parent.transform);
                connectionRoadCorner.name = "ConnectionRoadCorner";
                connectionRoadCorner.transform.position = current;
                
                Vector3 tileToPlacePos = new Vector3(0, 0, 0);
                
                if (current.z > to.z)
                {
                    connectionRoadCorner.transform.rotation = Quaternion.Euler(0, -90, 0);
                    tileToPlacePos = new Vector3(current.x + 1, 0, current.z + 1);
                }
                else if (current.z < to.z)
                {
                    connectionRoadCorner.transform.rotation = Quaternion.Euler(0, 0, 0);
                    tileToPlacePos = new Vector3(current.x + 1, 0, current.z - 1);
                }
                
                PlaceTileToPlace(parent, tileToPlacePos);
                
                break;
            }
            
            var connectionRoadX = (Road)PrefabUtility.InstantiatePrefab(_roadTile, parent.transform);
            connectionRoadX.name = "ConnectionRoadX";
            connectionRoadX.transform.position = current;
            connectionRoadX.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        
        while (Mathf.Abs(current.z - to.z) > 0.01f)
        {
            var last = current;
            current.z += current.z < to.z ? 1 : -1;
            
            if (current.z == to.z)
            {
                var connectionRoadCorner = (Road)PrefabUtility.InstantiatePrefab(_roadTileCorner, parent.transform);
                connectionRoadCorner.name = "ConnectionRoadCorner";
                connectionRoadCorner.transform.position = current;
                
                Vector3 tileToPlacePos = new Vector3(0, 0, 0);
                
                if (current.z > last.z)
                {
                    Debug.Log("Current Z:" + current.x + ", Last Z:" + last.x + " Rotate 180");
                    connectionRoadCorner.transform.rotation = Quaternion.Euler(0, 180, 0);
                    tileToPlacePos = new Vector3(current.x - 1, 0, current.z + 1);
                }
                else if(current.z < last.z)
                {
                    Debug.Log("Current Z:" + current.x + ", Last Z:" + last.x + " Rotate 90");
                    connectionRoadCorner.transform.rotation = Quaternion.Euler(0, 90, 0);
                    tileToPlacePos = new Vector3(current.x - 1, 0, current.z - 1);
                }
                
                PlaceTileToPlace(parent, tileToPlacePos);

                break;
            }
            
            var connectionRoadZ = (Road)PrefabUtility.InstantiatePrefab(_roadTile, parent.transform);
            connectionRoadZ.name = "ConnectionRoadZ";
            connectionRoadZ.transform.position = current;
            connectionRoadZ.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void PlaceTileToPlace(GameObject parent, Vector3 position)
    {
        var tileToPlace = (Tile)PrefabUtility.InstantiatePrefab(_tileToPlace, parent.transform);
        tileToPlace.transform.position = position;
    }
}
