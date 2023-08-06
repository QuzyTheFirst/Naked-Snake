using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruit : MonoBehaviour
{
    float time;
    [SerializeField] GameObject Apple;
    [SerializeField] GameObject It;
    GameObject[] grids;
    SnakeMovement snake;
    int children;
    GameObject MyApple;
    //Vector3.Distance(MyApple.transform.position, It.transform.position) < 0.3f
    void Start()
    {
        children = transform.childCount;
        snake = GameObject.Find("SnakeHead").GetComponent<SnakeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyApple == null && snake.State == SnakeMovement.state.alive)
        {
            SpawnApple();
        }
    }

    private void SpawnApple()
    {
        int ChildForAppleSpawn = Random.Range(0, children);
        Vector3 GridForSpawnPosition = transform.GetChild(ChildForAppleSpawn).localPosition;
        Vector3 AppleSpawnPosition = GridForSpawnPosition + new Vector3(0f, 1f, 0f);
        MyApple = Instantiate(Apple, AppleSpawnPosition, Quaternion.identity);
    }
}
