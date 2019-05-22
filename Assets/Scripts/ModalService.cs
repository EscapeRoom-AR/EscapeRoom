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
        public GameObject modal;

        void Start()
        {
            showModal("test");
        }

        public void showModal(string text)
        {
            //Text messageObject = GameObject.Find("Message").GetComponent<Text>();
            //messageObject.text = text;
            print("show modal");
            modal = Instantiate(modalPrefab, new Vector3(Screen.width / 2, Screen.height /2, 0), Quaternion.identity);
            modal.transform.SetParent(canvas.transform,false);

        }

        public void hideModal()
        {
            Destroy(modal);
        }
    }
}
