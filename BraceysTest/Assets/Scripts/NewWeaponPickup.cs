using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeaponPickup : MonoBehaviour
{
    public float distance = 10.0f;
    public Transform equipPosition;
    public Transform dropPosition;
    GameObject currWeapon;
    public GameObject weaponInHand;
    bool canGrab;
    NewPlayerControls controls;
    // Start is called before the first frame update
    void Start()
    {
        controls = new NewPlayerControls();
        controls.Gameplay.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        checkGrab();
        
        if(canGrab){
            if(weaponInHand == null){
                if(controls.Gameplay.Pickup.IsPressed()){
                    pickUp();
                    Debug.Log("grabbing");
                }
            }
            //else{if(controls.Gameplay.Drop.IsPressed()){Drop();  pickUp();}}
        }
        //if(controls.Gameplay.Drop.IsPressed()){Drop();}
    }

    private void checkGrab(){
        RaycastHit hit;

        if(Physics.Raycast(transform.position,transform.forward, out hit,distance)){
            if (hit.transform.tag == "canGrab"){
                Debug.Log("i can grab it");
                currWeapon = hit.transform.gameObject;
                canGrab = true;
            }
            else
            {
                canGrab = false;
            }
        }
    }

    private void pickUp(){
        currWeapon.GetComponent<BoxCollider>().enabled = false;
        
        currWeapon.transform.position = equipPosition.position;
        currWeapon.transform.parent = equipPosition;
        currWeapon.transform.localEulerAngles = new Vector3(0f,0f,0f);
        currWeapon.GetComponent<Rigidbody>().isKinematic=true;
        currWeapon.GetComponent<NewGunSystem>().enabled = true;
        weaponInHand = currWeapon.gameObject;
        Debug.Log("grabbed it");
    }
    void Drop(){
        weaponInHand.GetComponent<BoxCollider>().enabled = true;
        weaponInHand.GetComponent<Rigidbody>().isKinematic=false;
        weaponInHand.GetComponent<NewGunSystem>().enabled = false;
        weaponInHand.transform.position = dropPosition.transform.position;
        weaponInHand.transform.parent = null;
        weaponInHand = null;
    }
}
