using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnClick : MonoBehaviour
{
    /* void Start()
     {
     }*/

    private void OnMouseDown()
    { Debug.Log(gameObject.tag);
        Destroy(gameObject);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
                { BoxCollider bc = hit.collider as BoxCollider;
                
                if (bc == null)
                {
                    Debug.Log("calling remove on click");

                    Destroy(bc.gameObject);

                }
            } } } }


