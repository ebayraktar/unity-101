using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private Camera _cam;

    public Transform playerTransform;

    // Start is called before the first frame update
    public void Start()
    {
        //_cam = Camera.main;
        Debug.Log("Hello World!");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // var position = playerTransform.position;
            // position = new Vector3(position.x + 0.1f, position.y, position.z);
            var position = playerTransform.transform.position + Vector3.forward;
            playerTransform.position = position;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // var position = playerTransform.position;
            // position = new Vector3(position.x - 0.1f, position.y, position.z);
            var position = playerTransform.transform.position + Vector3.back;
            playerTransform.position = position;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            var rotation = playerTransform.rotation;
            rotation = new Quaternion(rotation.x, rotation.y - 0.1f, rotation.z, rotation.w);
            playerTransform.rotation = rotation;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            var rotation = playerTransform.rotation;
            Debug.Log(rotation.w);
            rotation = new Quaternion(rotation.x, rotation.y + 0.1f, rotation.z, rotation.w);
            playerTransform.rotation = rotation;
        }
    }
}