using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class FruitsCollector : MonoBehaviour
{
    private int _collectedFruits;

    public static event EventHandler<int> OnCollectedFruitAmountChanged;

    public int CollectedFruits => _collectedFruits;

    public void ResetCollectedFruitsAmount()
    {
        _collectedFruits = 0;
        OnCollectedFruitAmountChanged?.Invoke(this, CollectedFruits);
    }

    private void OnEnable()
    {
        SnakeMovement.OnSnakeHitFruit +=  SnakeMovementOnOnSnakeHitFruit;
    }

    private void SnakeMovementOnOnSnakeHitFruit(object sender, Vector2Int position)
    {
        _collectedFruits++;
        OnCollectedFruitAmountChanged?.Invoke(this, CollectedFruits);
    }
    
    private void OnDisable()
    {
        SnakeMovement.OnSnakeHitFruit -=  SnakeMovementOnOnSnakeHitFruit;
    }

}
