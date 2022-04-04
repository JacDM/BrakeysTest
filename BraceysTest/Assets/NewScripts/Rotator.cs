using UnityEngine.InputSystem;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    //Make sure you attach a Rigidbody in the Inspector of this GameObject
    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    public float Val=180;
    public float angularVelocity = 360;
    public int CurrVal = 2700;
    public int counter = 0;


    private float flippedAngle = 1f;
    private float unFlippedAngle = 559f;
    void Start()
    {
        //Set the axis the Rigidbody rotates in (100 in the y axis)
        m_EulerAngleVelocity = new Vector3(Val, 0, 0);

        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
    }
    private void Update()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            
        }
    }
    void rotator()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime * angularVelocity);
        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        counter = counter + 1;
        print(Mathf.RoundToInt(transform.rotation.eulerAngles[0]));
    }
}
