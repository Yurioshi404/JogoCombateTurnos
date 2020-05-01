using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IS    //Struct feito para retornar um valor 'int' e um valor 'string'
{
    public int valUm;
    public string valDois;

    public IS(int valUm, string valDois) { this.valUm = valUm; this.valDois = valDois; }
}

public struct II    //Struct feito para retornar a vida do jogador e do inimigo depois do calculo de dano
{
    public int vidaJogador;
    public int vidaInimigo;

    public II(int vidaJogador, int vidaInimigo) { this.vidaJogador = vidaJogador; this.vidaInimigo = vidaInimigo; }
}

public struct BBB   //Struct feito para saber se o jogador ou o inimigo atacou, defendeu ou buffou
{
    public bool atacou, defendeu, buffou;

    public BBB(bool atacou, bool defendeu, bool buffou) { this.atacou = atacou; this.defendeu = defendeu; this.buffou = buffou; }
}

public struct III   //Struct feito para saber quantas vezes o jogador atacou, defendeu ou buffou
{
    public int atacou, defendeu, buffou;

    public III(int atacou, int defendeu, int buffou) { this.atacou = atacou; this.defendeu = defendeu; this.buffou = buffou; }
}