using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shotgun : MonoBehaviour
{
    [SerializeField] GameObject maincamera;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject barrel2;
    [SerializeField] TextMeshProUGUI ammotext;
    [SerializeField] GameObject startlocation;
    [SerializeField] GameObject aimlocation;

    public float bullets = 7f;
    public float magazines = 4f;
    public float magazinemax = 7f;
    public float maxmagazines = 4f;
    public float nextActionTime = 0.0f;

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

        if(Input.GetMouseButton(0) && canShoot){
            if(bullets == 0 || bullets < 0){
                return;
            }
            StartCoroutine(shoot());
        }

        if(Input.GetKeyDown(KeyCode.R)){
            if(bullets > 0 || magazines == 0){
                return;
            }
            this.GetComponent<Animation>().Play("ShotgunReload");
            magazines -= 1f;
            bullets = magazinemax;
        }
    }

    IEnumerator shoot(){
        this.GetComponent<Animation>().Play("Shotgun Recoil");
        barrel.GetComponent<ParticleSystem>().Play();
        bullets -= 1f;
        GameObject bulletclone = Instantiate(bullet,barrel.transform.position, barrel.transform.rotation);
        GameObject bulletclone2 = Instantiate(bullet,barrel2.transform.position, barrel2.transform.rotation);
        bulletclone.GetComponent<Rigidbody>().velocity = barrel.transform.forward * 50;
        bulletclone.GetComponent<Bullet>().damage = 3f;
        bulletclone2.GetComponent<Rigidbody>().velocity = barrel2.transform.forward * 50;
        bulletclone2.GetComponent<Bullet>().damage = 3f;
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    private void OnDisable(){
        canShoot = true;
    }
}
