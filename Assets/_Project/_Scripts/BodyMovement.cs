using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    SnakeMovement snake;
    int i;
    SpawnFruit Fruits;
    void Start()
    {
        Fruits = GameObject.Find("GridBoxes").GetComponent<SpawnFruit>();
        snake = GameObject.Find("SnakeHead").GetComponent<SnakeMovement>();
        for(i = 0; i < snake.Bodies.Length; i++)
        {
            if (snake.Bodies[i] == null)
            { snake.Bodies[i] = gameObject;break; }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = snake.PreviousPositions[i];
        transform.rotation = snake.PreviousRotations[i];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Apple")
        {
            Destroy(other.gameObject);
        }
    }       
}
