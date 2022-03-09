using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteLayer : MonoBehaviour
{
    public SpriteRenderer[] m_Sprites;

    public void InFront()
    {
        for (int i = 0; i < m_Sprites.Length; i++)
        {
            m_Sprites[i].sortingLayerName = "Front";
        }
    }
    public void Behind()
    {
        for (int i = 0; i < m_Sprites.Length; i++)
        {
            m_Sprites[i].sortingLayerName = "Behind";
        }
    }
}