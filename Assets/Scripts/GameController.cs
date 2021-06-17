using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    public Transform playerPosition;

    [Header("Ghost")]
    public Transform posToLerpGhost;
    public GameObject[] ghostPrefabs;
    public List<GameObject> ghostsPool;
    public List<Ghost.GhostMovement> spawnedGhosts;
    [Header("Score")]
    public int ghostsSucked;

    private IEnumerator _spawningCoroutine;

    void Awake()
    {
        _instance = this;
        Init();
    }

    void Start()
    {
        StartGame();
    }


    public void GhostSucked(Ghost.GhostMovement ghost)
    {
        // print("u sucked a ghost");
        ghostsSucked++;
        spawnedGhosts.Remove(ghost);
        ghost.gameObject.SetActive(false);
    }

    private void Init()
    {
        for (int i = 0; i < ghostPrefabs.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject obj = Instantiate(ghostPrefabs[i]);
                obj.SetActive(false);
                ghostsPool.Add(obj);
            }
        }
    }

    public void StartGame()
    {
        if (_spawningCoroutine != null)
        {
            StopCoroutine(_spawningCoroutine);
        }
        _spawningCoroutine = SpawningCoroutine();
        StartCoroutine(_spawningCoroutine);
    }

    private IEnumerator SpawningCoroutine()
    {
        for (int i = 0; i < Utils.Constants.SPAWN_COUNT; i++)
        {
            SpawnGhost();
            yield return new WaitForSeconds(Utils.Constants.SPAWNING_PERIOD);
        }

        _spawningCoroutine = null;
        print("GAME FINISHED");
    }
    public void SpawnGhost()
    {
        Ghost.GhostMovement obj = GetFreeGhost().GetComponent<Ghost.GhostMovement>();
        spawnedGhosts.Add(obj);
        obj.transform.position = GetRandomPosToSpawn();
        obj.MoveGhost();
        obj.gameObject.SetActive(true);
    }

    private Vector3 GetRandomPosToSpawn()
    {
        int r = Random.Range(0, 4);
        float x = 14;
        float y = 8;
        float z = -8;
        switch (r)
        {
            case 0:
                return new Vector3(GameController.Instance.playerPosition.position.x - x, Random.Range(-y, y), z);
            case 1:
                return new Vector3(GameController.Instance.playerPosition.position.x + x, Random.Range(-y, y), z);
            case 2:
                return new Vector3(Random.Range(-x, x), GameController.Instance.playerPosition.position.y + y, z);
            default:
                return new Vector3(Random.Range(-x, x), GameController.Instance.playerPosition.position.y - y, z);
        }
    }

    private GameObject GetFreeGhost()
    {
        var ghosts = new List<GameObject>();

        for (int i = 0; i < ghostsPool.Count; i++)
        {
            if (!ghostsPool[i].activeSelf)
            {
                ghosts.Add(ghostsPool[i]);
            }
        }

        if (ghosts.Count < 1)
        {
            GameObject obj = Instantiate(ghostPrefabs[0]);
            obj.SetActive(false);
            ghostsPool.Add(obj);
            return obj;
        }

        return ghosts[ghosts.Count - 1];
    }
}
