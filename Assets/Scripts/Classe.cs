using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Classe
{
    protected int vida;
    public int Vida { get { return vida; } set { vida = value; } }

    protected int manaMax;
    public int GetManaMax { get { return manaMax; } }

    protected int mana;
    public int Mana { get { return mana; } set { mana = value; } }

    public BBB boolADB;
    public III intADB = new III(0,0,0);

    protected int turno;
    public int GetTurno { get { return turno; } }

    protected int statVit;
    public int GetVit { get { return statVit; } }

    protected int statInt;
    public int GetInt { get { return statInt; } }

    protected int statFor;
    public int GetFor { get { return statFor; } }

    protected int statDes;
    public int GetDes { get { return statDes; } }

    protected int statVel;
    public int GetVel { get { return statVel; } }

    public virtual void InitialStats()
    {
        vida = statVit * 25;
        mana = statInt * 10;
        manaMax = mana;
    }

    public abstract IS Atacar();
    public abstract IS Defender();
    public abstract IS Habilidade1();
    public virtual IS Habilidade2() { return new IS(0, "0"); }
    public virtual void EndBuff() { return; }
}

public class Guerreiro : Classe
{
    public int forTemporaria = 0;

    public override void EndBuff()
    {
        forTemporaria -= 3;
        statFor -= forTemporaria;
    }

    public override void InitialStats()
    {
        statVit = 3;
        statInt = 1;
        statFor = 4;
        statDes = 5;
        statVel = 2;

        base.InitialStats();
    }

    public override IS Atacar()
    {
        intADB.atacou++;

        int _forTemporaria = forTemporaria;
        string text = null;
        int vidaPorcentagem = vida / 25;
        forTemporaria += statFor - vidaPorcentagem;
        IS infos = new IS(statFor + forTemporaria, text);

        text = "O jogador usou o Estilo - Espadada Sangrenta e deu " + infos.valUm + " de dano";
        infos.valDois = text;

        forTemporaria = _forTemporaria;
        turno++;

        return infos;
    }

    public override IS Defender()
    {
        intADB.defendeu++;

        string text = null;
        int desTemporaria = vida / 25;
        int defesaReal = statDes - 3 + desTemporaria;
        IS infos = new IS(defesaReal, text);

        text = "O jogador usou o Estilo - Defesa Limpa e defendeu " + infos.valUm + " de dano";
        infos.valDois = text;

        turno++;

        return infos;
    }

    public override IS Habilidade1()
    {
        if(mana > 2)
        {
            intADB.buffou++;

            string text = "O jogador usou o Estilo - Aura Guerreira e ganhou +3 de força por 2 turnos";
            forTemporaria += 3;
            statFor += forTemporaria;
            IS infos = new IS(forTemporaria, text);
            mana -= 3;

            turno++;

            return infos;
        }
        else
        {
            return new IS(0, null);
        }
    }

    public override IS Habilidade2()
    {
        intADB.atacou++;

        string text = null;
        vida -= 25;
        int dano = 25 + forTemporaria;
        IS infos = new IS(dano, text);

        text = "O jogador usou o Estilo - Espada do Sacrifício e deu " + infos.valUm + " de dano";
        infos.valDois = text;

        turno++;

        return infos;
    }
}

/*
public class Arqueiro : Base
{
    public override void InitialStats()
    {
        statVit = 2;
        statInt = 4;
        statFor = 2;
        statDes = 2;
        statVel = 5;

        base.InitialStats();
    }

    public override FFS Atacar()
    {
        string attackName = "Flechas Multiplas";
        int numeroDeFlechas = Random.Range(1, statVel);
        FFS infos = new FFS(numeroDeFlechas * statFor, numeroDeFlechas, attackName);

        return infos;
    }

    public override int Defender()
    {

    }

    public int Habilidade1()
    {

    }

    public int Habilidade2()
    {

    }
}

public class Mago : Base
{

    public override void InitialStats()
    {
        statVit = 2;
        statInt = 8;
        statFor = 1;
        statDes = 1;
        statVel = 3;

        base.InitialStats();
    }

    public override FFS Atacar() { return new FFS(0,0,"0"); }

    public FS Atacar(int charges)
    {
        string attackName = "Tiro de Mana";
        FS infos = new FS(statInt * (charges + 1), attackName);

        return infos;
    }

    public override int Defender()
    {

    }

    public int Habilidade1()
    {
        
    }

    public FS Habilidade2(int charges)
    {
        string skillName = "Concentrar Mana";

        FS infos = new FS(charges + 1, skillName);

        return infos;
    }
    
}
*/