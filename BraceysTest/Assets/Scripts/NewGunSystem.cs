using System.Collections;
using EZCameraShake;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewGunSystem : MonoBehaviour
{
    public float spread;
    public float reloadTime = 1f;
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 50f;
    public float TTF;
    public float camShakeRoughness;
    public float camShakeMagnitude;
    public float camShakeFadeInTime;
    public float camShakeFadeOutTime;

    public int MagSize = 7;
    public int BacAmmo = 14;
    public int CurrAmmo = 0;
    public int dartsPerTap;
    private int dartsShot;

    public Camera FPCamera;
    public ParticleSystem muzzle;
    public GameObject impactHole;
    public GameObject sparks;
    public Animator animator;
    public bool allowButtonHold;
    public bool scopingInAllowed = true;
    private bool isReloading = false;
    private bool shooting;


    NewPlayerControls controls;


    // Start is called before the first frame update
    void OnEnable()
    {
        foreach(Transform part in transform) { if (part.gameObject.GetComponent<MeshCollider>()) { part.gameObject.GetComponent<MeshCollider>().enabled = false; } }

        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        controls = new NewPlayerControls();
        controls.Gameplay.Enable();

        isReloading = false;
        animator.SetBool("Reloading", false);
        print("enabled");

    }
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        UIScript.WeaponName = gameObject.tag;
        CurrAmmo = MagSize;
        BacAmmo = BacAmmo - MagSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null) { OnDrop(); }
        UIScript.WeaponName = this.gameObject.tag;
        UIScript.CurrentAmmo = CurrAmmo.ToString();
        UIScript.BackupAmmo = BacAmmo.ToString();
        if (CurrAmmo == 0) { UIScript.Warnings = "No Ammo";  }
        else if (CurrAmmo <= Mathf.RoundToInt(MagSize / 2)) { UIScript.Warnings = "Low Ammo"; }
        else { UIScript.Warnings = ""; }
        

        if (isReloading == true) { return; }
        if (allowButtonHold != true)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame || Gamepad.current.rightTrigger.wasPressedThisFrame)
            {
                shoot();
                shooting = true;
            }
            else { shooting = false; }
        }
        else
        {
            if (Mouse.current.leftButton.isPressed || Gamepad.current.rightTrigger.isPressed)
            {
                shoot();
                shooting = true;
            }
            else { shooting = false; }
        }
        controls.Gameplay.Reload.started += ctx => StartCoroutine(reload());
    }
    void shoot()
    {
        if (Time.time >= TTF)
        {
            TTF = Time.time + 1f / fireRate;
            while (dartsPerTap != dartsShot)
            {
                if (CurrAmmo != 0)
                {
                    float x = Random.Range(-spread, spread);
                    float y = Random.Range(-spread, spread);

                    //Calculate Direction with Spread
                    Vector3 direction = FPCamera.transform.forward + new Vector3(x, y, 0);

                    CurrAmmo--;
                    muzzle.Play();
                    RaycastHit hit;
                    if (Physics.Raycast(FPCamera.transform.position, direction, out hit, range))
                    {
                        Target target = hit.transform.GetComponent<Target>();
                        if (target != null)
                        {
                            target.TakeDamage(damage);
                        }
                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(-hit.normal *impactForce);
                        }
                        GameObject impactGo = Instantiate(impactHole, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(impactGo, 0.05f);
                    }
                    CameraShaker.Instance.ShakeOnce(camShakeMagnitude, camShakeRoughness, camShakeFadeInTime, camShakeFadeOutTime);
                    //Graphics
                    Instantiate(impactHole, hit.point, Quaternion.identity);
                }
                dartsShot++;
            }
            dartsShot = 0;
        }
        
        
    }


    IEnumerator reload()
    {
        if (CurrAmmo != MagSize && BacAmmo != 0)
        {
            isReloading = true;
            animator.SetBool("Reloading", true);
            Debug.Log("im here");
            yield return new WaitForSeconds(reloadTime);
            while (CurrAmmo != MagSize)
            {
                if (BacAmmo > 0)
                {
                    BacAmmo = BacAmmo - 1;
                    CurrAmmo = CurrAmmo + 1;
                }
                else if (BacAmmo == 0) { break; } 
            }
            animator.SetBool("Reloading", false);
            isReloading = false;
        }
    }

    public void OnDrop()
    {
        foreach (Transform part in transform) { if (part.gameObject.GetComponent<MeshCollider>()) { part.gameObject.GetComponent<MeshCollider>().enabled = true; } }
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        
        UIScript.WeaponName = "-";
        print("disabled");
        controls.Gameplay.Disable();
        this.GetComponent<NewGunSystem>().enabled = false;
    }
}
