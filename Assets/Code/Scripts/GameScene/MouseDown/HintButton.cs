using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{
    public GameController GameController;
    private bool isModalShown;
    private GameObject modal;
    public GameObject modalPrefab;
    public Canvas canvas;


    void Start()
    {
        isModalShown = false;
    }

    public void OnClick()
    {
        ShowModal();
        FXService.Instance.PlayClick();
    }

    private void ShowModal()
    {
        if (isModalShown)
            return;
        isModalShown = true;

        modal = Instantiate(modalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        modal.transform.Find("Content").Find("Message").GetComponent<Text>().text = GameController.GetHint();
        modal.transform.SetParent(canvas.transform, false);
        modal.transform.Find("ButtonHolder").Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            isModalShown = false;
            Destroy(modal);
        });
        
    }
}
