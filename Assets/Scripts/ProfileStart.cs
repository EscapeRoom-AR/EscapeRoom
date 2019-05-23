using UnityEngine;

public class ProfileStart : MonoBehaviour
{
    public AuthController authController;
    public GameObject windowLoggedPrefab;
    public GameObject windowUnLoggedPrefab;
    public Canvas canvas;

    void Start()
    {
        if (authController.IsAuthenticated())
        {
            GameObject window = Instantiate(windowLoggedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            window.transform.SetParent(canvas.transform, false);
        } else
        {
            GameObject window = Instantiate(windowUnLoggedPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            window.transform.SetParent(canvas.transform, false);
        }
            

    }
}
