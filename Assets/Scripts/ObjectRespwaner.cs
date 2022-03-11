using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespwaner : MonoBehaviour
{
    public static ObjectRespwaner Instance;

    public GameObject m_TreePrefab;
    public GameObject m_RockPrefab;

    public List<GameObject> m_Trees;
    public List<GameObject> m_Rocks;

    private int cacheTreeCount = 0;
    private int cacheRockCount = 0;
    Vector3 cacheColliderPoint;

    public float m_RespawnTreeTime = 5.0f;
    public float m_RespawnTreeTimer = 0.0f;

    public float m_RespawnRockTime = 5.0f;
    public float m_RespawnRockTimer = 0.0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Tree[] trees = FindObjectsOfType<Tree>();

        for (int i = 0; i < trees.Length; i++)
        {
            m_Trees.Add(trees[i].gameObject);
        }

        Rock[] rocks = FindObjectsOfType<Rock>();

        for (int i = 0; i < rocks.Length; i++)
        {
            m_Rocks.Add(rocks[i].gameObject);
        }

        cacheTreeCount = m_Trees.Count;
        cacheRockCount = m_Rocks.Count;
    }

    private void Update()
    {
        RespawnObject(m_Trees, cacheTreeCount, m_TreePrefab, ref m_RespawnTreeTimer, m_RespawnTreeTime, 5.2f);

        RespawnObject(m_Rocks, cacheRockCount, m_RockPrefab, ref m_RespawnRockTimer, m_RespawnRockTime, 1.86f);


    }

    private void RespawnObject(List<GameObject> _List, int _ObjectCount, GameObject _Prefab, ref float _Timer, float _Time,float _Height)
    {
        if (_List.Count < _ObjectCount)
        {
            _Timer += Time.deltaTime;

            if (_Timer > _Time)
            {
                Debug.Log("no");
                for (int i = 0; i < _ObjectCount - _List.Count; i++)
                {
                    cacheColliderPoint = new Vector3(Random.Range(13, 85), _Height, Random.Range(20, 47));
                    transform.TransformPoint(cacheColliderPoint);

                    Debug.Log("Spawning" + _Prefab.name);
                    GameObject temp = Instantiate(_Prefab, cacheColliderPoint, _List[0].transform.localRotation, _List[0].transform.parent);
                    _List.Add(temp);
                    _Timer = 0;
                    break;
                }
            }
        }
    }





}
