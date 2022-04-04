using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{

   // public static int SelectedWeapon = -1;
    public GameObject Slot0;
    public GameObject Slot1;
    public static int currSlot;
    public Animator animator2;
    public static GameObject hand;
    NewPlayerControls controls;



    // Start is called before the first frame update
    void Start()
    {
        currSlot = 0;
        SelectWeapon();
        hand = Slot0;
    }
    void Awake()
    {
        controls = new NewPlayerControls();
        controls.Gameplay.SwitchSlots.performed += ctx => Switching();
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform Slot in transform)
        {
            if (i == currSlot)
            {
                Slot.gameObject.SetActive(true);
                if (Slot.childCount > 0)
                {
                    Slot.GetChild(0).gameObject.GetComponent<NewGunSystem>().enabled = true;
                }
            }
            else
            {
                if(Slot.childCount > 0)
                {
                    Slot.GetChild(0).gameObject.GetComponent<NewGunSystem>().enabled = false;
                }
                Slot.gameObject.SetActive(false);
            }
            i++;
        }
    }

    void Switching()
    {
        if (currSlot == 0)
        {
            currSlot = 1;
            hand = Slot1;
        }
        else
        {
            currSlot = 0;
            hand = Slot0;
        }
        SelectWeapon();
    }
}
