using PcBolt.Beans.Aruk;


public class Memoria : AruCikk
{
    #region Adattagok deklaralasa
    int tipusId = -1;

    public int TipusId
    {
        get { return tipusId; }
        set { tipusId = value; }
    }


    public string Tipus
    {
        get { return tipus; }
        
    }

    int sebesseg = 0;

    public int Sebesseg
    {
        get { return sebesseg; }
        set { sebesseg = value; }
    }


    #endregion
}