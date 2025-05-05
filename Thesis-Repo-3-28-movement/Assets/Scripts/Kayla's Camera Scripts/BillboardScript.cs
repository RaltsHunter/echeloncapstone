using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }
}
