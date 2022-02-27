using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTranslucent : MonoBehaviour
{
    public SpriteRenderer cacheSprite;

    private void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 100.0f))
        {
            //if (hitInfo.collider.transform.childCount > 0)
            //{
            //    if (hitInfo.collider.transform.GetChild(0).GetComponent<SpriteRenderer>())
            //    {
            //        cacheSprite = hitInfo.collider.transform.GetChild(0).GetComponent<SpriteRenderer>();
            //        Debug.Log("sprite hit");
            //        hitInfo.collider.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);
            //    }
            //}
            //else
            //{
            //    cacheSprite.color = Color.white;
            //}

            if (hitInfo.collider.transform.GetComponent<SpriteRenderer>())
            {
                cacheSprite = hitInfo.collider.transform.GetComponent<SpriteRenderer>();
                hitInfo.collider.transform.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = Camera.main.transform.forward * 100f;
        Gizmos.DrawRay(Camera.main.transform.position, direction);
        //Gizmos.DrawRay(ray);

    }
}
