using UnityEngine;
using UnityEngine.InputSystem;

public class PCPlayer : MonoBehaviour
{
    [SerializeField]
    float lookUpSpeed = 1, lookSideSpeed = 1;
    [SerializeField]
    bool invertLook = false;
    [SerializeField]
    Transform headT;

    bool looking;
    Vector2 lookDir;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float ft = Time.deltaTime;
        //move head
        if (IsLooking())
        {
            Vector2 looking = LookDirection();
            headT.Rotate(Vector3.up, looking.x * lookSideSpeed * ft, Space.World);
            int invert = invertLook ? 1 : -1;
            headT.Rotate(Vector3.right, invert * looking.y * lookUpSpeed * ft, Space.Self);
        }
    }

    public void OnStartLook(InputValue v)
    {
        looking = v.isPressed;
    }

    public void OnLook(InputValue v)
    {
        lookDir = v.Get<Vector2>();
    }

    public bool IsLooking()
    {
        return looking;
    }

    public Vector2 LookDirection()
    {
        return lookDir;
    }
}
