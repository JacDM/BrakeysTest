using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScript : MonoBehaviour
{
    public static string WeaponName;
    public TextMeshProUGUI UIWeaponName;
    public static string CurrentAmmo;
    public TextMeshProUGUI UICurrentAmmo;
    public static string BackupAmmo;
    public TextMeshProUGUI UIBackupAmmo;
    public static string Warnings;
    public TextMeshProUGUI UIWarnings;
    public static string PickupText;
    public TextMeshProUGUI UIPickupText;
    public TextMeshProUGUI UIFPSCounter;

    private float timer;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        UIFPSCounter.text = "FPS: " + ((int)fps).ToString();
        UIWeaponName.text = WeaponName;
        UICurrentAmmo.text = CurrentAmmo;
        UIBackupAmmo.text = BackupAmmo;
        UIWarnings.text = Warnings;
        UIPickupText.text = PickupText;

        if(Warnings == "") { return; }
        timer = timer + Time.deltaTime;
        if (timer >= 0.5)
        {
            UIWarnings.enabled = true;
        }
        if (timer >= 1)
        {
            UIWarnings.enabled = false;
            timer = 0;
        }
    }
}
