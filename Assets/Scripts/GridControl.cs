using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] float verticalSpacing;
    [SerializeField] float scrollMagnitude;
    [SerializeField] Transform defaultCamPos;
    [SerializeField] float correctionSpeed;

    Camera cam;
    bool shouldFix = false;
    float topBound;
    float targetHeight;

    private void Awake()
    {
        cam = Camera.main;
        cam.transform.position = defaultCamPos.position;
        topBound = defaultCamPos.position.y;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                shouldFix = false;
                Vector2 delta = Input.GetTouch(0).deltaPosition;
                float move = -delta.y * scrollMagnitude;

                if (cam.transform.position.y + move < topBound && cam.transform.position.y + move > defaultCamPos.position.y - (App.playerBehaviour.GetTheatreCount() - 3)* verticalSpacing)
                    cam.transform.position += new Vector3(0, move, 0);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                shouldFix = true;
                float realDistance = -cam.transform.position.y + topBound;
                int row = Mathf.RoundToInt(realDistance / verticalSpacing);

                targetHeight = topBound - row * verticalSpacing;
            }
        }

        if (shouldFix)
        {
            if (targetHeight < cam.transform.position.y)
            {
                Debug.Log("down");
                cam.transform.Translate(new Vector3(0, -correctionSpeed, 0) * Time.deltaTime);
                if (targetHeight >= cam.transform.position.y)
                {
                    cam.transform.position = new Vector3(defaultCamPos.position.x, targetHeight, defaultCamPos.position.z);
                    shouldFix = false;
                }
            }
            else
            {
                Debug.Log("up");
                cam.transform.Translate(new Vector3(0, correctionSpeed, 0) * Time.deltaTime);
                if (targetHeight <= cam.transform.position.y)
                {
                    cam.transform.position = new Vector3(defaultCamPos.position.x, targetHeight, defaultCamPos.position.z);
                    shouldFix = false;
                }

            }
        }
    }
}