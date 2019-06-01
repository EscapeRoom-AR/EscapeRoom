using UnityEngine;
using System.Collections;

public class TorchFire : MonoBehaviour
{

    public GameObject TorchLight;
    public GameObject MainFlame;
    public GameObject BaseFlame;
    public GameObject Etincelles;
    public GameObject Fumee;
    public float MaxLightIntensity;
    public float IntensityLight = 5;




    void Update()
    {
      
     

        TorchLight.GetComponent<Light>().color = new Color(Mathf.Min(IntensityLight / 1.5f, 1f), Mathf.Min(IntensityLight / 2f, 1f), 0f);
        MainFlame.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 20f;
        BaseFlame.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 15f;
        Etincelles.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 7f;
        Fumee.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 12f;

    }
}

