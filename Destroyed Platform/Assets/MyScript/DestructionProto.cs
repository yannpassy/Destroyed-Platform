using UnityEngine;
using System.Collections;

public class DestructionProto : MonoBehaviour {
    private GameObject surfaceDetruite1;
    private GameObject surfaceDetruite2;
    private GameObject surfaceDetruite3;
    private GameObject surfaceDetruite4;
    private bool changerPlateforme;

    void Start()
    {
        surfaceDetruite1 = GameObject.Find("surfaceDetruite1");
        surfaceDetruite2 = GameObject.Find("surfaceDetruite2");
        surfaceDetruite2.SetActive(false);
        surfaceDetruite3 = GameObject.Find("surfaceDetruite3");
        surfaceDetruite3.SetActive(false);
        surfaceDetruite4 = GameObject.Find("surfaceDetruite4");
        surfaceDetruite4.SetActive(false);
        changerPlateforme = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Box001")
        {
            changerPlateforme = true;
        }

        if (col.gameObject.name == "surfaceDetruite1" && changerPlateforme)
        {
            surfaceDetruite1.SetActive(false);
            surfaceDetruite2.SetActive(true);
            changerPlateforme = false;
        }

        if (col.gameObject.name == "surfaceDetruite2" && changerPlateforme)
        {
            surfaceDetruite2.SetActive(false);
            surfaceDetruite3.SetActive(true);
            changerPlateforme = false;
        }

        if (col.gameObject.name == "surfaceDetruite3" && changerPlateforme)
        {
            surfaceDetruite3.SetActive(false);
            surfaceDetruite4.SetActive(true);
            changerPlateforme = false;
        }
    }
}
