using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockInput : MonoBehaviour
{
    GameObject window;
    public GameObject modalPrefab;
    public Canvas canvas;
    private bool isModalShown = false;
    public AudioSource audioPlayer;

    public void OnMouseDown()
    {
        if (isModalShown) return;
        isModalShown = true;
        window = Instantiate(modalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        window.transform.SetParent(canvas.transform, false);
        Button button1 = window.transform.Find("Button").GetComponent<Button>();
        button1.onClick.AddListener(() => closeWindow());
    }

    public void closeWindow()
    {

        InputField minuteField = window.transform.Find("MinuteField").GetComponent<InputField>();
        InputField horaField = window.transform.Find("HourField").GetComponent<InputField>();

        if (horaField.text.ToLower().Equals("10") && minuteField.text.ToLower().Equals("45"))
        {
            audioPlayer.Play();
        }
        Destroy(window);
        isModalShown = false;

    }
}
