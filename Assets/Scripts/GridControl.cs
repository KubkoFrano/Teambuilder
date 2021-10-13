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
    EmployeeGenerator[] generators;

    Camera cam;
    bool isMovingV = false;
    bool isMovingH = false;

    int theatreIndex = 0;
    int horizontalIndex = 0;

    private bool tapRequested;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    bool canSwipe = true;

    private void Awake()
    {
        App.gridControl = this;

        cam = Camera.main;
        TheatreBehaviour[] temp = GetComponentsInChildren<TheatreBehaviour>();
        horizontalSpacing = 5.62f;
        rows = new Transform[temp.Length];
        lengths = new int[temp.Length];
        generators = new EmployeeGenerator[temp.Length];

        foreach (TheatreBehaviour t in temp)
            t.enabled = true;

        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = temp[i].transform;
            lengths[i] = temp[i].GetEmpCount();
            generators[i] = temp[i].gameObject.GetComponent<EmployeeGenerator>();
            generators[i].SetIndex(i);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            //Mobile input
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
        else if (Input.GetMouseButtonDown(0))
        {
            //PC input
            Touch(Input.mousePosition);
        }

        swipeDelta = Vector2.zero;
        if (isDragging && Input.touchCount > 0)
            swipeDelta = Input.touches[0].position - startTouch;

        if (canSwipe && swipeDelta.magnitude > 100)
        {
            //Mobile input
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
        else if (canSwipe)
        {
            //PC input
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                SwipeHorizontal(true);
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                SwipeHorizontal(false);
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                SwipeVertical(false);
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                SwipeVertical(true);
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
            return;

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
            return;

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

        if (inc && horizontalIndex < lengths[theatreIndex] + 1 && rows[theatreIndex].gameObject.GetComponent<TheatreBehaviour>().IsUnlocked())
            horizontalIndex++;
        else if (!inc && horizontalIndex > 0)
            horizontalIndex--;
    }

    int FindIndex(Transform theatreTransform)
    {
        for (int i = 0; i < rows.Length; i++)
            if (rows[i] == theatreTransform)
                return i;

        return 0;
    }

    public void ResfreshLength(Transform theatreTransform, int newLength)
    {
        lengths[FindIndex(theatreTransform)] = newLength;
    }

    public void PositionEBB(Transform theatreTransform, Transform button, float offset)
    {
        float x = (lengths[FindIndex(theatreTransform)] + 1) * offset;
        button.localPosition = new Vector3(x, button.localPosition.y, button.localPosition.z);
    }

    public void RefreshEmpPositions(List<Employee> emps, float offset)
    {
        List<Transform> empTransforms = new List<Transform>();

        for (int i = 0; i < emps.Count; i++)
        {
            Transform t = emps[i].GetTransform();
            t.localPosition = new Vector3((i + 1) * offset, t.localPosition.y, t.localPosition.z);
        }
    }

    public void SetSwipe(bool value)
    {
        canSwipe = value;
    }

    public EmployeeGenerator GetCurrentGenerator()
    {
        return generators[theatreIndex];
    }

    public int GetTheatreIndex()
    {
        return theatreIndex;
    }
}