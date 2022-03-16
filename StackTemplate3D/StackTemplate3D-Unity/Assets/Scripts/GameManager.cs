using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject MainObjectPrefab; // pbject prefab
    public GameObject CurrentPivotObject;   // current object

    [Header("MOVEMENT")]
    public float ZOffset; // object distance
    public float XLerpSpeed; // stack objects sensivity

    public List<GameObject> CurrentObjects = new List<GameObject>();    // Stack objects

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreatePivotObject();
    }

    private void Update()
    {
        stackObjectsMovement();
    }

    public void CreatePivotObject()
    {
        CurrentPivotObject = Instantiate(MainObjectPrefab);
        CurrentPivotObject.GetComponent<Object>().IsPivotObject = true;
        CurrentPivotObject.GetComponent<Object>().IsStackList = true;

        // cam follow
        Camera.main.transform.SetParent(CurrentPivotObject.transform);
    }

    public void AddObjecttoList(GameObject stackObject)
    {
        CurrentObjects.Add(stackObject);
    }

    // movements stack objects
    public void stackObjectsMovement()
    {
        if (CurrentObjects.Count == 0)
        {
            // my list is empty
            return;
        }
        for (int i = 0; i < CurrentObjects.Count; i++)
        {

            if (i == 0)
            {
                // temp vector 3 pos
                Vector3 tempPos = CurrentPivotObject.transform.position;
                // add ofset
                tempPos.z += ZOffset;
                // first stack elements
                CurrentObjects[i].transform.position = Vector3.Lerp(CurrentObjects[i].transform.position, tempPos,Time.deltaTime * XLerpSpeed);
            }
            else
            {
                // temp vector 3 pos
                Vector3 tempPos = CurrentObjects[i - 1].transform.position;
                // add ofset
                tempPos.z += ZOffset;
                // my stack list size
                CurrentObjects[i].transform.position = Vector3.Lerp(CurrentObjects[i].transform.position, tempPos, Time.deltaTime * XLerpSpeed);
            }
        }
    }
}
