﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlacement : MonoBehaviour
{
    public GameObject m_BuildablePrefab;

    public GameObject m_CurrentPlaceableObject;

    private BuildItemController m_BuildController;
    private Hand m_Hand;
    private Items cacheItemType;

    private void Start()
    {
        m_BuildController = FindObjectOfType<BuildItemController>();
        m_Hand = FindObjectOfType<Hand>();
    }

    private void Update()
    {
        //Right click to remove item
        //if (Input.GetMouseButtonDown(1))
        
        if (!m_BuildController.CanBuild())
        {
            return;
        }
        if (m_CurrentPlaceableObject == null)
        {
            m_BuildablePrefab = m_BuildController.GetItem(m_Hand.m_ItemType);
            cacheItemType = m_Hand.m_ItemType;
            if (m_BuildablePrefab != null)
            {
                m_CurrentPlaceableObject = Instantiate(m_BuildablePrefab);
                m_CurrentPlaceableObject.layer = 2;
                m_CurrentPlaceableObject.GetComponent<BoxCollider>().isTrigger = true;
                //m_CurrentPlaceableObject.transform.GetChild(0).gameObject.layer = 2;
            }

        }
        else
        {
            if (m_BuildController.GetItem(m_Hand.m_ItemType) == null || cacheItemType != m_Hand.m_ItemType) 
            {
                Destroy(m_CurrentPlaceableObject);
                m_BuildablePrefab = null;
            }

        }


        if (m_CurrentPlaceableObject != null)
        {
            MoveObjectToMouse();
        }
    }

    private void MoveObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            m_CurrentPlaceableObject.transform.position = hitInfo.point;
            //m_CurrentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }

        //If object has a collider
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Floor"))
            {

                BoxCollider PlaceableCollider = m_CurrentPlaceableObject.GetComponent<BoxCollider>();
                //PlaceableCollider.isTrigger = true;
                Vector3 BoxCenter = m_CurrentPlaceableObject.transform.position + PlaceableCollider.center;
                Vector3 HalfExtents = PlaceableCollider.size / 2;

                m_CurrentPlaceableObject.GetComponent<SpriteRenderer>().color = new Vector4(0.4629316f, 0.827038f, 0.9716981f, 0.682353f);

                if (Physics.CheckBox(BoxCenter, HalfExtents, Quaternion.identity))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        m_CurrentPlaceableObject.GetComponent<SpriteRenderer>().color = Color.white;
                        m_BuildController.UseItem(m_Hand.m_ItemType);
                        m_CurrentPlaceableObject.layer = 1;
                        PlaceableCollider.isTrigger = false;
                        m_CurrentPlaceableObject = null;
                    }
                }

            }
            else
            {
                m_CurrentPlaceableObject.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.3553701f, 0.3349057f, 0.682353f);
            }
        }
    }
}