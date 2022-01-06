using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class Demon : MonoBehaviour
{
    Vector3 currentDestination;
    public NavMeshAgent navmesh;
    Player player;
    public DemonState state;
    new Transform transform;
    public float foundRadius = 10;
    public float searchTime = 10;
    public Transform eyes;
    public LayerMask playerAndWallsLayer;
    int wanderCount = 0;
    int wanderCountToSearchPlayer = 3;
    int wanderCountToRunToPlayer = 6;
    List<Transform> wanderSpots;
    public float speedWalk = 5;
    public float speedRun = 10;
    public bool isRunning = false;
    public float catchDistance = 1;
    public int searchIterrations = 5;
    public float searchRadius = 10;
    public float nearSpotDistance = 20;
    Coroutine foundCour;
    [SerializeField] SpriteRenderer stateIndicator;
    bool stunned = false;
    private void Awake()
    {
        transform = gameObject.transform;
        navmesh = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        Manager.instance.demon = this;
        player = Manager.instance.player;
        Manager.instance.OnSound += CheckSound;
        wanderSpots = Manager.instance.wanderSpots;
        SetStatus(DemonState.wander);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < foundRadius)
        {
            RaycastHit hit = new RaycastHit();
            //eyes.LookAt(player.transform);
            //Physics.Raycast(demon.eyes.position, demon.eyes.forward, out hit, demon.foundRadius, demon.playerAndWallsLayer);
            Physics.Raycast(eyes.position, player.transform.position - eyes.position, out hit, foundRadius, playerAndWallsLayer);
            if (hit.collider != null)
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.LogWarning("Found");
                    SetStatus(DemonState.found);
                }
        }
    }
    public void SetNewDestination(Vector3 newDest)
    {
        currentDestination = newDest;
        UpdateNavDestination();
    }
    void UpdateNavDestination()
    {
        navmesh.SetDestination(currentDestination);
    }
    void SetSpeed(bool run)
    {
        if (!stunned)
        {
            isRunning = run;
            if (!isRunning)
            {
                navmesh.speed = speedWalk;
            }
            else
            {
                navmesh.speed = speedRun;
            }
        }
    }
    void CheckSound(SoundArgs arg)
    {
        if(Vector3.Distance(transform.position, arg.soundPos) < arg.loudness)
        {
            SetStatus(DemonState.run);
            SetNewDestination(arg.soundPos);
        }
    }
    public void SetStatus(DemonState newState)
    {
        if (!stunned)
        {
            state = newState;
            switch (state)
            {
                case DemonState.found:
                    wanderCount = 0;
                    Found();
                    stateIndicator.color = Color.red;
                    Debug.LogWarning("State Found");
                    break;
                case DemonState.search:
                    wanderCount = 0;
                    Search();
                    stateIndicator.color = Color.yellow;
                    Debug.LogWarning("State Search");
                    break;
                case DemonState.run:
                    wanderCount = 0;
                    Run();
                    stateIndicator.color = Color.blue;
                    Debug.LogWarning("State Run");
                    break;
                case DemonState.wander:
                    Wander();
                    stateIndicator.color = Color.green;
                    Debug.LogWarning("State Wander");
                    break;
                default:
                    SetStatus(DemonState.wander);
                    break;
            }
        }
    }

    void Found()//change to interruptable
    {
        if(foundCour != null)
        {
            StopCoroutine(foundCour);
        }
        foundCour = StartCoroutine(FoundCour());
    }
    IEnumerator FoundCour()
    {
        SetSpeed(true);
        SetNewDestination(player.transform.position);
        //yield return new WaitUntil(()=> Vector3.Distance(transform.position, currentDestination) > navmesh.stoppingDistance);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => navmesh.velocity.magnitude > 1.15f && Vector3.Distance(transform.position, currentDestination) > navmesh.stoppingDistance * 4);
        SetSpeed(false);
        SetStatus(DemonState.search);
    }
    async void Search()
    {
        Vector3 startSearchPos = transform.position;
        for (int i = 0; i < searchIterrations; i++)
        {
            Debug.Log(i + " iterration of search");
            SetSpeed(false);
            SetNewDestination(startSearchPos + new Vector3(Random.Range(-searchRadius, searchRadius), startSearchPos.y, Random.Range(-searchRadius, searchRadius)).normalized * searchRadius);
            await Task.Delay(100);
            while (navmesh.velocity.magnitude > 1.15f && Vector3.Distance(transform.position, currentDestination) > navmesh.stoppingDistance * 4 && state == DemonState.search)
            {
                await Task.Yield();
            }
        }
        if (state == DemonState.search)
            SetStatus(DemonState.wander);
    }
    async void Run()
    {
        await Task.Delay(100);
        while (navmesh.velocity.magnitude > 1.15f && Vector3.Distance(transform.position, currentDestination) > navmesh.stoppingDistance * 4 && state == DemonState.run)
        {
            await Task.Yield();
        }
        if(state == DemonState.run)
        {
            SetStatus(DemonState.search);
        }
    }
    async void Wander()
    {
        while (state == DemonState.wander && wanderCount < 100)
        {
            if (wanderCount < wanderCountToSearchPlayer)
            {
                SetSpeed(false);
                SetNewDestination(GetWanderSpot().position);
            }
            else if (wanderCount < wanderCountToRunToPlayer)
            {
                SetSpeed(false);
                SetNewDestination(player.transform.position);
            }
            else
            {
                SetSpeed(true);
                SetNewDestination(player.transform.position);
            }
            wanderCount++;
            Debug.Log(wanderCount + "wand.count");
            while (Vector3.Distance(transform.position, currentDestination) > navmesh.stoppingDistance && state == DemonState.wander && wanderCount < 100)
            {
                //Vector3.Distance(transform.position, currentDestination) > catchDistance 
                await Task.Yield();
            }
        }
    }
    Transform GetWanderSpot()
    {
        Transform destination;
        do
        {
            destination = wanderSpots[Random.Range(0, wanderSpots.Count)];
        } while (Vector3.Distance(new Vector3(destination.position.x, transform.position.y, destination.position.z), transform.position) < nearSpotDistance);
        return destination;
    }
    public enum DemonState
    {
        found //found player and runs towards him
        ,search //just lost player and tries to find him
        ,run //runs towards important clue(loud sound etc.)
        ,wander //desn't know where the palyer is, so he is just checking some importants
    }
    public async void Stun(float amount)
    {
        navmesh.speed = 0;
        stunned = true;
        await Task.Delay((int)(amount * 1000));
        SetSpeed(isRunning);
    }
}

