using UnityEngine;

public class TowerObject : MonoBehaviour
{
    private bool isUsed = false;
    public bool ISUsed => isUsed;
    private float power = 0f;

    private void Awake()
    {
        power = 10f;
    }

    public void SetUsedState(bool isused)
    {
        isUsed = isused;
        if (!isUsed)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
