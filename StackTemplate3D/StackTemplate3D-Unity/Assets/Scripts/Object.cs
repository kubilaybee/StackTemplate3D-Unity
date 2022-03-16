using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public bool IsPivotObject; // main object
    public bool IsStackList; // added the stack list

    [Header("MAIN OBJECT MOVEMENT VARIABLES")]
    public float MovementSpeed;
    public float swipeSpeed;
    private float distance = 50;

    // Update is called once per frame
    void Update()
    {
        if (IsPivotObject)
        {
            // Main object Movement
            transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);
            if (Input.GetMouseButton(0))
            {
                MainObjectMvement();
            }
        }
    }

    private void MainObjectMvement()
    {
        // raycast mouse movement
        Vector3 mousePos = Input.mousePosition;
        // cam laser point
        mousePos.z = Camera.main.transform.localPosition.z;
        // laser
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        // find hit point
        RaycastHit hit;
        // turn boolean
        if(Physics.Raycast(ray,out hit, distance))
        {
            // find the pos
            Vector3 hitVec = hit.point;
            // for horizantal movement
            hitVec.y = transform.position.y;
            hitVec.z = transform.position.z;

            // movement
            transform.position = Vector3.Lerp(transform.position, hitVec, Time.deltaTime * swipeSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // my object in stack list
        if (IsStackList)
        {
            if (other.GetComponent<Object>())
            {
                // other object not in stack list
                if (!other.GetComponent<Object>().IsStackList)
                {
                    other.GetComponent<Object>().IsStackList = true;
                    // add the list 
                    GameManager.Instance.AddObjecttoList(other.gameObject);
                }
            }
        }
    }
}
