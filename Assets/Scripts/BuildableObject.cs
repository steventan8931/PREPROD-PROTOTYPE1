using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    public float m_HeightCenter = 0;
    public bool m_Collidable = true;

    private void Start()
    {
        if (!m_Collidable)
        {
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
    }
}
