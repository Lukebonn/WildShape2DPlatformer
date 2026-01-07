using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject winUI;

    private void Start()
    {
        winUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            winUI.SetActive(true);
        }
    }
}
