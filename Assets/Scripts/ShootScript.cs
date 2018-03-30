using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default
{

    public class ShootScript : MonoBehaviour
    {
        //hier zet je de prefab, shootpoint en preview cube in, je zorgt hiermee dat je kan zien waar je de cube neer gaat zetten
        //voordat hij er staat. 
        public GameObject prefab;
        public Transform shootPoint;
        public Transform previewCube;
     

        // Update is called once per frame
        void Update()
        {
            //als je de linkermuisknop indruk zie je de preview van de cube die je neer gaat zetten
            if (Input.GetMouseButtonDown(0))
            {
                previewCube.gameObject.SetActive(true);
            }


            if (Input.GetMouseButton(0))
            {   //hier stel je in waar de previewcube geplaatst wordt, namelijk op de plek van je shoot point.
                RaycastHit hit;
                if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 100f))
                {
                    previewCube.transform.position = hit.point + hit.normal * 0.5f;
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                //als je de linkermuisknop loslaat, zet je de echte cube neer en disable je de preview cube, zodat je die niet meer ziet. 
                RaycastHit hit;
                if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 100f))
                {
                    Instantiate(prefab, previewCube.transform.position, previewCube.transform.rotation);
                }
                previewCube.gameObject.SetActive(false);
            }
        }
    }
}