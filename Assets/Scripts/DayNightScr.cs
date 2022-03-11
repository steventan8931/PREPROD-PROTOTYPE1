using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayNightScr : MonoBehaviour
{
    public bool isNight = false;
    public float timeDN = 180f;
    public float DayTimeVal = 180f;
    public float NightTimeVal = 120f;
    public Image dayNightImg;
    public GameObject sunImg;
    public GameObject moonImg;

    public float m_HealthDecay = 5.0f;
    public CharacterMotor m_Player;

    void Start()
    {
        m_Player = FindObjectOfType<CharacterMotor>();
        //switchToNight();
    }

    // Update is called once per frame
    void Update()
    {
        DayNightCycle();

    
    }

    void DayNightCycle()
    {
        timeDN -= Time.deltaTime;
        int intTime = Mathf.FloorToInt(timeDN);

        if(timeDN <= 0 && isNight == false)
        {
            // change to night time
            isNight = true;
            timeDN = NightTimeVal;
            sunImg.SetActive(false);
            moonImg.SetActive(true);
        }

        if(timeDN <= 0 && isNight == true)
        {
            //change to day time
            isNight = false;
            timeDN = DayTimeVal;
            sunImg.SetActive(true);
            moonImg.SetActive(false);
        }
        if(timeDN > 0 && isNight == false)
        {
            float percentage = timeDN / DayTimeVal;
            float imageAval = 200 *(1 - percentage);
            int intAval = Mathf.FloorToInt(imageAval);
            Color32 origColor = dayNightImg.color;
            Color32 currColor = new Color32(origColor.r, origColor.g, origColor.b, (byte)intAval);
            dayNightImg.color = currColor;
        }
        if (timeDN > 0 && isNight == true)
        {
            if (!m_Player.m_NearCampfire)
            {
                if (m_Player.hitpoints > 0)
                {
                    m_Player.hitpoints -= m_HealthDecay * Time.deltaTime;
                }
                else
                {
                    //player died
                    m_Player.takeDmg(1);
                }
            }
            float percentage = timeDN / NightTimeVal;
            float imageAval = 200 * percentage;
            int intAval = Mathf.FloorToInt(imageAval);
            Color32 origColor = dayNightImg.color;
            Color32 currColor = new Color32(origColor.r, origColor.g, origColor.b, (byte)intAval);
            dayNightImg.color = currColor;
        }
    }

    public void switchToNight()
    {
        isNight = true;
        timeDN = NightTimeVal;
        sunImg.SetActive(false);
        moonImg.SetActive(true);
    }
}
