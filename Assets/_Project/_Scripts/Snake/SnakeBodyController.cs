using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{
      [SerializeField] private Transform _snakeBodyPf;

      private GridTile[] _lastSnakeGridTiles;
      
      private List<SnakeBody> _spawnedSnakeBodies;
      
      [SerializeField] private int _amountOfLastPlayerPositionsToSave;


      public IReadOnlyList<SnakeBody> SpawnedSnakeBodies => _spawnedSnakeBodies;

      private void Awake()
      {
            _spawnedSnakeBodies = new List<SnakeBody>();
            _amountOfLastPlayerPositionsToSave = _spawnedSnakeBodies.Count + 1;
            
            _lastSnakeGridTiles = new GridTile[_amountOfLastPlayerPositionsToSave];
      }

      private void PlayerMoved(GridTile from)
      {
            AddLastPositionToArray(from);
            
            MoveSnakeBodies();
      }

      private void AddLastPositionToArray(GridTile tile)
      {
            for (int i = _lastSnakeGridTiles.Length - 2; i >= 0; i--)
            {
                  _lastSnakeGridTiles[i + 1] = _lastSnakeGridTiles[i];
            }

            _lastSnakeGridTiles[0] = tile;
      }
      
      private void MoveSnakeBodies()
      {
            for (int i = 0; i < _spawnedSnakeBodies.Count; i++)
            {
                  SnakeBody body = _spawnedSnakeBodies[i];
                  GridTile tile = _lastSnakeGridTiles[i];
                  
                  body.Transform.position = new Vector3(tile.X, 0, tile.Y) * LevelGenerator.DistanceBetweenTiles;
                  body.SetGridPosition(tile.X,tile.Y);
            }
      } 
      
      private void SpawnNewBody()
      {
            Transform body = Instantiate(_snakeBodyPf);
            GridTile tile = _lastSnakeGridTiles[_lastSnakeGridTiles.Length - 1];
            SnakeBody snakeBody = new SnakeBody(tile.X, tile.Y, body);
            body.position = new Vector3(tile.X, 0, tile.Y) * LevelGenerator.DistanceBetweenTiles + Vector3.up;
            
            _spawnedSnakeBodies.Add(snakeBody);
            _amountOfLastPlayerPositionsToSave = _spawnedSnakeBodies.Count + 1;
            UpdateSavedPositionsCapacity();
      }

      private void UpdateSavedPositionsCapacity()
      {
            GridTile[] tmp = new GridTile[_amountOfLastPlayerPositionsToSave];
            for (int i = 0; i < _lastSnakeGridTiles.Length; i++)
            {
                  tmp[i] = _lastSnakeGridTiles[i];
            }

            _lastSnakeGridTiles = tmp;
      }

      private void OnEnable()
      {
            FruitSpawner.OnFruitEaten += FruitSpawner_OnFruitEaten;
            SnakeMovement.OnSnakeMoved += SnakeMovement_OnSnakeMoved;
      }

      private void FruitSpawner_OnFruitEaten(object sender, EventArgs e)
      {
            SpawnNewBody();
      }

      private void SnakeMovement_OnSnakeMoved(object sender, GridTile from)
      {
            PlayerMoved(from);
      }
      
      private void OnDisable()
      {
            FruitSpawner.OnFruitEaten -= FruitSpawner_OnFruitEaten;
            SnakeMovement.OnSnakeMoved -= SnakeMovement_OnSnakeMoved;
      }
}
