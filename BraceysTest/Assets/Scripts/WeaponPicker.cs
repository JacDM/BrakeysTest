using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponPicker : MonoBehaviour
{
    private GameObject hand;
    public GameObject dropPoint;

    public Vector3 scaleChange;

    private Transform currHand;

    NewPlayerControls controls;

    void Awake(){ controls = new NewPlayerControls(); }
    void OnEnable() { controls.Gameplay.Enable(); }
    void OnDisable() { controls.Gameplay.Disable(); }
    

    void Update()
    {
        hand = WeaponSwitching.hand;
        if(Keyboard.current.qKey.wasPressedThisFrame){Drop();}
        int i = 0;
    }

    public void Pickup(Collision col)
    {
        SelectHand();
        Drop();
        col.rigidbody.useGravity=false;
        col.transform.parent = hand.transform;
        col.rigidbody.constraints= RigidbodyConstraints.FreezeAll;
        UIScript.PickupText = "";
        col.gameObject.GetComponent<NewGunSystem>().enabled = true;
        col.transform.localScale = col.transform.localScale-scaleChange;

        col.transform.localPosition = Vector3.zero;
        col.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.layer == 11)
        {
            UIScript.PickupText = "Press E to pickup " + col.gameObject.tag;
            if (Keyboard.current.eKey.wasPressedThisFrame || Gamepad.current.rightShoulder.wasPressedThisFrame) { Pickup(col); }
        }
    }

    void OnCollisionExit(Collision collisionInfo) { UIScript.PickupText = ""; }
    void SelectHand()
    {
        hand = WeaponSwitching.hand;
        currHand = hand.gameObject.transform;
    }
    void Drop()
    {
        print("hiyou");
        hand.GetComponentInChildren<NewGunSystem>().OnDrop();
        int i = 0;
        foreach (Transform weap in currHand)
        {
            i = i + 1;
            weap.transform.localScale = weap.transform.localScale + scaleChange;
            weap.transform.parent = null;
            weap.transform.position = dropPoint.transform.position;
        }
    }
}