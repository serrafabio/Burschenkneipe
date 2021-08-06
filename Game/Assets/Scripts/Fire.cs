using UnityEngine;

public class Fire : MonoBehaviour
{
    public float firespeed;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up*firespeed*Time.deltaTime, Space.Self); 
    }

}
