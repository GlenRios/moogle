namespace MoogleEngine;

public class Vector
{
    public Dictionary<string, double> TFIDF;
    public double Norma;

    public Vector(Documento documento)
    {
        this.TFIDF = new Dictionary<string, double>();
        foreach (var palabra in documento.ContadorDePalabras)
        {
            double tf = 0;
            if (documento.CantidadDePalabras != 0)
            {
                tf = (double)documento.ContadorDePalabras[palabra.Key] / documento.CantidadDePalabras;
            }
            TFIDF.Add(palabra.Key, tf);
        }

        this.Norma = 0;
        foreach (var palabra in TFIDF)
        {
            this.Norma += palabra.Value * palabra.Value;
        }
        this.Norma = Math.Sqrt(this.Norma);
        this.PalabrasNoImportantes = new List<string>();
        this.PalabrasObligatorias = new List<string>();
    }

    public List<string> PalabrasObligatorias;
    public List<string> PalabrasNoImportantes;

    public Vector(string query)
    {
        this.TFIDF = new Dictionary<string, double>();
        string[] palabrasQuery = query.ToLower().Split(Constantes.caracteresNoImportantesII, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
        this.PalabrasObligatorias = new List<string>();
        this.PalabrasNoImportantes = new List<string>();
        for (int i = 0; i < palabrasQuery.Length; i++)
        {
            string palabra = "";
            for (int j = 0; j < palabrasQuery[i].Length; j++)
            {
                if (palabrasQuery[i][j] == '!' || palabrasQuery[i][j] == '^' || palabrasQuery[i][j] == '*') continue;

                else
                {
                    palabra += palabrasQuery[i][j];
                }

            }
            if (palabrasQuery[i][0] == '!')
            {
                PalabrasNoImportantes.Add(palabra);
            }
            int k = 0;
            while (palabrasQuery[i][k] == '*')
            {
                if (this.TFIDF.ContainsKey(palabra))
                {
                    this.TFIDF[palabra] = this.TFIDF[palabra] * 2;
                }
                else
                {
                    this.TFIDF.Add(palabra, 2);
                }
                k++;
            }
            if (palabrasQuery[i][0] == '^')
            {
                PalabrasObligatorias.Add(palabra);
            }
        }
        foreach (string palabra in query.ToLower().Split(Constantes.caracteresNoImportantes, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray())
        {
            if (this.TFIDF.ContainsKey(palabra)) continue;

            this.TFIDF.Add(palabra, 1);
        }
    }

    public static double MultiplicarVectores(Vector vector1, Vector vector2)
    {
        double Producto = 0;
        foreach (var palabra in vector1.TFIDF)
        {
            if (vector2.TFIDF.ContainsKey(palabra.Key))
            {
                Producto += palabra.Value * vector2.TFIDF[palabra.Key];
            }
        }
        return Producto;
    }

    public static double SimilitudDeCoseno(Vector vector1, Vector vector2)
    {
        double score = 0;
        if (vector1.Norma != 0 && vector2.Norma != 0)
        {
            score = MultiplicarVectores(vector1, vector2) / (vector1.Norma * vector2.Norma);
        }
        return score;
    }
}