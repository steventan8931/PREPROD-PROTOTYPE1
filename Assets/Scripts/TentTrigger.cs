using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TentTrigger : MonoBehaviour
{
    public Animator m_Animation;
    private CharacterMotor m_Player;
    public bool m_IsColliding = false;
    public bool m_TentOpen = false;
    public GameObject m_EndUI;

    public float m_Timer = 0.0f;
    public float m_FadeTime = 2.0f;
    private Color cachecolor;
    private void Start()
    {
        //m_Animation = GetComponent<Animator>();
        cachecolor = m_EndUI.transform.GetChild(0).GetComponent<Image>().color;
        m_EndUI.transform.GetChild(0).GetComponent<Image>().color = new Color(cachecolor.r,cachecolor.g,cachecolor.b, 0.0f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterMotor>())
        {
            m_Player = other.GetComponent<CharacterMotor>();
            m_IsColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterMotor>())
        {
            m_IsColliding = false;
        }
    }

    private void Update()
    {
        if (m_IsColliding && m_Player.m_FinishedQuests)
        {
            m_Player.GetComponent<Inventory>().Prompt("Press F to Enter The Tent");
            Debug.Log("in tent");
            if (Input.GetKeyDown(KeyCode.F))
            {
                m_EndUI.SetActive(true);
                m_Player.m_Animation.ResetTrigger("Attacking");
                m_Player.m_Animation.SetTrigger("Attacking");
                m_TentOpen = true;
            }
        }

        if (m_TentOpen)
        {
            m_Animation.SetBool("open", true);
            m_Timer += Time.deltaTime;

            m_EndUI.transform.GetChild(0).GetComponent<Image>().color = new Color(cachecolor.r, cachecolor.g, cachecolor.b, m_Timer / m_FadeTime);
            if (m_Timer >= m_FadeTime)
            {
                SceneManager.LoadScene(2);
            }

        }
        else
        {
            m_Animation.SetBool("open", false);
        }
    }
}
