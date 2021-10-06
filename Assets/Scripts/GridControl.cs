using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] float verticalSpacing;
    [SerializeField] float swipeSpeed;

    float horizontalSpacing;

    Transform[] rows;
    int[] lengths;

    Camera cam;
    bool isMovingV = false;
    bool isMovingH = false;

    int theatreIndex = 0;
    int horizontalIndex = 0;

    private bool tapRequested;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    private void Start()
    {
        cam = Camera.main;
        TheatreBehaviour[] temp = GetComponentsInChildren<TheatreBehaviour>();
        horizontalSpacing = 5.62f;
        rows = new Transform[temp.Length];
        lengths = new int[temp.Length];

        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = temp[i].transform;
            lengths[i] = temp[i].GetEmpCount();
        }
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
        if (isMovingH)
            StopCoroutine("MoveTheatre");

        if (right)
            IncHI(false);
        else
            IncHI(true);

        StartCoroutine(MoveTheatre(-horizontalIndex * horizontalSpacing, theatreIndex));
    }

    IEnumerator MoveTheatre(float targetX, int index)
    {
        if (isMovingV || index >= rows.Length)
            yield break;

        isMovingH = true;
        bool right = targetX > rows[index].position.x;

        if (right)
            while (targetX > rows[index].position.x)
            {
                rows[index].Translate(Vector3.right * Time.deltaTime * swipeSpeed);
                yield return new WaitForEndOfFrame();
            }
        else
            while (targetX < rows[index].position.x)
            {
                rows[index].Translate(Vector3.left * Time.deltaTime * swipeSpeed);
                yield return new WaitForEndOfFrame();
            }

        rows[index].position = new Vector3(targetX, rows[index].position.y, rows[index].position.z);

        isMovingH = false;
        yield return 0;
    }

    void SwipeVertical(bool up)
    {
        if (isMovingV)
            StopCoroutine("MoveCam");

        if (up)
            IncTI(true);
        else
            IncTI(false);

        StartCoroutine(MoveCam(-theatreIndex * verticalSpacing));
    }

    IEnumerator MoveCam(float targetHeight)
    {
        isMovingV = true;
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

        isMovingV = false;
        horizontalIndex = 0;
        yield return 0;
    }

    void IncTI(bool inc)
    {
        if (inc && theatreIndex + 2 < App.playerBehaviour.GetTheatreCount())
        {
            StartCoroutine(MoveTheatre(0, theatreIndex));
            theatreIndex++;
        }   
        else if (!inc && theatreIndex > 0)
        {
            StartCoroutine(MoveTheatre(0, theatreIndex));
            theatreIndex--;
        }
    }
    
    void IncHI(bool inc)
    {
        if (theatreIndex >= lengths.Length)
            return;

        if (inc && horizontalIndex < lengths[theatreIndex])
            horizontalIndex++;
        else if (!inc && horizontalIndex > 0)
            horizontalIndex--;
    }
}