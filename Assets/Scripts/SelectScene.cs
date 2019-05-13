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

            selectScene(touch);
            } }
            

            public void selectScene(Touch t){ 
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
