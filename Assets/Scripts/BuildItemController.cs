using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItemController : MonoBehaviour
{
    public GameObject m_RockPrefab;
    public GameObject m_WoodPrefab;

    public GameObject GetItem(Items _ItemType)
    {
        switch (_ItemType)
        {
            case Items.Empty:
                return null;
            case Items.Rock:
                return m_RockPrefab;
            case Items.Wood:
                return m_WoodPrefab;
            case Items.Sword:
                return m_WoodPrefab;
        }

        return null;

    }
}
