using UnityEngine;
using System.Collections;

public class BulletHoleScript : MonoBehaviour
{

    public float maxDist = 1000000000f;
    public GameObject decalHitWall;
    float floatInFrontOfWall = 0.00001f;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDist))
        {
            Instantiate(decalHitWall, hit.point + (hit.normal * floatInFrontOfWall), Quaternion.LookRotation(hit.normal),hit.transform);
        }
    }
}
