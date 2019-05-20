using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Services;

public class Example : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button m_YourFirstButton;
    private REST2 service = new REST2();

    void Start()
    {
        Debug.Log("Form udfgsrga!");
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        m_YourFirstButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
        service.register(new Model.User("username", "test@gmail.com", "pswrd"));
    }

    void TaskWithParameters(string message)
    {
        //Output this to console when the Button2 is clicked
        Debug.Log(message);
    }

    void ButtonClicked(int buttonNo)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Button clicked = " + buttonNo);
        //service.login("admin", "escape.room");
    }
}
