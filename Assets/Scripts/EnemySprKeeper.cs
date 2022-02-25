using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySprKeeper : MonoBehaviour
{
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var tempRotationVec = transform.rotation.eulerAngles;
        tempRotationVec.y = Enemy.transform.rotation.y;
        transform.rotation = Quaternion.Euler(tempRotationVec);
    }
}
