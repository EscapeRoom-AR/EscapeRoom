using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectScene : MonoBehaviour

{
    void Update()
    {

        GameObject[] aboutUs;

        aboutUs = GameObject.FindGameObjectsWithTag("AboutUsButton");

        Debug.Log(Input.touchCount);
        if (Input.GetButtonDown("AboutUsButton")){ loadScene("6.AboutUsScene"); }
         void loadScene(string sceneName)
        {
            Debug.Log("algo");
            SceneManager.LoadScene(sceneName);
        }

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("touch detected");
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "AboutUsButton")
                {
                    loadScene("6.AboutUsScene");
                }

                if (raycastHit.collider.CompareTag("AboutUsButton"))
                {
                    loadScene("6.AboutUsScene");
                }
            }
        }
    }
}


/*void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) { Debug.Log("TOUCH  " + touch.position); }
            //  selectScene(touch);
        }
    }


    public void selectScene(Touch t)
    {
        Debug.Log(this.gameObject.name);

        switch (this.gameObject.name)
        {
            case "AboutUsButton":
                SceneManager.LoadScene("6.AboutUsScene");
                break;

            case "RankingButton":
                SceneManager.LoadScene("9.RankingScene");
                break;

            case "Back":
                SceneManager.LoadScene("4.MainScreen");
                break;

            case "ProfileButton":
                SceneManager.LoadScene("ProfileScreen");
                break;

            case "PlayButton":
                SceneManager.LoadScene("7.PlayScene");
                break;

        }
    }
}
    */
