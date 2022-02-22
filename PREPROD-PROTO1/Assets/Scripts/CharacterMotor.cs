using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public float m_MoveSpeed = 5.0f;

    public Rigidbody m_Rigid;

    private Vector3 m_Movement;

    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        m_Movement.x = Input.GetAxisRaw("Horizontal");
        m_Movement.z = Input.GetAxisRaw("Vertical");

        m_Rigid.MovePosition(m_Rigid.position + m_Movement * m_MoveSpeed * Time.deltaTime);
    }
}
