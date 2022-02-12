using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerettaM9 : MonoBehaviour
{
    [SerializeField] GameObject maincamera;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject barrel;
    [SerializeField] TextMeshProUGUI ammotext;
    [SerializeField] GameObject startlocation;
    [SerializeField] GameObject aimlocation;

    public float bullets = 15f;
    public float magazines = 2f;
    public float magazinemax = 15f;
    public float maxmagazines = 2f;
    public float nextActionTime = 0.0f;
    public float period = 0.1f;

    public bool canShoot = true;

    void Start()
    {
    }

    void Update()
    {
        ammotext.text = bullets+"/"+magazines*magazinemax;

        if(Input.GetMouseButton(1)){
            maincamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(maincamera.GetComponent<Camera>().fieldOfView, 40f, 0.1f);
            this.transform.position = Vector3.Lerp(this.transform.position, aimlocation.transform.position, 7 * Time.deltaTime);
        }else{
            maincamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(maincamera.GetComponent<Camera>().fieldOfView, 60f, 0.1f);
            this.transform.position = Vector3.Lerp(this.transform.position, startlocation.transform.position, 7 * Time.deltaTime);
        }


        if(Input.GetMouseButtonDown(0) && canShoot){
            if(bullets == 0 || bullets < 0){
                return;
            }
            StartCoroutine(shoot());
        }

        if(Input.GetKeyDown(KeyCode.R)){
            if(bullets > 0 || magazines == 0){
                return;
            }
            this.GetComponent<Animation>().Play("BerettaReload");
            magazines -= 1f;
            bullets = magazinemax;
        }
    }

    IEnumerator shoot(){
        this.GetComponent<Animation>().Play("Recoil");
        barrel.GetComponent<ParticleSystem>().Play();
        bullets -= 1f;
        GameObject bulletclone = Instantiate(bullet,barrel.transform.position, barrel.transform.rotation);
        bulletclone.GetComponent<Rigidbody>().velocity = barrel.transform.forward * 50;
        bulletclone.GetComponent<Bullet>().damage = 2f;
        canShoot = false;
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }

    private void OnDisable(){
        canShoot = true;
    }
}
