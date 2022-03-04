using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMovingScr : MonoBehaviour
{
    public bool isTalking = false;


    public NavMeshAgent agent;
    public LayerMask groundMask, playermask;

    //var for patrol
    public Vector3 patrolPoint;
    public bool isPatrolPointSet;
    public float patrolPointRange;
    public float speed = 4;

    //var for idle
    public float idleTime = 2.5f;
    float currIdleTime = 0f;
    public bool isIdling = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        Patroling();
    }

    private void Patroling()
    {
        if (!isPatrolPointSet)
        {
            SearchPatrolPoint();
        }

        if (isPatrolPointSet)
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

        if (Physics.Raycast(patrolPoint, -transform.up, 2f, groundMask))
        {
            isPatrolPointSet = true;
        }
    }

    private void Idling()
    {
        //idle after arrived at patrol point 
        if (isIdling == false)
        {
            isIdling = true;
        }
        if (isIdling == true && currIdleTime == 0)
        {
            currIdleTime = idleTime;
        }
        else if (isIdling == true && currIdleTime > 0)
        {
            currIdleTime -= Time.deltaTime;
            if (currIdleTime <= 0)
            {
                currIdleTime = 0;
                isIdling = false;
            }
        }
    }
}
