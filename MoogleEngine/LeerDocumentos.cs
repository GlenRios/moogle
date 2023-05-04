namespace MoogleEngine;
public static class LeerDocumentos
{
    public static Documento[] Leer()
    {
        string[] archivos = Directory.GetFiles(Constantes.Direccion, "*.txt");
        Documento[] documentos = new Documento[archivos.Length];
        for (int i = 0; i < archivos.Length; i++)
        {
            documentos[i] = new Documento(Path.GetFileName(archivos[i]), File.ReadAllText(archivos[i]));
        }

        return documentos;
    }
}