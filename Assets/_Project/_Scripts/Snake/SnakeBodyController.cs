using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{
      [SerializeField] private Transform _snakeBodyPf;

      private List<Transform> _spawnedSnakeBodies;

      public Transform SpawnNewBody()
      {
            Transform body = Instantiate(_snakeBodyPf);
            _spawnedSnakeBodies.Add(body);
            return body;
      }
}
