using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTouch : MonoBehaviour
{

    public Animator anim;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                print("detected");
                //Input.GetTouch(i).position.y

            }
        }


    }
}

/*{
    string clickedName;
    public void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit)) {
                clickedName = Hit.transform.name;
                Debug.Log("OBJECT DETECTED!!!!!" + clickedName);
            }
        }
    }




    public void Interaction(GameObject gameObject)
    {
        Debug.Log("click detectado");
        Debug.Log(gameObject.name);
        
    }

    void RegisterModelTouch()
    {
  
        Touch touch = Input.touches[0];
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out hit))
        { Debug.Log(hit.collider.tag);
        }

    }
} */