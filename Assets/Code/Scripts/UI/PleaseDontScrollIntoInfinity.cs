using UnityEngine;

public class PleaseDontScrollIntoInfinity : MonoBehaviour
{
    private int buttonSize;
    private int numberOfButtons;
    private int mValue;
    public RectTransform cont;
    // Start is called before the first frame update
    void Start()
    {
        buttonSize = 200;
        numberOfButtons = 10;
        mValue = buttonSize * numberOfButtons;
    }

    // Update is called once per frame
    void Update()
    {
        if (cont.offsetMax.y < 0)
        { //It seems that is checking for less than 0, but the syntax is weird
            cont.offsetMax = new Vector2(); //Sets its value back.
            cont.offsetMin = new Vector2(); //Sets its value back.
        }

        if (cont.offsetMax.y > (numberOfButtons * buttonSize) - buttonSize)
        { // Checks the values
            cont.offsetMax = new Vector2(0, (numberOfButtons * buttonSize) - buttonSize); // Set its value back
            cont.offsetMin = new Vector2(); //Depending on what values you set on your scrollview, you                                              might want to change this, but my one didn't need it.
        }
    }
}
