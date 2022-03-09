using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTranslucent : MonoBehaviour
{
    public SpriteRenderer cacheSprite;
    public Transform m_Player;
    private void Start()
    {
        m_Player = FindObjectOfType<CharacterMotor>().gameObject.transform;
    }

    private void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 100.0f))
        {
            //Only if player is behind the object
            if (hitInfo.collider.transform.position.z < m_Player.position.z)
            {
                if (hitInfo.collider.transform.GetComponent<SpriteRenderer>())
                {
                    cacheSprite = hitInfo.collider.transform.GetComponent<SpriteRenderer>();
                    if (hitInfo.collider.transform.GetComponent<BuildableObject>())
                    {
                        if (hitInfo.collider.transform.GetComponent<BuildableObject>().m_Collidable)
                        {
                            hitInfo.collider.transform.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);
                        }
                    }
                    else
                    {
                        hitInfo.collider.transform.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);
                    }

                }
                else
                {
                    if (cacheSprite != null)
                    {
                        cacheSprite.color = Color.white;
                    }
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = Camera.main.transform.forward * 100f;
        Gizmos.DrawRay(Camera.main.transform.position, direction);
        //Gizmos.DrawRay(ray);

    }
}
