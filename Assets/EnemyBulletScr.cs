﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScr : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //do damage
            collision.gameObject.GetComponent<CharacterMotor>().takeDmg(damage);
            //destroy itself
            Destroy(gameObject);
        }
    }
}
