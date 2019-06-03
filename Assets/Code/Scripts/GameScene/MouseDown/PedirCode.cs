using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PedirCode : MonoBehaviour
{
    GameObject window;
    public GameObject modalPrefab;
    public Canvas canvas;
    public Animator animator;
    public GameController gameController;
    private bool isModalShown = false;

    public void OnMouseDown()
    {
        if (isModalShown) return;
        isModalShown = true;
        window = Instantiate(modalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        window.transform.SetParent(canvas.transform, false);
        Button button1= window.transform.Find("Button1").GetComponent<Button>();
        button1.onClick.AddListener(() => closeWindow());
    }

    public void closeWindow()
    {

        InputField field1 = window.transform.Find("Input1").GetComponent<InputField>();
        string palabraSecreta=field1.text;
     
        if (palabraSecreta.ToLower().Equals("candle") || palabraSecreta.ToLower().Equals("torch"))
        {
            FXService.Instance.PlayOpenDoor();
            animator.SetTrigger("subirTrigger");
            gameController.GameOver();
        }
        Destroy(window);
        isModalShown = false;

    }
}
