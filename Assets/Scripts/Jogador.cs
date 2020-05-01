using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public Classe classeJogador;
    public static event Action OnGameStart;
    public static Jogador Singleton { get; private set; }

    private void Awake()
    {
        classeJogador = new Guerreiro();
        classeJogador.InitialStats();
        CriarSingleton();
        OnGameStart.Invoke();
    }

    private void CriarSingleton()
    {
        if (Singleton == null && Singleton != this)
        {
            Singleton = this;
        }
        else { Destroy(this.gameObject); }
    }
}
