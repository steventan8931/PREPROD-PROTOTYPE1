using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScr : MonoBehaviour
{
    public float damage;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(destroyAfterWhile), 1.2f);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //nearDetection();
    }

    void destroyAfterWhile()
    {
        Destroy(gameObject);
    }
    
    void nearDetection()
    {
        if(Vector3.Distance(Player.transform.position,transform.position) < 0.3f)
        {
            Player.GetComponent<CharacterMotor>().takeDmg(damage);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("hit something");
            //do damage
            collision.gameObject.GetComponent<CharacterMotor>().takeDmg(damage);
            //destroy itself
            Destroy(gameObject);
        }
    }
}
