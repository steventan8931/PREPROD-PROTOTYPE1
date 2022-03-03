using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform PlayerTransform;
    public LayerMask groundMask, playermask;
    public FieldOfView enemyFOV;
    public bool lastFrameInSight = false;
    [Range(1,2)]
    public int EnemyType;
    public float HitPoints;
    public float speed;
    public float ghostChaseSpd;
    public float dmgVal;

    public CharacterMotor playerMotor;

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
    public GameObject EnemyProjectile;
    //state
    public float attackRange;
    public bool isPlayerInSight, isPlayerInAttackRange;


    //for quest
    public Quest quest;

    private void Awake()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        quest = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>().CurrQuest;
        if(EnemyType == 2)
        {
            EnemyProjectile.GetComponent<EnemyBulletScr>().damage = dmgVal;
        }
    }
    void Update()
    {
        // update quest 
        quest = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>().CurrQuest;
        //check if player is in sight or in attackRange
        isPlayerInSight = enemyFOV.canSeePlayer;
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playermask);
        if (lastFrameInSight == true && isPlayerInSight == false)
        {
            //lost visual of player
            isConfused = true;
            Debug.Log("confused!");
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
        agent.SetDestination(PlayerTransform.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(PlayerTransform);

        if(!isAttacked)
        {
            // attack player
            
            if(EnemyType == 1)
            {
                //do melee attack 
                Debug.Log("attacking (melee)");
                playerMotor.takeDmg(dmgVal);
                // add trigger to attack animation
                

            }
            if (EnemyType == 2)
            {
                //do ranged attack
                Rigidbody rb = Instantiate(EnemyProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.forward * 8f, ForceMode.Impulse);
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
    
    public void takeDmg(float dmg)
    {
        HitPoints -= dmg;
        if(HitPoints <= 0)
        {
            deathFunc();
        }
    }

    void deathFunc()
    {
        if(EnemyType == 1)
        {
            quest.goal.MeleeEnemyKilled();
        }
        if(EnemyType == 2)
        {
            quest.goal.RangedEnemyKilled();
        }
        Destroy(gameObject);
    }
}
