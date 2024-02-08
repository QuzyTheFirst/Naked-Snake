using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SnakePartsVisual : MonoBehaviour
{
    struct ItemToUpdate
    {
        public int X;
        public int Y;
    }

    struct SnakePart
    {
        public Transform Part;
        public int Id;
        public SnakePartsGrid.SnakePartGridObject.TileTypeEnum TileType;
    }
    
    [Header("Snake Parts")]
    [SerializeField] private Transform _snakeHeadPf;
    [SerializeField] private Transform _snakeBodyPf;

    [Header("Transitions")] 
    [SerializeField] private float _positionTransitionTime = 0.2f;

    [SerializeField] private LeanTweenType _positionLeanTweenType;

    [SerializeField] private float _snakeBodyRotationTransitionTime = 0.1f;
    [SerializeField] private float _snakeHeadRotationTransitionTime = 0.05f;
    [SerializeField] private LeanTweenType _rotationLeanTweenType;

    [Header("Explosion")] 
    [SerializeField] private ParticleSystem _explosionParticles;
    
    private Grid<SnakePartsGrid.SnakePartGridObject> _grid;
    private bool _updateSnake = false;
    
    private List<SnakePart> _allSnakeParts;

    private Queue<ItemToUpdate> _visualUpdatesQueue;

    private void Awake()
    {
        _allSnakeParts = new List<SnakePart>();
        _visualUpdatesQueue = new Queue<ItemToUpdate>();
    }
    
    public void SetGrid(Grid<SnakePartsGrid.SnakePartGridObject> grid)
    {
        _grid = grid;

        UpdateLevelEditorVisual();

        _grid.OnGridObjectChanged += OnGridObjectChanged;
    }
    
    private void OnGridObjectChanged(object sender, Grid<SnakePartsGrid.SnakePartGridObject>.OnGridObjectChangedEventArgs e)
    {
        _updateSnake = true;
        _visualUpdatesQueue.Enqueue(new ItemToUpdate(){X = e.x, Y = e.y});
    }
    
    private void LateUpdate()
    {
        if (_updateSnake)
        {
            _updateSnake = false;
            UpdateLevelEditorVisual();
        }
    }
    
    private void UpdateLevelEditorVisual()
    {
        while (_visualUpdatesQueue.Any())
        {
            ItemToUpdate item = _visualUpdatesQueue.Dequeue();

            SnakePartsGrid.SnakePartGridObject tile = _grid.GetGridObject(item.X, item.Y);

            if (tile == null)
                continue;
            
            SnakePartsGrid.SnakePartGridObject.TileTypeEnum tileType = tile.GetTileType();

            if (tileType == SnakePartsGrid.SnakePartGridObject.TileTypeEnum.Empty)
                continue;
            
            if (_allSnakeParts.Any(part => part.Id == tile.ID))
            {
                //We already have this body and we need to just change it position or rotation
                SnakePart snakePart = _allSnakeParts.Single(part => part.Id == tile.ID);

                Vector3 newSnakePosition =
                    new Vector3(item.X, 0.25f, item.Y) * _grid.GetCellSize() + _grid.GetOriginPosition();
                
                LeanTween.move(snakePart.Part.gameObject, newSnakePosition, _positionTransitionTime)
                    .setEase(_positionLeanTweenType);

                if (snakePart.TileType == SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakeHead)
                {
                    LeanTween.rotate(snakePart.Part.gameObject, new Vector3(0, tile.Rotation, 0),
                            _snakeHeadRotationTransitionTime)
                        .setEase(_rotationLeanTweenType);
                }
                else if (snakePart.TileType == SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakePart)
                {
                    LeanTween.rotate(snakePart.Part.gameObject, new Vector3(0, tile.Rotation, 0),
                            _snakeBodyRotationTransitionTime)
                        .setEase(_rotationLeanTweenType);
                }
                
            }
            else
            {
                // We have to instantiate a new body
                Transform objToInstantiate;
                
                
                switch (tileType)
                {
                    case SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakeHead:
                        objToInstantiate = _snakeHeadPf;
                        break;
                    case SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakePart:
                        objToInstantiate = _snakeBodyPf;
                        break;
                    default:
                        continue;
                        break;
                }
                
                SnakePart snakePart = new SnakePart()
                {
                    Part = Instantiate(objToInstantiate,
                        new Vector3(item.X, 0.25f, item.Y) * _grid.GetCellSize() + _grid.GetOriginPosition(),
                        Quaternion.identity),
                    Id = tile.ID,
                    TileType = tileType,
                };
                
                _allSnakeParts.Add(snakePart);
            }
        }
    }
    
    private void DeleteAllSnakes()
    {
        foreach (SnakePart snake in _allSnakeParts)
        {
            Destroy(snake.Part.gameObject);
        }
    }

    private void BlowUpSnake()
    {
        Queue<Rigidbody> rigidBodies = new Queue<Rigidbody>();
        
        foreach (SnakePart snakePart in _allSnakeParts)
        {
            rigidBodies.Enqueue(snakePart.Part.GetComponent<Rigidbody>());
        }
        
        StartCoroutine(Explode(rigidBodies));
    }
    
    public IEnumerator Explode(Queue<Rigidbody> rigidbodies)
    {
        SoundManager.Instance.Play("UhOh");
        yield return new WaitForSeconds(.2f);
        foreach (Rigidbody rig in rigidbodies)
        {
            rig.isKinematic = false;
            rig.useGravity = true;

            float randomXValue = Random.Range(-1f, 1f);
            float randomYValue = Random.Range(0, 1f);
            float randomZValue = Random.Range(-1f, 1f);

            Vector3 randomDirection = (new Vector3(randomXValue * 5f, randomYValue * 10f, randomZValue * 5f)).normalized;
            
            rig.velocity = randomDirection * 10f;
            rig.angularVelocity = new Vector3(randomXValue, randomYValue, randomZValue) * 720f;

            _explosionParticles.transform.position = rig.position;
            _explosionParticles.Play();
            
            SoundManager.Instance.Play("Explosion");
            
            yield return new WaitForSecondsRealtime(.2f);
        }
    }

    private void OnEnable()
    {
        SnakeController.OnSnakeDeath += SnakeControllerOnOnSnakeDeath; 
    }

    private void SnakeControllerOnOnSnakeDeath(object sender, EventArgs e)
    {
        BlowUpSnake();
    }

    private void OnDisable()
    {
        SnakeController.OnSnakeDeath -= SnakeControllerOnOnSnakeDeath;
    }
}
