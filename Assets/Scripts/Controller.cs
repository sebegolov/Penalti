using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _penaltiMenu;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GoalTriger _gate;
    
    [SerializeField] private StateAnimationController _manController;
    [SerializeField] private StateAnimationController _girlController;
    
    private GameMenu _menuUI;

    private bool _mainMenu = true;
    private bool _goal = false;
    private GameState _currentGameState = GameState.Start;
    static Random GetRandom = new Random();

    private int _score = 0;
    private int _countPenalti = 0;

    private void Start()
    {
        _menuUI = _penaltiMenu.GetComponent<GameMenu>();
        _gate.Goal += Goal;
        ShowMenu();
    }

    private void Goal()
    {
        if (!_goal && _score < 5)
        {
            _score++;
            _goal = true;
            _menuUI.SetResult(_score);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LeftKick()
    {
        _ball.GetComponent<Kick>().SetSide(Side.Left);
        ControlPenaltiMenu();
    }

    public void RightKick()
    {
        _ball.GetComponent<Kick>().SetSide(Side.Right);
        ControlPenaltiMenu();
    }

    public void Kick()
    {
        _countPenalti++;
        _goal = false;
        _girlController.SetState(1);
        StartCoroutine(KickWaiting());
        ControlPenaltiMenu();
        StartCoroutine(RestartAnimationWaiting());
    }

    public void NextKick()
    {
        _goal = false;
        if (_countPenalti == 5)
        {
            _countPenalti = 0;
            ShowMenu(true);
        }
        _ball.GetComponent<Kick>().ResetParameters();
        ControlPenaltiMenu();
    }

    public void ShowMenu(bool MainMenu)
    {
        if (MainMenu)
        {
            _currentGameState = GameState.Start;
            _score = 0;
        }
        _mainMenu = MainMenu;
        ShowMenu();
    }

    public void SetState(GameState state)
    {
        _currentGameState = state;
    }

    private IEnumerator KickWaiting()
    {
        yield return new WaitForSeconds(0.71f);
        _ball.GetComponent<Kick>().KickBall();
        _manController.SetState(GetRandom.Next(0,100)%2 + 1);
    }

    private IEnumerator RestartAnimationWaiting()
    {
        yield return new WaitForSeconds(3);
        _manController.SetState(0);
        _girlController.SetState(0);
    }

    private void ShowMenu()
    {
        _startMenu.SetActive(_mainMenu);
        _penaltiMenu.SetActive(!_mainMenu);
        ControlPenaltiMenu();
    }

    public void ControlPenaltiMenu()
    {
        if (!_mainMenu)
        {
            _menuUI.SetResult(_score);

            switch (_currentGameState)
            {
                case GameState.Start:
                {
                    _menuUI.ShowKickButton(false);
                    _menuUI.ShowNextButton(false);
                    _menuUI.ShowSideButton(true);
                    _currentGameState = GameState.SideChoise;
                    break;
                }
                case GameState.SideChoise:
                {
                    _menuUI.ShowKickButton(true);
                    _menuUI.ShowNextButton(false);
                    _menuUI.ShowSideButton(false);
                    _currentGameState = GameState.Kick;
                    break;
                }
                case GameState.Kick:
                {
                    _menuUI.ShowKickButton(false);
                    _menuUI.ShowNextButton(true);
                    _menuUI.ShowSideButton(false);
                    _currentGameState = GameState.Start;
                    break;
                }
            }
        }
    }

    public enum GameState
    {
        Start, SideChoise, Kick
    }
}
