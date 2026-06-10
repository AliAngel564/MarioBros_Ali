using UnityEngine;

public class Flagpole : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            FindAnyObjectByType<WinMenu>().ShowWin();
    }
}