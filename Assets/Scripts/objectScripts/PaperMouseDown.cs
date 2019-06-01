using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperMouseDown : MonoBehaviour
{
    public PaperHintController controller;
    
    private void OnMouseDown()
    {
        controller.PaperTapped(gameObject);
    }
}
