using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class FruitsCollector : MonoBehaviour
{
    public static event EventHandler AllFruitsCollected;
    public static event EventHandler<Vector2Int> FruitSuccessfullyEaten;
    
    private static int _collectedFruits;
    private static int _amountOfFruitsToCollect;

    public static int CollectedFruits => _collectedFruits;
    public static int AmountOfFruitsToCollect => _amountOfFruitsToCollect;
    
    public void SetNewTarget(int amountOfFruitsToCollect)
    {
        _amountOfFruitsToCollect = amountOfFruitsToCollect;
        _collectedFruits = 0;
    }

    private void OnEnable()
    {
        SnakeMovement.OnSnakeHitFruit +=  SnakeMovementOnOnSnakeHitFruit;
    }

    private void SnakeMovementOnOnSnakeHitFruit(object sender, Vector2Int position)
    {
        _collectedFruits++;

        if (_collectedFruits >= _amountOfFruitsToCollect)
        {
            AllFruitsCollected?.Invoke(this, EventArgs.Empty);
        }
        
        FruitSuccessfullyEaten?.Invoke(this, position);
    }
    
    private void OnDisable()
    {
        SnakeMovement.OnSnakeHitFruit -=  SnakeMovementOnOnSnakeHitFruit;
    }

}
