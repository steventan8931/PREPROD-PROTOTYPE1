using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public float m_MoveSpeed = 5.0f;
    private float cachespeed = 0;
    public bool m_Attacked = false;
    public float m_AttackCooldown = 0.5f;
    private float m_AttackMaxCooldown = 0.0f;
    public float hitpoints = 150f;
    public bool cacheDead = false;

    public Transform m_AttackPoint;
    public Rigidbody m_Rigid;
    public GameObject m_Sprite;
    public Animator m_Animation;
    private Vector3 m_Movement;
    private Hand m_Hand;

    private AudioManager m_Audio;
    private AudioSource m_AudioSource;

    public Vector3 m_SpawnPoint;
    private Vector3 cacheSpawnPoint;
    public bool m_CanMove = true;
    public bool m_FinishedQuests = false;

    public GameObject m_DamageVFX;

    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
        m_AttackMaxCooldown = m_AttackCooldown;
        m_Sprite = transform.GetChild(0).gameObject;
        m_Animation = m_Sprite.GetComponent<Animator>();
        m_Hand = GetComponent<Hand>();
        m_AudioSource = GetComponent<AudioSource>();

        m_SpawnPoint = transform.position;
        cacheSpawnPoint = m_SpawnPoint;
    }

    //For quests
    public bool SpawnPointChanged()
    {
        if (cacheSpawnPoint != m_SpawnPoint)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        m_Movement.x = Input.GetAxisRaw("Horizontal");
        m_Movement.z = Input.GetAxisRaw("Vertical");

        //dont move if dead
        if (hitpoints <= 0)
        {
            m_Rigid.velocity = Vector3.zero;
            //Revive
            if (Input.GetKeyDown(KeyCode.R))
            {
                m_Animation.SetBool("IsDead", false);
                transform.position = m_SpawnPoint;
                hitpoints = 150;
                GetComponent<Inventory>().m_PressGText.enabled = false;
            }
            return;
        }
        //if (m_AttackPoint != null)
        {
            UpdateStates();
            Attack();
        }

        if (!m_CanMove)
        {
            m_Movement.x = 0;
            m_Movement.z = 0;
            UpdateStates();
            return;
        }
        else
        {
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
            m_Sprite.transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);

        }
        else if (m_Movement.x > 0)
        {
            m_AttackPoint.localPosition = new Vector3(1.0f, 0.0f, 0.0f);
            m_Sprite.transform.localScale = new Vector3(-0.2f, 0.2f, 1.0f);

        }

        if (m_Movement.x == 0 && m_Movement.z == 0)
        {
            m_Rigid.velocity = Vector3.zero;
            m_Animation.SetBool("IsWalking", false);
            m_AudioSource.Stop();
        }
        else
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }

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
            AudioManager.Instance.PlayAudio("onhit");
            Debug.Log("Attacking");
            m_Animation.ResetTrigger("Attacking");
            m_Animation.SetTrigger("Attacking");
            Collider[] objects = Physics.OverlapSphere(m_AttackPoint.position, 1.0f);

            foreach (Collider hit in objects)
            {

                if (hit.GetComponent<Tree>() != null)
                {
                    Debug.Log("hit tree");
                    //m_AttackPoint.GetComponent<Renderer>().material.color = Color.red; //Temp
                    hit.GetComponent<Interactable>().TakeDamage(m_Hand.GetTreeDamage());
                    //Instantiate(hit.GetComponent<Interactable>().m_DamagePrefab, m_AttackPoint.position, Quaternion.identity);
                    AudioManager.Instance.PlayAudio("treehit");
                }

                if (hit.GetComponent<Rock>() != null)
                {
                    //m_AttackPoint.GetComponent<Renderer>().material.color = Color.red; //Temp
                    hit.GetComponent<Interactable>().TakeDamage(m_Hand.GetRockDamage());
                    //Instantiate(hit.GetComponent<Interactable>().m_DamagePrefab, m_AttackPoint.position, Quaternion.identity);
                    AudioManager.Instance.PlayAudio("rockhit");
                }

                if (hit.GetComponent<EnemyAI>() != null)
                {
                    hit.GetComponent<EnemyAI>().takeDmg(m_Hand.GetEnemyDamage());
                    AudioManager.Instance.PlayAudio("enemyhit");
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
            if (!cacheDead)
            {
                GetComponent<Inventory>().Prompt("Press R to Revive");
                m_Animation.ResetTrigger("Dying");
                m_Animation.SetTrigger("Dying");
            }
            cacheDead = true;
            m_Animation.SetBool("IsDead", true);
        }
        else
        {
            Instantiate(m_DamageVFX,transform);
            AudioManager.Instance.PlayAudio("playerhit");
            cacheDead = false;
        }
    }

}

