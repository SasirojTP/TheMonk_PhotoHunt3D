using UnityEngine;

public class RotateObject : MonoBehaviour
{
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    [SerializeField] Transform rotatePivot;
    GameManager.CameraState referenceCameraState;
    float rotateSpeed = 0.5f;

    private void Update()
    {
        Rotate();
    }
    void Rotate()
    {
        if (GameManager.inst.cameraState == referenceCameraState)
        {
            if (Input.touchCount > 0)
            {
                GameManager.inst.SetRotateState(GameManager.RotateState.Rotating);
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    mPosDelta = Input.mousePosition - mPrevPos;
                    if (Vector3.Dot(transform.up, Vector3.up) >= 0)
                    {
                        rotatePivot.Rotate(transform.up, -Vector3.Dot(mPosDelta * rotateSpeed, Camera.main.transform.right), Space.World);
                    }
                    else
                    {
                        rotatePivot.Rotate(transform.up, Vector3.Dot(mPosDelta * rotateSpeed, Camera.main.transform.right), Space.World);
                    }

                    rotatePivot.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta * rotateSpeed, Camera.main.transform.up), Space.World);
                }
                else
                {
                    GameManager.inst.SetRotateState(GameManager.RotateState.NotRotate);
                    print("Not Rotate");
                }
            }

            if (Input.GetMouseButton(0) && Input.touchCount <= 0)
            {
                GameManager.inst.SetRotateState(GameManager.RotateState.Rotating);
                mPosDelta = Input.mousePosition - mPrevPos;
                if (Vector3.Dot(transform.up, Vector3.up) >= 0)
                {
                    rotatePivot.Rotate(transform.up, -Vector3.Dot(mPosDelta * rotateSpeed, Camera.main.transform.right), Space.World);
                }
                else
                {
                    rotatePivot.Rotate(transform.up, Vector3.Dot(mPosDelta * rotateSpeed, Camera.main.transform.right), Space.World);
                }

                rotatePivot.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta * rotateSpeed, Camera.main.transform.up), Space.World);
            }
            if(Input.GetMouseButtonUp(0))
            {
                GameManager.inst.SetRotateState(GameManager.RotateState.NotRotate);
                print("Not Rotate");
            }
        }

        mPrevPos = Input.mousePosition;
    }
    public void SetReferenceCameraState(GameManager.CameraState ReferenceCameraState)
    {
        referenceCameraState = ReferenceCameraState;
    }
}
