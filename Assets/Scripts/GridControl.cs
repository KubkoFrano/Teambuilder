using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] float verticalSpacing;
    [SerializeField] float swipeSpeed;

    Camera cam;
    bool moving = false;

    int theatreIndex = 0;

    private bool tapRequested;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tapRequested = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                if (tapRequested)
                    Touch(Input.GetTouch(0).position);
                isDragging = false;
                Reset();
            }
        }

        swipeDelta = Vector2.zero;
        if (isDragging && Input.touchCount > 0)
            swipeDelta = Input.touches[0].position - startTouch;

        if (swipeDelta.magnitude > 100)
        {
            tapRequested = false;
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x > 0)
                    SwipeHorizontal(true);
                else
                    SwipeHorizontal(false);
            }
            else
            {
                if (y > 0)
                    SwipeVertical(true);
                else
                    SwipeVertical(false);
            }
            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    void Touch(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position), Vector2.zero);

        if (hit && hit.collider != null)
        {
            ITouchable i = hit.collider.gameObject.GetComponent<ITouchable>();
            if (i != null)
                i.OnTouch();
        }
    }

    void SwipeHorizontal(bool right)
    {
        Debug.Log("horizontal");
    }

    void SwipeVertical(bool up)
    {
        if (moving)
            StopCoroutine("MoveCam");

        if (up)
            IncTI(true);
        else if (!up && theatreIndex > 0)
            IncTI(false);

        StartCoroutine(MoveCam(-(theatreIndex) * verticalSpacing));
    }

    IEnumerator MoveCam(float targetHeight)
    {
        Debug.Log("move");
        moving = true;
        bool up = targetHeight > cam.transform.position.y;

        if (up)
            while (targetHeight > cam.transform.position.y)
            {
                cam.transform.Translate(Vector3.up * Time.deltaTime * swipeSpeed);
                yield return new WaitForEndOfFrame();
            }
        else
            while (targetHeight < cam.transform.position.y)
            {
                cam.transform.Translate(Vector3.down * Time.deltaTime * swipeSpeed);
                yield return new WaitForEndOfFrame();
            }

        cam.transform.position = new Vector3(0, targetHeight, -10);

        moving = false;
        yield return 0;
    }

    void IncTI(bool inc)
    {
        if (inc && theatreIndex + 2 < App.playerBehaviour.GetTheatreCount())
            theatreIndex++;
        else if (!inc && theatreIndex > 0)
            theatreIndex--;
    }
}