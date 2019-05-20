using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectScene : MonoBehaviour
   
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            selectScene();
            } }


            public void selectScene()
    {
            Debug.Log(this.gameObject.name);

                switch (this.gameObject.name)
            {
            case "AboutUsButton":
                SceneManager.LoadScene("AboutUsButton");
                break;
          
       
                case "RankingButton":
                    SceneManager.LoadScene("RankingScene");
                    break;
            case "Back":
                SceneManager.LoadScene("MainScreen");
                break;

            case "ProfileButton":
                SceneManager.LoadScene("ProfileScreen");
                break;
            case "PlayButton":
                SceneManager.LoadScene("PlayScene");
                break;

        }
    }
}
