using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playermask;
    public FieldOfView enemyFOV;
    public bool lastFrameInSight = false;
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
    // var for confused
    public float confuseTime = 2.5f;
    public float currConfuseTime = 0f;
    public bool isConfused = false;
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
        if (lastFrameInSight == true && isPlayerInSight == false)
        {
            //lost visual of player
            isConfused = true;
            currConfuseTime = confuseTime;
            Confusing();
            

        }
        else if (isConfused == true)
        {
            //stay in confused state
            Confusing();
        }
        
        else
        {
            if (!isPlayerInSight && !isPlayerInAttackRange)
            {
                Patroling();
            }
            if (isPlayerInSight && !isPlayerInAttackRange)
            {
                currIdleTime = 0;
                Chasing();
            }
            if (isPlayerInAttackRange && isPlayerInSight)
            {
                Attacking();
            }
        }
        //save in sight bool
        lastFrameInSight = isPlayerInSight;
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
            
            if(EnemyType == 1)
            {
                //do melee attack 
                Debug.Log("attacking (melee)");
                // add trigger to attack animation
            }
            if (EnemyType == 2)
            {
                //do ranged attack
                //add trigger to attack animation
            }
            isAttacked = true;
            Invoke(nameof(ResetAttack), attackCD);
        }
    }
    private void Idling()
    {
        //idle after arrived at patrol point 
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

    private void Confusing()
    {
        // enemy in confused state will not move
        // enemy will enter confuse state if lost visual of player
        if(currConfuseTime > 0)
        {
           currConfuseTime -= Time.deltaTime;
           
        }
        if (currConfuseTime <= 0)
        {
            currConfuseTime = 0;
            isConfused = false;
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
    

}
