using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsController : MonoBehaviour
{
    private int _collectedFruits;
    
    private FruitsGrid _fruitsGrid;
    private GridsManipulator _gridsManipulator;

    private List<FruitsGrid.FruitGridObject> _spawnedFruits;

    public static event EventHandler<int> OnCollectedFruitAmountChanged;
    public static event EventHandler SnakeHaveEatenAllPossibleApples;
    
    private int CollectedFruits
    {
        get => _collectedFruits;
        set
        {
            _collectedFruits = value;
            OnCollectedFruitAmountChanged?.Invoke(this, _collectedFruits);
        }
    }
    
    public void Initialize(FruitsGrid fruitsGrid, GridsManipulator gridsManipulator)
    {
        _fruitsGrid = fruitsGrid;
        _gridsManipulator = gridsManipulator;

        _spawnedFruits = new List<FruitsGrid.FruitGridObject>();
    }

    public void SpawnFruit()
    {
        // Get Random Spawnpoint or walkable tile without snake
        MapTilesGrid.MapTileGridObject spawnTile = _gridsManipulator.GetRandomWalkableOrSpawnpointTileWithoutSnake();

        if(spawnTile == null)
        {
            SnakeHaveEatenAllPossibleApples?.Invoke(this, EventArgs.Empty);
            return;
        }

        var tilePos = spawnTile.GetCoordinates();
        FruitsGrid.FruitGridObject fruitTile = _fruitsGrid.GetFruitTile(tilePos.xPos, tilePos.yPos);
        fruitTile.SetTileType(FruitsGrid.FruitGridObject.TileTypeEnum.Fruit);
        
        _spawnedFruits.Add(fruitTile);
    }
    
    private void OnEnable()
    {
        SnakeController.OnSnakeHitFruit += SnakeControllerOnOnSnakeHitFruit;
    }

    private void SnakeControllerOnOnSnakeHitFruit(object sender, Vector2Int pos)
    {
        FruitsGrid.FruitGridObject fruitTile = _fruitsGrid.GetFruitTile(pos.x, pos.y);
        
        _spawnedFruits.Remove(fruitTile);
        fruitTile.ClearTile();

        CollectedFruits++;
        
        SpawnFruit();
    }

    private void OnDisable()
    {
        SnakeController.OnSnakeHitFruit -= SnakeControllerOnOnSnakeHitFruit;
    }
}
