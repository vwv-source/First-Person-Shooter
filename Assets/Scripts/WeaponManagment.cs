using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagment : MonoBehaviour
{

    [SerializeField] GameObject AK47;
    [SerializeField] GameObject BerettaM9;
    [SerializeField] GameObject Shotgun;
    [SerializeField] LayerMask raycastlayer;
    public float selected = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E)){
            RaycastHit ray;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out ray, Mathf.Infinity, raycastlayer)){
                if(ray.collider.gameObject.tag == "berettaAmmo"){
                    BerettaM9.GetComponent<BerettaM9>().magazines = BerettaM9.GetComponent<BerettaM9>().maxmagazines;
                    BerettaM9.GetComponent<BerettaM9>().bullets = BerettaM9.GetComponent<BerettaM9>().magazinemax;
                }
                if(ray.collider.gameObject.tag == "ak47Ammo"){
                    AK47.GetComponent<AK47>().magazines = AK47.GetComponent<AK47>().maxmagazines;
                    AK47.GetComponent<AK47>().bullets = AK47.GetComponent<AK47>().magazinemax;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            selected = 1;
            AK47.SetActive(false);
            Shotgun.SetActive(false);
            BerettaM9.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            selected = 2;
            AK47.SetActive(true);
            Shotgun.SetActive(false);
            BerettaM9.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            selected = 3;
            AK47.SetActive(false);
            Shotgun.SetActive(false);
            BerettaM9.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            selected = 4;
            AK47.SetActive(false);
            BerettaM9.SetActive(false);
            Shotgun.SetActive(true);
        }
    }
}
