using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private float zoomStep, minCamSize, maxCamSize;
    [SerializeField] private float mapMinX, mapMaxX, mapMinY, mapMaxY, mapMinZ, mapMaxZ;
    private Vector3 dragOrigin;
    [SerializeField] private enum Cam { cam1, cam2 };
    [SerializeField] private Cam selectedCam;

    private void Update()
    {
        PanCamera();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            ZoomIn();

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            ZoomOut();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(1))
            dragOrigin = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - _cam.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = ClampCamera(this.transform.position + difference);
        }
    }

    public void ZoomIn()
    {
        float newSize = _cam.orthographicSize - zoomStep;
        _cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        if (_cam.orthographicSize == newSize)
        {
            mapMinX -= 50;
            mapMinY -= 50;
            mapMinZ -= 50;
            mapMaxX += 50;
            mapMaxY += 50;
            mapMaxZ += 50;
        }
        this.transform.position = ClampCamera(this.transform.position);
    }

    public void ZoomOut()
    {
        float newSize = _cam.orthographicSize + zoomStep;
        _cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        if (_cam.orthographicSize == newSize)
        {
            mapMinX += 50;
            mapMinY += 50;
            mapMinZ += 50;
            mapMaxX -= 50;
            mapMaxY -= 50;
            mapMaxZ -= 50;
        }
        this.transform.position = ClampCamera(this.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPos)
    {
        float newX = Mathf.Clamp(targetPos.x, mapMinX, mapMaxX);
        float newY = Mathf.Clamp(targetPos.y, mapMinY, mapMaxY);
        float newZ = Mathf.Clamp(targetPos.z, mapMinZ, mapMaxZ);

        return new Vector3(newX, newY, newZ);
    }

    public void SwitchCam()
    {

        if (selectedCam == Cam.cam1)
            selectedCam = Cam.cam2;
        else selectedCam = Cam.cam1;

        SetCam();

    }

    private void SetCam()
    {       

        if (selectedCam == Cam.cam1)
        {
            this.transform.eulerAngles = new Vector3(90, 0, 0);
            maxCamSize = 350;
            _cam.orthographicSize = 350;
        }
        if (selectedCam == Cam.cam2)
        {
            this.transform.eulerAngles = new Vector3(30, 45, 0);
            maxCamSize = 250;
            _cam.orthographicSize = 250;
        }
        ResetZoom();
        
    }

    private void ResetZoom()
    {
        this.transform.position = new Vector3(0, 0, 0);
        mapMinX = 0;
        mapMinY = 0;
        mapMinZ = 0;
        mapMaxX = 0;
        mapMaxY = 0;
        mapMaxZ = 0;
    }


}
