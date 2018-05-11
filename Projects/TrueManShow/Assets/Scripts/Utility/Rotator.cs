using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 Speed;

    public void SetRotate(bool value)
    {
        enabled = value;
    }
    
    private void LateUpdate()
    {
        transform.Rotate(Speed * Time.deltaTime);
    }
}
