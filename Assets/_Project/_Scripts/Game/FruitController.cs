using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    private FruitsGrid _fruitsGrid;
    private GridsManipulator _gridsManipulator;
    
    public void Initialize(FruitsGrid fruitsGrid, GridsManipulator gridsManipulator)
    {
        _fruitsGrid = fruitsGrid;
        _gridsManipulator = gridsManipulator;
    }

    public void SpawnFruit()
    {
        // Get Random Spawnpoint or walkable tile without snake
        MapTilesGrid.MapTileGridObject spawnTile = _gridsManipulator.GetRandomWalkableOrSpawnpointTileWithoutSnake();
        var tilePos = spawnTile.GetCoordinates();
        FruitsGrid.FruitGridObject fruitTile = _fruitsGrid.GetFruitTile(tilePos.xPos, tilePos.yPos);
        fruitTile.SetTileType(FruitsGrid.FruitGridObject.TileTypeEnum.Fruit);
    }
    
    private void OnEnable()
    {
        SnakeController.OnSnakeHitFruit += SnakeControllerOnOnSnakeHitFruit;
    }

    private void SnakeControllerOnOnSnakeHitFruit(object sender, EventArgs e)
    {
        Debug.Log("Snake has eaten fruit!");
    }

    private void OnDisable()
    {
        SnakeController.OnSnakeHitFruit -= SnakeControllerOnOnSnakeHitFruit;
    }
}
