using System.Collections;
using UnityEngine;

public class Locker : MonoBehaviour
{
    [Header("Arrastra aquí el objeto de la llave")]
    public GameObject llaveObjeto;

    void Start()
    {

        if (llaveObjeto != null)
        {
            llaveObjeto.SetActive(false);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (llaveObjeto != null)
            {
                StartCoroutine(ActivarConRetraso());
            }
        }
    }


    IEnumerator ActivarConRetraso()
    {
        yield return new WaitForSeconds(0.3f); 
        llaveObjeto.SetActive(true);
    }
}