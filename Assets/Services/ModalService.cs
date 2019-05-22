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
        private GameObject modal;

        public void showModal(string text)
        {
            modal = Instantiate(modalPrefab, new Vector3(0,0,0), Quaternion.identity);
            Text messageObject = GameObject.Find("Message").GetComponent<Text>();
            messageObject.text = text;
            modal.transform.SetParent(canvas.transform,false);
            modal.name = "ErrorModal";
        }

        public void hideModal()
        {
            print("hide modal");
            Destroy(GameObject.Find("ErrorModal"));
        }
    }
}
