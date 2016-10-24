using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public int AmApple;
    public static EatValue eat;
    public static float light;
    public static Dictionary<string, KeyCode> Snake1Keys = new Dictionary<string, KeyCode>();
    public static Dictionary<string, KeyCode> Snake2Keys = new Dictionary<string, KeyCode>();
    public Material S1, S2, snake1col, snake2col;
    public Image Load, Tutorials;
    void Start()
    {
        if (Snake1Keys.Count == 0)
        {
            AmApple = 1;
            light = 1;
            eat = EatValue.GetAll;
            Snake1Keys["Up"] = KeyCode.W;
            Snake1Keys["Left"] = KeyCode.A;
            Snake1Keys["Right"] = KeyCode.D;
            Snake1Keys["Down"] = KeyCode.S;

            Snake2Keys["Up"] = KeyCode.UpArrow;
            Snake2Keys["Left"] = KeyCode.LeftArrow;
            Snake2Keys["Right"] = KeyCode.RightArrow;
            Snake2Keys["Down"] = KeyCode.DownArrow;
        }

        Load.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && Tutorials.gameObject.activeSelf)
            Tutorials.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        Load.gameObject.SetActive(true);
        SceneManager.LoadScene("Game");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Exit()
    {
        S1.SetColor("_Color", snake1col.color);
        S2.SetColor("_Color", snake2col.color);
        S1.name = "Green Snake";
        S2.name = "Purple Snake";
        Application.Quit();
    }

    public void LoadTutorials()
    {
        Tutorials.gameObject.SetActive(true);
    }
}
public enum EatValue
{
    GetAll,
    GetOne,
    GetNothing
}
