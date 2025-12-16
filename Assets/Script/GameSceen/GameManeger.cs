using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameManeger Instance;

    public GameState CurrentState { get; private set; }


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SetState(GameState.Titel);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetState(GameState state)
    {
        CurrentState = state;

        Time.timeScale = (state == GameState.Playing) ? 1f : 0f;
    }
}
