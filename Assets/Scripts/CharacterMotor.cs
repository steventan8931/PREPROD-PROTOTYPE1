using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public float m_MoveSpeed = 5.0f;
    public bool m_Attacked = false;
    public float m_AttackCooldown = 0.5f;
    private float m_AttackMaxCooldown = 0.0f;
    public float hitpoints = 150f;

    public Transform m_AttackPoint;
    public Rigidbody m_Rigid;
    public GameObject m_Sprite;
    public Animator m_Animation;
    private Vector3 m_Movement;
    private Hand m_Hand;

    
    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        m_AttackMaxCooldown = m_AttackCooldown;
        m_Sprite = transform.GetChild(0).gameObject;
        m_Animation = m_Sprite.GetComponent<Animator>();
        m_Hand = GetComponent<Hand>();
    }

    private void Update()
    {
        m_Movement.x = Input.GetAxisRaw("Horizontal");
        m_Movement.z = Input.GetAxisRaw("Vertical");

        //if (m_AttackPoint != null)
        {
            UpdateStates();
            Attack();
        }
        m_Rigid.velocity = m_Movement * m_MoveSpeed * Time.fixedDeltaTime;
        //m_Rigid.AddForceAtPosition(m_Movement * m_MoveSpeed * Time.deltaTime, transform.position, ForceMode.Impulse);
        //if (m_Rigid.velocity.magnitude > m_MoveSpeed)
        //{
        //    m_Rigid.velocity = m_Rigid.velocity.normalized * m_MoveSpeed;
        //}
        //m_Rigid.MovePosition(m_Rigid.position + m_Movement * m_MoveSpeed * Time.deltaTime);
    }

    private void UpdateStates()
    {
        if (m_Movement.z < 0)
        {
            m_AttackPoint.localPosition = new Vector3(0.0f, 0.0f, -1.0f);
            //m_Sprite.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        }
        else if (m_Movement.z > 0)
        {
            m_AttackPoint.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
            //m_Sprite.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        }

        if (m_Movement.x < 0)
        {
            m_AttackPoint.localPosition = new Vector3(-1.0f, 0.0f, 0.0f);
            m_Sprite.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

        }
        else if (m_Movement.x > 0)
        {
            m_AttackPoint.localPosition = new Vector3(1.0f, 0.0f, 0.0f);
            m_Sprite.transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);

        }

        if (m_Movement.x == 0 && m_Movement.z == 0)
        {
            m_Rigid.velocity = Vector3.zero;
            m_Animation.SetBool("IsWalking", false);
        }
        else
        {
            m_Animation.SetBool("IsWalking", true);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(m_AttackPoint.position, 1.0f);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !m_Attacked)
        {
            Debug.Log("Attacking");


            Collider[] objects = Physics.OverlapSphere(m_AttackPoint.position, 1.0f);

            foreach (Collider hit in objects)
            {
                if (hit.GetComponent<Tree>() != null)
                {
                    Debug.Log("hit tree");
                    //m_AttackPoint.GetComponent<Renderer>().material.color = Color.red; //Temp
                    hit.GetComponent<Interactable>().TakeDamage(m_Hand.GetTreeDamage());
                    Instantiate(hit.GetComponent<Interactable>().m_DamagePrefab, m_AttackPoint.position, Quaternion.identity);
                }

                if (hit.GetComponent<Rock>() != null)
                {
                    //m_AttackPoint.GetComponent<Renderer>().material.color = Color.red; //Temp
                    hit.GetComponent<Interactable>().TakeDamage(m_Hand.GetRockDamage());
                    Instantiate(hit.GetComponent<Interactable>().m_DamagePrefab, m_AttackPoint.position, Quaternion.identity);
                }

                if (hit.GetComponent<EnemyAI>() != null)
                {
                    hit.GetComponent<EnemyAI>().takeDmg(m_Hand.GetEnemyDamage());
                }
                m_Attacked = true;
            }
        }
        if (m_Attacked)
        {
            m_AttackCooldown -= Time.deltaTime;
            if (m_AttackCooldown <= 0)
            {
                m_AttackCooldown = m_AttackMaxCooldown;
                m_Attacked = false;
            }
        }
        else
        {
            //m_AttackPoint.GetComponent<Renderer>().material.color = Color.black; //Temp
        }
    }

    public void takeDmg (float dmg)
    {
        hitpoints -= dmg;
        if(hitpoints <= 0)
        {
            hitpoints = 0;
            // death func
        }
    }
}

