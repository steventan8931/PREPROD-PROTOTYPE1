using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playermask;
    public FieldOfView enemyFOV;
    [Range(1,2)]
    public int EnemyType;

    public float speed;
    //var for patrol
    public Vector3 patrolPoint;
    public bool isPatrolPointSet;
    public float patrolPointRange;
    //var for idle
    public float idleTime = 2.5f;
    float currIdleTime = 0f;
    public bool isIdling = false;
    //var for attack
    public float attackCD;
    bool isAttacked;

    //state
    public float attackRange;
    public bool isPlayerInSight, isPlayerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }
    void Update()
    {
        //check if player is in sight or in attackRange
        isPlayerInSight = enemyFOV.canSeePlayer;
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playermask);

        if (!isPlayerInSight && !isPlayerInAttackRange)
        {
            Patroling();
        }
        if (isPlayerInSight && !isPlayerInAttackRange)
        {
            Chasing();
        }
        if (isPlayerInAttackRange)
        {
            Attacking();
        }
    }
    private void Patroling()
    {
        if(!isPatrolPointSet)
        {
            SearchPatrolPoint();
        }

        if(isPatrolPointSet)
        {
            agent.SetDestination(patrolPoint);

        }

        Vector3 distanceToPatrolPoint = transform.position - patrolPoint;

        //arrive patrol point
        if (distanceToPatrolPoint.magnitude < 1f)
        {
            Idling();
            if (isIdling == false)
            {
                isPatrolPointSet = false;
            }
        }
    }

    private void SearchPatrolPoint()
    {
        //Generate random patrol point in range
        float randomZ = Random.Range(-patrolPointRange, patrolPointRange);
        float randomX = Random.Range(-patrolPointRange, patrolPointRange);

        patrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(patrolPoint,-transform.up,2f,groundMask))
        {
            isPatrolPointSet = true;
        }
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!isAttacked)
        {
            // attack player

            isAttacked = true;
            Invoke(nameof(ResetAttack), attackCD);
        }
    }
    private void Idling()
    {
        if(isIdling == false)
        {
            isIdling = true;
        }
        if(isIdling == true && currIdleTime == 0)
        {
            currIdleTime = idleTime;
        }else if(isIdling == true && currIdleTime >0)
        {
            currIdleTime -= Time.deltaTime;
            if(currIdleTime <=0)
            {
                currIdleTime = 0;
                isIdling = false;
            }
        }
    }
    private void ResetAttack()
    {
        isAttacked = false;
    }






    private void OnDrawGizmosSelected()
    {
        //draw attackRange
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    // Update is called once per frame

}
