using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorInterface : MonoBehaviour
{
    public IS infoBotaoAtual;
    public Button botaoAtacar;
    public Button botaoDefender;
    public Button botaoHabilidade1;
    public Button botaoHabilidade2;
    public bool avancar;
    public Text infoPanel;
    public Text textMana;
    public Text textVida;
    public Text textVit;
    public Text textInt;
    public Text textFor;
    public Text textDes;
    public Text textVel;

    public void Status()
    {
        textVit.text = Jogador.Singleton.classeJogador.GetVit.ToString();
        textInt.text = Jogador.Singleton.classeJogador.GetInt.ToString();
        textFor.text = Jogador.Singleton.classeJogador.GetFor.ToString();
        textDes.text = Jogador.Singleton.classeJogador.GetDes.ToString();
        textVel.text = Jogador.Singleton.classeJogador.GetVel.ToString();
        textVida.text = Jogador.Singleton.classeJogador.Vida.ToString();
        textMana.text = Jogador.Singleton.classeJogador.Mana.ToString();
    }

    public void BotaoAtacar()
    {
        infoBotaoAtual = Jogador.Singleton.classeJogador.Atacar();

        Jogador.Singleton.classeJogador.boolADB = new BBB(true, false, false);

        Interactable(false);

        Status();
    }

    public void BotaoDefender()
    {
        infoBotaoAtual = Jogador.Singleton.classeJogador.Defender();

        Jogador.Singleton.classeJogador.boolADB = new BBB(false, true, false);

        Interactable(false);
    }

    public void BotaoHabilidade1()
    {
        infoBotaoAtual = Jogador.Singleton.classeJogador.Habilidade1();

        Jogador.Singleton.classeJogador.boolADB = new BBB(false, false, true);

        Interactable(false);

        Status();
    }

    public void BotaoHabilidade2()
    {
        infoBotaoAtual = Jogador.Singleton.classeJogador.Habilidade2();

        Jogador.Singleton.classeJogador.boolADB = new BBB(true, false, false);

        Interactable(false);
    }

    public void BotaoAvancar() 
    {
        avancar = true;
    }

    public void Interactable(bool isOn)
    {
        botaoAtacar.interactable = isOn;
        botaoDefender.interactable = isOn;
        botaoHabilidade1.interactable = isOn;
        botaoHabilidade2.interactable = isOn;
    }

    private void OnEnable()
    {
        Jogador.OnGameStart += Status; 
    }

    private void OnDisable()
    {
        Jogador.OnGameStart -= Status;
    }
}
