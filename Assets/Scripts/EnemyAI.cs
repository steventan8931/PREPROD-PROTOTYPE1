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

    //to right
    public bool isRight = false;
    public Vector3 previousPos;
    public float curSpeed;
    //var for dead 
    public bool isDead = false;
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

    //var for flee from fire
    public bool isLastFrameFlee = false;
    public float avoidDist;
    //state
    public float attackRange;
    public bool isPlayerInSight, isPlayerInAttackRange;

    //for spawner
    public EnemySpawner spawner;

    //for quest
    public Quest quest;

    //for animator
    public Animator enemyAnimator;
    private void Awake()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        quest = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>().CurrQuest;
        if(EnemyType == 2)
        {
            EnemyProjectile.GetComponent<EnemyBulletScr>().damage = dmgVal;
        }
    }
    void Update()
    {
        if (isDead != true)
        {
            // update quest 
            quest = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>().CurrQuest;
            //check if player is in sight or in attackRange
            isPlayerInSight = enemyFOV.canSeePlayer;
            isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playermask);

            GameObject closestFire = FindClosestObj("FirePlace");
            float distance = Mathf.Infinity;

            Vector3 curMove = transform.position - previousPos;
            curSpeed = curMove.magnitude / Time.deltaTime;

            if (curMove.x > 0 && isRight == false)
            {
                Vector3 tempScaleVec = new Vector3(-1, 1, 1);
                transform.localScale = tempScaleVec;
                isRight = true;
            }

            if (curMove.x < 0 && isRight == true)
            {
                Vector3 tempScaleVec = new Vector3(1, 1, 1);
                transform.localScale = tempScaleVec;
                isRight = false;
            }


            if (closestFire != null)
            {
                distance = Vector3.Distance(transform.position, closestFire.transform.position);
                //Debug.Log("enemy to fire distance: " + distance);

            }


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
            else if (distance < avoidDist)
            {
                isPatrolPointSet = false;
                Vector3 distToFire = transform.position - closestFire.transform.position;
                Vector3 newLoc = transform.position + distToFire;
                agent.SetDestination(newLoc);

            }
            else
            {
                // if (!isPlayerInSight && !isPlayerInAttackRange)
                if (!isPlayerInSight)
                {
                    Patroling();
                    //enemyAnimator.SetBool("IsAttacking", false);
                }
                if (isPlayerInSight && !isPlayerInAttackRange)
                {
                    currIdleTime = 0;
                    Chasing();
                    //enemyAnimator.SetBool("IsAttacking", false);
                }
                if (isPlayerInAttackRange && isPlayerInSight)
                {
                    Attacking();
                }


            }
            //save in sight bool
            lastFrameInSight = isPlayerInSight;

            previousPos = transform.position;
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
            enemyAnimator.SetBool("IsWalking", true);
            enemyAnimator.SetBool("IsIdling", false);
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

        NavMeshPath patrolPath = new NavMeshPath();
        if(Physics.Raycast(patrolPoint,-transform.up,2f,groundMask) && (agent.CalculatePath(patrolPoint, patrolPath) && patrolPath.status == NavMeshPathStatus.PathComplete))
        {
            isPatrolPointSet = true;
        }
    }

    private void Chasing()
    {
        agent.SetDestination(PlayerTransform.position);
        enemyAnimator.SetBool("IsWalking", true);
        enemyAnimator.SetBool("IsIdling", false);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        //transform.LookAt(PlayerTransform);

        if(!isAttacked)
        {
            // attack player
            
            if(EnemyType == 1)
            {
                //do melee attack 
                Debug.Log("attacking (melee)");
                enemyAnimator.SetBool("IsAttacking", true);
                playerMotor.takeDmg(dmgVal);
                // add trigger to attack animation
                

            }
            if (EnemyType == 2)
            {
                //do ranged attack
                Rigidbody rb = Instantiate(EnemyProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                Vector3 fireDir = PlayerTransform.position - transform.position;
               // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
              //  rb.AddForce(transform.forward * 8f, ForceMode.Impulse);
                rb.AddForce(fireDir * 32f, ForceMode.Impulse);
               // rb.AddForce(fireDir * 8f, ForceMode.Impulse);
                //add trigger to attack animation
                enemyAnimator.SetBool("IsAttacking", true);
            }
            isAttacked = true;
            Invoke(nameof(stopAttackingAnim), 0.2f);
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
            enemyAnimator.SetBool("IsIdling", true);
            enemyAnimator.SetBool("IsWalking", false);
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
           enemyAnimator.SetBool("IsIdling", true);
           enemyAnimator.SetBool("IsWalking", false);

        }
        if (currConfuseTime <= 0)
        {
            currConfuseTime = 0;
            isConfused = false;
            enemyAnimator.SetBool("IsIdling", false);
           
        }

    }
    private void ResetAttack()
    {
        isAttacked = false;
    }

    private void fleeFromFire()
    {
        
    }
    public GameObject FindClosestObj(string Tag)
    {
        GameObject[] objCollection;
        objCollection = GameObject.FindGameObjectsWithTag(Tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in objCollection)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void stopAttackingAnim()
    {
        enemyAnimator.SetBool("IsAttacking", false);
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
        isDead = true;
        spawner.enemyCount--;
        if (EnemyType == 1)
        {
            quest.goal.MeleeEnemyKilled();
            enemyAnimator.SetBool("IsDead", true);
            Invoke(nameof(killEnemy), 0.42f);

            
        }
        if(EnemyType == 2)
        {
            quest.goal.RangedEnemyKilled();
            killEnemy();
        }
        
        
       // Destroy(gameObject);
    }

    void killEnemy()
    {
        Destroy(gameObject);
    }
}
