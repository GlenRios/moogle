namespace MoogleEngine;

public class Documento
{
    public string Titulo;
    public string[] Palabras;
    public Dictionary<string, int> ContadorDePalabras;
    public int CantidadDePalabras;

    public Documento(string titulo, string texto)
    {
        this.Titulo = titulo;
        this.Palabras = texto.ToLower().Split(Constantes.caracteresNoImportantes, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
        this.CantidadDePalabras = Palabras.Length;
        this.ContadorDePalabras = new Dictionary<string, int>();

        foreach (string palabra in Palabras)
        {
            if (this.ContadorDePalabras.ContainsKey(palabra))
            {
                this.ContadorDePalabras[palabra]++;
            }
            else
            {
                this.ContadorDePalabras.Add(palabra, 1);
            }
        }
    }
}
