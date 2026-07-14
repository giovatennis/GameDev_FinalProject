using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform target;
    private float height = 40f;

    private void LateUpdate()
    {
        if(target == null)
        {
            return;
        }
        transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z);
        transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
