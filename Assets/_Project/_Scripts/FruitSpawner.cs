using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _fruitPf;

    private GridsManipulator _gridsManipulator;

    public void Initialize(GridsManipulator gridsManipulator)
    {
        _gridsManipulator = gridsManipulator;
    }
    
    public void SpawnFruit()
    {
        List<GridTile> tiles = null;/*_gridsManipulator.FindAllWalkableTilesWithoutSnakesAndFruits();*/
        if (tiles.Count == 0)
            return;
        
        int randomNumber = Random.Range(0, tiles.Count);
        GridTile tile = tiles[randomNumber];

        if (tile == null) 
            throw new Exception("Walkable Tile Wasn't found");
        
        Vector3 spawnPos = new Vector3(tile.X, 0, tile.Y) + Vector3.up * 0.25f;
        Transform fruit = Instantiate(_fruitPf, spawnPos, _fruitPf.rotation,transform);

        GridTransformItem item = new GridTransformItem();
        item.SetItem(tile.X, tile.Y, fruit);
        //_gridsManipulator.GridItems.TrySetTile(item);
    }

    public void DeleteAllFruits()
    {
        foreach (Transform fruit in transform)
        {
            Destroy(fruit.gameObject);
        }
    }
    
    private void OnEnable()
    {
        SnakeMovement.OnSnakeHitFruit += SnakeMovement_OnSnakeHitFruit;
    }

    private void SnakeMovement_OnSnakeHitFruit(object sender, Vector2Int fruitPos)
    {
        GridTransformItem gridItem = null; //_gridsManipulator.GridItems.TryGetTile(fruitPos.x, fruitPos.y);

        if (gridItem == null)
            return;

        Transform item = gridItem.Value;

        /*if (_gridsManipulator.GridItems.TryClearTile(fruitPos.x, fruitPos.y) && item != null)
        {
            Destroy(item.gameObject);
        }*/

        SpawnFruit();
    }

    private void OnDisable()
    {
        SnakeMovement.OnSnakeHitFruit -= SnakeMovement_OnSnakeHitFruit;
    }
}
