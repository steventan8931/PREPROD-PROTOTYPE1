using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerUpdate : MonoBehaviour
{
    public bool isFront = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterMotor>())
        {
            if (isFront)
            {
                other.gameObject.transform.GetChild(0).GetComponent<PlayerSpriteLayer>().InFront();
            }
            else
            {
                other.gameObject.transform.GetChild(0).GetComponent<PlayerSpriteLayer>().Behind();
            }
        }
    }


}
