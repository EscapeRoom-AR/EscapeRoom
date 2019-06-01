using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class ModalService : MonoBehaviour
    {
        public Canvas canvas;
        public GameObject modalPrefab;

        public void ShowModal(string text)
        {
            GameObject modal = Instantiate(modalPrefab, new Vector3(0,0,0), Quaternion.identity);
            Text messageObject = modal.transform.Find("Content").Find("Message").GetComponent<Text>();
            messageObject.text = text;
            modal.transform.SetParent(canvas.transform,false);
            modal.transform.Find("ButtonHolder").Find("Button").GetComponent<Button>().onClick.AddListener(() => Destroy(modal));
        }
    }
}
