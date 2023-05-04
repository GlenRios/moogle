namespace MoogleEngine;

public class Cuerpo
{
    public Dictionary<string, double> IDF;
    public Vector[] Vectores;

    public Cuerpo(Documento[] documentos)
    {
        Dictionary<string, int> DocumentosPorPalabra = new Dictionary<string, int>();
        for (int i = 0; i < documentos.Length; i++)
        {
            foreach (var palabra in documentos[i].ContadorDePalabras)
            {
                if (DocumentosPorPalabra.ContainsKey(palabra.Key))
                {
                    DocumentosPorPalabra[palabra.Key]++;
                }
                else
                {
                    DocumentosPorPalabra.Add(palabra.Key, 1);
                }
            }
        }

        this.IDF = new Dictionary<string, double>();

        foreach (var palabra in DocumentosPorPalabra)
        {
            this.IDF.Add(palabra.Key, Math.Log((double)documentos.Length / palabra.Value));
        }

        this.Vectores = new Vector[documentos.Length];

        for (int i = 0; i < documentos.Length; i++)
        {
            this.Vectores[i] = new Vector(documentos[i]);

            foreach (var palabra in documentos[i].ContadorDePalabras)
            {
                double tf = this.Vectores[i].TFIDF[palabra.Key];
                double idf = IDF[palabra.Key];

                this.Vectores[i].TFIDF[palabra.Key] = tf * idf;
            }
        }
    }
}