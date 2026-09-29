using Assets.Logic.Scripts.Wrappers;
using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private LayerMask whatIsPlayer;

    private Animator animator;

    private AudioSource audioSource;

    private NavMeshAgent agent;

    private float speed = GameInfo.Instance.EnemyWalkSpeed;
    
    private float runSpeed = GameInfo.Instance.EnemyRunSpeed;

    private float sightRange = GameInfo.Instance.SightRange;

    private bool playerInSightMesh;

    private Vector3 walkPoint;

    private bool walkPointIsSet;

    private bool isCoroutineEnd;

    private ArrayWalkPointWrapper arrayWalkPointWrapper = new ArrayWalkPointWrapper(29);

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        arrayWalkPointWrapper.ReadJson("walkPoints.json");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Moving", 1);
        playerInSightMesh = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);


        if (playerInSightMesh)
        {
            ChasePlayer();
        }

        else
            Patroling();
    }

    private void Patroling()
    {
        audioSource.pitch = 1.2f;
        agent.speed = speed;

        if (!walkPointIsSet)
        {
            audioSource.Pause();

            animator.SetFloat("Moving", 0);

            StartCoroutine(Wait());

            if (isCoroutineEnd)
            {
                SetWalkPoint();
                isCoroutineEnd = false;
            }
        }
        else
        {
            agent.SetDestination(walkPoint);

            animator.SetFloat("Moving", 1);
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        var enemyPosition = transform.position;
        enemyPosition.y = 0;

        if (Vector3.Distance(walkPoint, enemyPosition) < 1)
        {
            walkPointIsSet = false;
        }
    }

    private void ChasePlayer()
    {
        agent.speed = runSpeed;
        audioSource.pitch = 1.5f;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        transform.LookAt(player);

        agent.SetDestination(player.position);

        animator.SetFloat("Moving", 2);
    }

    private void SetWalkPoint()
    {
        animator.SetFloat("Moving", 1);
        int walkPointIndex = Random.Range(0, arrayWalkPointWrapper.WalkPoints.Length - 1);

        walkPoint.x = arrayWalkPointWrapper.WalkPoints[walkPointIndex].x;
        walkPoint.y = 0;
        walkPoint.z = arrayWalkPointWrapper.WalkPoints[walkPointIndex].z;

        walkPointIsSet = true;  
    }

    private IEnumerator Wait()
    {
        animator.SetFloat("Moving", 0);

        yield return new WaitForSecondsRealtime(5);

        isCoroutineEnd = true;
    }
}
