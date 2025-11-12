using UnityEngine;

public class LineToMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0;

        Debug.DrawLine(Vector3.zero, mouseWorldPosition, Color.green);

        float angleInDegrees = Mathf.Atan2(mouseWorldPosition.y, mouseWorldPosition.x) * Mathf.Rad2Deg;
        float wrongAngleInDegrees = Mathf.Atan(mouseWorldPosition.y / mouseWorldPosition.x);

        Vector3 wrongVector = new Vector3(Mathf.Cos(wrongAngleInDegrees), Mathf.Sin(wrongAngleInDegrees), 0f);

        Debug.DrawLine(Vector3.zero, wrongVector, Color.red);

        print($"The atan2 angle is: {angleInDegrees} and the atan angle is: {wrongAngleInDegrees * Mathf.Rad2Deg}");
    }
}
