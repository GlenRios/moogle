﻿namespace MoogleEngine;

public static class Moogle
{
    static Documento[] Documentos;
    static Cuerpo CuerpoDeVectores;

    static Moogle()
    {
        Documentos = LeerDocumentos.Leer();
        CuerpoDeVectores = new Cuerpo(Documentos);
    }

    static void ProcesarConsulta(Vector consulta)
    {
        foreach (var palabra in consulta.TFIDF)
        {
            if (CuerpoDeVectores.IDF.ContainsKey(palabra.Key) && CuerpoDeVectores.IDF[palabra.Key] == 0)
            {
                consulta.TFIDF[palabra.Key] = 0;
            }
        }
        consulta.Norma = 0;
        foreach (var palabra in consulta.TFIDF)
        {
            consulta.Norma += palabra.Value * palabra.Value;
        }
        consulta.Norma = Math.Sqrt(consulta.Norma);
    }

    static List<SearchItem> EncontrarResultados(Vector consulta)
    {
        List<SearchItem> resultados = new List<SearchItem>();

        for (int i = 0; i < Documentos.Length; i++)
        {
            double relevancia = Vector.SimilitudDeCoseno(consulta, CuerpoDeVectores.Vectores[i]);
            if (relevancia != 0)
            {
                resultados.Add(new SearchItem(Documentos[i].Titulo, "no snippet", relevancia));
            }
        }

        return resultados;
    }

    public static SearchResult Query(string query)
    {
        Vector consulta = new Vector(query);
        ProcesarConsulta(consulta);
        List<SearchItem> resultados = EncontrarResultados(consulta);

        return new SearchResult(resultados.ToArray(), query);
    }
}
