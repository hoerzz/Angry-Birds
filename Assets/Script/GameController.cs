using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    public BoxCollider2D TapCollider;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Text _statusInfo;

    public List<Bird> Birds;
    public List<Enemy> Enemies;

    private Bird _shotBird;
    public bool _isGameEnded = false;

    void Start()
    {
        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }
    void Update() {
        if (Input.GetKeyDown (KeyCode.R))
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
            Time.timeScale = 1.0f;
        }
        if (_isGameEnded)
        {
            return;
        }
    }
    public void ChangeBird()
    {
        TapCollider.enabled = false;
        if (_isGameEnded)
        {
            return;
        }
        Birds.RemoveAt(0);

        if(Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
        }
        if(Birds.Count == 0)
        {
            _isGameEnded = true;
            SetGameOver (false);
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if(Enemies.Count == 0)
        {
            _isGameEnded = true;
            SetGameOver (true);
            Time.timeScale = 0f;
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }
    public void SetGameOver (bool isWin)
    {
        _statusInfo.text = isWin ? "You Win!" : "You Lose!";
        _panel.gameObject.SetActive (true);
    }

    void OnMouseUp()
    {
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Anda Sudah Keluar");
    }
    public void nextLevelone()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void nextLevelTwo()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void nextLevelThree()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    
}
