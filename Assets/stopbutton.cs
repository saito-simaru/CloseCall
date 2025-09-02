using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UnityEditor.Searcher;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using UnityEngine.InputSystem;
public class stopbuttom : MonoBehaviour
{
    [Header("UI")]
    public Canvas NormalCanvas;
    public TextMeshProUGUI scoretext;
    [Header("ball")]
    public GameObject ball;
    private Rigidbody ballrb;
    private int waitsecond;
    private bool isgameover = false;
    void Start()
    {
        ballrb = ball.GetComponent<Rigidbody>();
        ballrb.useGravity = false;

        NormalCanvas.gameObject.SetActive(false);

        waitsecond = UnityEngine.Random.Range(5, 11);

        StartCoroutine(begginingball());
    }
    public void Fire(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (isgameover == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        Debug.Log("Stop Button Pressed" + isgameover);
        ballrb.constraints = RigidbodyConstraints.FreezeAll;
        displayScore();
        isgameover = true;

    }

    IEnumerator begginingball()
    {
        yield return new WaitForSeconds(waitsecond);
        ballrb.useGravity = true;
    }

    private void displayScore()
    {
        double score = Math.Round(Math.Abs(ball.transform.position.x - gameObject.transform.position.x), 2);
        Debug.Log(score);
        scoretext.text = $"スコア : {score}\nリトライ : ボタンを押す";
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerON");
        NormalCanvas.gameObject.SetActive(true);
        isgameover = true;
    }
}
