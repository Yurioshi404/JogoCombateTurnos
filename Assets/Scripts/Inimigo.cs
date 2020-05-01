using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : Classe
{
    public string nome;
    public string tipo;

    public Inimigo(string nome,string tipo, int statVit, int statInt, int statFor, int statDes, int statVel)
    {
        this.nome = nome;
        this.tipo = tipo;
        this.statVit = statVit;
        this.statInt = statInt;
        this.statFor = statFor;
        this.statDes = statDes;
        this.statVel = statVel;
    }
    
    public override IS Atacar()
    {
        
        int dano = 0;

        if (tipo == "FOR") { dano = statFor * 2; }
        else if(tipo == "INT") { dano = statInt * 2; }

        string text = "O monstro " + nome + " usou Ataque Monstruoso e deu " + dano + " de dano";

        IS retorno = new IS(dano, text);

        boolADB = new BBB(true, false, false);
        turno++;
        return retorno;
    }

    public override IS Defender()
    {
        int defesa = statDes * 2;
        string text = "O monstro " + nome + " usou Defesa Monstruosa e defendeu " + defesa + " de dano";
        IS retorno = new IS(defesa, text);

        boolADB = new BBB(false, true, false);
        turno++;
        return retorno;
    }

    public override IS Habilidade1()
    {
        string text = "O monstro " + nome + " usou Buff Montruoso e ganhou +3 em um status aleatório.";
        int habilidade = UnityEngine.Random.Range(1, 5);
        switch (habilidade)
        {
            case 1:
                statInt += 3;
                break;
            case 2:
                statFor += 3;
                break;
            case 3:
                statDes += 3;
                break;
            case 4:
                statVel += 3;
                break;
        }
        IS retorno = new IS(3, text);

        boolADB = new BBB(false, false, true);
        turno++;
        return retorno;
    }

    public IS InimigoIA()
    {
        int decisao = UnityEngine.Random.Range(1, 101);

        III vezesJogador = Jogador.Singleton.classeJogador.intADB;

        int media = vezesJogador.atacou + vezesJogador.defendeu + vezesJogador.buffou;

        if(media == 0)
        {
            int rand = UnityEngine.Random.Range(1, 4);
            switch (rand)
            {
                case 1:
                    return Atacar();
                case 2:
                    return Defender();
                case 3:
                    return Habilidade1();
            }
        }

        media /= 100;

        III ADB = new III(vezesJogador.atacou * media, vezesJogador.defendeu * media, vezesJogador.buffou * media);

        if (ADB.atacou == ADB.defendeu && ADB.atacou == ADB.buffou)
        {
            int rand = UnityEngine.Random.Range(1, 4);
            switch (rand)
            {
                case 1:
                    return Atacar();
                case 2:
                    return Defender();
                case 3:
                    return Habilidade1();
            }
        }

        ADB = VerificarIgualdade(ADB);

        int maior = VerificarMaior(ADB);
        int menor = VerificarMenor(ADB);
        int medio = VerificarMedio(maior, menor, ADB);

        if (decisao <= menor)
        {
            if(menor == ADB.atacou)
            {
                return Defender();
            }
            else if(menor == ADB.defendeu)
            {
                return Habilidade1();
            }
            else { return Atacar(); }
        }
        else if (decisao > menor && decisao <= medio)
        {
            if (medio == ADB.atacou)
            {
                return Defender();
            }
            else if (medio == ADB.defendeu)
            {
                return Habilidade1();
            }
            else { return Atacar(); }
        }
        else if (decisao >= medio)
        {
            if (maior == ADB.atacou)
            {
                return Defender();
            }
            else if (maior == ADB.defendeu)
            {
                return Habilidade1();
            }
            else { return Atacar(); }
        }

        Debug.LogError("Chegou no final da IA");
        return new IS(0, "0");
    }

    private III VerificarIgualdade(III ADB)
    {
        int rand = UnityEngine.Random.Range(1, 3);

        if (ADB.atacou == ADB.defendeu)
        {
            switch(rand)
            {
                case 1:
                    ADB.atacou *= 2;
                    break;
                case 2:
                    ADB.defendeu *= 2;
                    break;
            }
        }
        else if(ADB.atacou == ADB.buffou)
        {
            switch (rand)
            {
                case 1:
                    ADB.atacou *= 2;
                    break;
                case 2:
                    ADB.buffou *= 2;
                    break;
            }
        }
        else
        {
            switch (rand)
            {
                case 1:
                    ADB.buffou *= 2;
                    break;
                case 2:
                    ADB.defendeu *= 2;
                    break;
            }
        }

        return ADB;
    }
    private int VerificarMaior(III ADB)
    {
        int maior;

        if (ADB.atacou > ADB.defendeu && ADB.atacou > ADB.buffou)
        {
            maior = ADB.atacou;
        }
        else if (ADB.defendeu > ADB.atacou && ADB.defendeu > ADB.buffou)
        {
            maior = ADB.defendeu;
        }
        else
        {
            maior = ADB.buffou;
        }

        return maior;
    }
    private int VerificarMedio(int maior, int menor, III ADB)
    {
        int medio;

        if (ADB.atacou == maior && ADB.defendeu == menor)
        {
            medio = ADB.buffou;
        }
        else if (ADB.defendeu == maior && ADB.buffou == menor)
        {
            medio = ADB.atacou;
        }
        else
        {
            medio = ADB.defendeu;
        }

        return medio;
    }
    private int VerificarMenor(III ADB)
    {
        int menor;

        if (ADB.atacou < ADB.defendeu && ADB.atacou < ADB.buffou)
        {
            menor = ADB.atacou;
        }
        else if (ADB.defendeu < ADB.atacou && ADB.defendeu < ADB.buffou)
        {
            menor = ADB.defendeu;
        }
        else
        {
            menor = ADB.buffou;
        }

        return menor;
    }
}
