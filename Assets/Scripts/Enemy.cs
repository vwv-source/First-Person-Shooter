using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject player;
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject bullet;
    public float health = 20f;
    public bool canShoot = true;

    public float bullets = 30f;
    public float maxbullets = 30f;

    [SerializeField] LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weapon.transform.LookAt(player.transform);
        //this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 1 * Time.deltaTime);
        this.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);

        RaycastHit ray;
        if (Physics.Raycast(barrel.transform.position, barrel.transform.TransformDirection(Vector3.forward), out ray, Mathf.Infinity, playerLayer)){
            if(ray.collider.gameObject.tag == "player" && canShoot){
                if(bullets == 0 || bullets < 0){
                    StartCoroutine(reload());
                    return;
                }
                StartCoroutine(shoot());
            }
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "bullet"){
            health -= collision.gameObject.GetComponent<Bullet>().damage;
            if(health == 0 || health < 0){
                Destroy(this.gameObject);
            }
        }   
    }

    IEnumerator shoot(){
        GameObject bulletclone = Instantiate(bullet,barrel.transform.position, barrel.transform.rotation);
        bulletclone.GetComponent<Rigidbody>().velocity = barrel.transform.forward * 50;
        bullets -= 1f;
        canShoot = false;
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }

    IEnumerator reload(){
        yield return new WaitForSeconds(5f);
        bullets = maxbullets;
    }

    private void OnDisable(){
        canShoot = true;
    }
}
