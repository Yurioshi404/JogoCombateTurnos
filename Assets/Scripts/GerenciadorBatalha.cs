using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorBatalha : MonoBehaviour
{
    public Inimigo inimigoAtual;
    private GerenciadorInterface gerenciadorInterface;
    public static event Action OnBattleBegin;
    public static event Action OnBattleEnds;

    private void Awake()
    {
        gerenciadorInterface = GetComponent<GerenciadorInterface>();
        NovoInimigo();
        inimigoAtual.InitialStats();
        StartBatalha();
    }

    private void NovoInimigo()
    {
        int aleatorio = UnityEngine.Random.Range(1, 6);

        Inimigo slime = new Inimigo("Slime", "FOR", 2, 1, 2, 1, 1);
        Inimigo goblin = new Inimigo("Goblin", "FOR", 2, 1, 2, 1, 2);
        Inimigo orc = new Inimigo("Orc", "FOR", 3, 1, 2, 3, 1);
        Inimigo spellcaster = new Inimigo("Spellcaster", "INT", 1, 4, 1, 2, 2);
        Inimigo lich = new Inimigo("Lich", "INT", 1, 7, 1, 1, 3);

        switch (aleatorio)
        {
            case 1:
                inimigoAtual = slime;
                break;
            case 2:
                inimigoAtual = goblin;
                break;
            case 3:
                inimigoAtual = orc;
                break;
            case 4:
                inimigoAtual = spellcaster;
                break;
            case 5:
                inimigoAtual = lich;
                break;
        }
    }

    private void StartBatalha()
    {
        StartCoroutine(Batalha());
    }

    private IEnumerator Batalha()
    {
        //int manaMax = Jogador.Singleton.classeJogador.Mana;

        while(Jogador.Singleton.classeJogador.Vida >= 0 || inimigoAtual.Vida >= 0)
        {
            int turno = Turnos() + 1;

            bool proximoJogador = false;
            bool proximoInimigo = false;

            int velInimigo = inimigoAtual.GetVel;
            int velJogador = Jogador.Singleton.classeJogador.GetVel;

            if (Jogador.Singleton.classeJogador.Mana < Jogador.Singleton.classeJogador.GetManaMax)
            {
                Jogador.Singleton.classeJogador.Mana += Jogador.Singleton.classeJogador.GetInt;
            }

            IS infosInimigoIA = new IS(0, null);

            gerenciadorInterface.Interactable(true);

            if (velJogador > velInimigo)
            {
                gerenciadorInterface.infoPanel.text = "Sua Vez";
                if(gerenciadorInterface.infoBotaoAtual.valDois == null) { yield return null; }
                StartWait();
                gerenciadorInterface.infoPanel.text = gerenciadorInterface.infoBotaoAtual.valDois;
                proximoInimigo = true;
                proximoJogador = false;
            }
            else if (velJogador < velInimigo)
            {
                infosInimigoIA = inimigoAtual.InimigoIA();
                gerenciadorInterface.infoPanel.text = infosInimigoIA.valDois;
                proximoInimigo = false;
                proximoJogador = true;
            }

            StartWait();

            if (proximoInimigo == true && proximoJogador == false)
            {
                infosInimigoIA = inimigoAtual.InimigoIA();
                gerenciadorInterface.infoPanel.text = infosInimigoIA.valDois;
                infosInimigoIA = inimigoAtual.InimigoIA();
            }
            else if (proximoJogador == true && proximoInimigo == false)
            {
                gerenciadorInterface.infoPanel.text = "Sua Vez";
                if (gerenciadorInterface.infoBotaoAtual.valDois == null) { yield return null; }
                StartWait();
                gerenciadorInterface.infoPanel.text = gerenciadorInterface.infoBotaoAtual.valDois;
            }

            StartWait();

            string resultado = VerificadorAcao(infosInimigoIA.valUm);

            gerenciadorInterface.Status();

            gerenciadorInterface.infoPanel.text = resultado;

            StartWait();
        }

        Debug.Log("ACABOU - SUCESSO!!!!!!!!!!");
        yield return null;
    }

    private string VerificadorAcao(int inimigoInfos)
    {
        string resultadoTexto = null;
        BBB acaoJogador = Jogador.Singleton.classeJogador.boolADB;
        int valorAcaoJogador = gerenciadorInterface.infoBotaoAtual.valUm;
        int dano = 0;

        if (acaoJogador.atacou && inimigoAtual.boolADB.atacou)
        {
            inimigoAtual.Vida -= valorAcaoJogador;
            Jogador.Singleton.classeJogador.Vida -= inimigoInfos;

            resultadoTexto = "Você e o Monstro se atacam, levando consecutivamente " + valorAcaoJogador + " e " + inimigoInfos + " de dano";
        }
        else if(acaoJogador.atacou && inimigoAtual.boolADB.defendeu)
        {
            dano = inimigoInfos - valorAcaoJogador;

            if(dano >= 0)
            {
                resultadoTexto = "A defesa do Monstro prevaleceu ao seu ataque e ele saiu ileso";
            }
            else if(dano < 0)
            {
                inimigoAtual.Vida += dano;
                resultadoTexto = "O Monstro levou " + dano + " de dano";
            }
        }
        else if(acaoJogador.atacou && inimigoAtual.boolADB.buffou)
        {
            resultadoTexto = "Você ataca o Monstro enquanto ele aumenta seu poder e da " + valorAcaoJogador + " de dano";
        }
        else if(acaoJogador.defendeu && inimigoAtual.boolADB.atacou)
        {
            dano = valorAcaoJogador - inimigoInfos;

            if (dano >= 0)
            {
                resultadoTexto = "A sua defesa prevaleceu ao ataque do Monstro e você saiu ileso";
            }
            else if (dano < 0)
            {
                Jogador.Singleton.classeJogador.Vida += dano;
                resultadoTexto = "Você levou " + dano + " de dano";
            }
        }
        else if (acaoJogador.defendeu && inimigoAtual.boolADB.defendeu)
        {
            resultadoTexto = "Nada aconteceu";
        }
        else if(acaoJogador.defendeu && inimigoAtual.boolADB.buffou)
        {
            resultadoTexto = "Nada Aconteceu";
        }
        else if(acaoJogador.buffou && inimigoAtual.boolADB.atacou)
        {
            resultadoTexto = "O Monstro lhe ataca enquanto voce aumentava suas forças e da " + inimigoInfos + " de dano";
            Jogador.Singleton.classeJogador.Vida -= inimigoInfos;
        }
        
        return resultadoTexto;
    }

    private void StartWait()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        while (gerenciadorInterface.avancar == false)
        {
            yield return null;
        }
        gerenciadorInterface.avancar = false;
    }

    public int Turnos()
    {
        int turnoJogador = Jogador.Singleton.classeJogador.GetTurno;
        int turnoInimigo = inimigoAtual.GetTurno;
        int turnoAtual = 0;

        if (turnoJogador == turnoInimigo)
        {
            turnoAtual = (turnoInimigo + turnoJogador) / 2;
        }

        return turnoAtual;
    }
}
