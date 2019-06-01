using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class pedirCode : MonoBehaviour
{
    GameObject window;
    public GameObject InputCode;
    public Canvas canvas;
    public Animator animator;
    private bool isModalShown = false;

    public void OnMouseDown()
    {
        if (isModalShown) return;
        isModalShown = true;
        window = Instantiate(InputCode, new Vector3(0, 0, 0), Quaternion.identity);
        window.transform.SetParent(canvas.transform, false);

      Button button1= window.transform.Find("Button1").GetComponent<Button>();

        button1.onClick.AddListener(() => closeWindow());
    }

    public void closeWindow()
    {

        InputField field1 = window.transform.Find("Input1").GetComponent<InputField>();
        string palabraSecreta=field1.text;
     
        if (palabraSecreta.ToLower().Equals("stars"))
        {
            animator.SetTrigger("subirTrigger");
        }
        Destroy(window);
        isModalShown = false;

    }
}
