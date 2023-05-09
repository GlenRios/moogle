namespace MoogleEngine;
public class Matrix
{
    public Matrix()
    {
    }
    public static double[][] MatrixSuma(double[][] A, double[][] B)
    {
        int CantidadDeFilasDeA = A.GetLength(0);
        int CantidadDeFilasDeB = B.GetLength(0);
        int CantidadDeColumnasDeA = A.GetLength(1);
        int CantidadDeColumnasDeB = B.GetLength(1);
        double[][] MatrixSuma = new double[CantidadDeFilasDeA][];
        if (CantidadDeFilasDeA == CantidadDeFilasDeB && CantidadDeColumnasDeA == CantidadDeColumnasDeB)
        {
            for (int i = 0; i < CantidadDeFilasDeA; i++)
            {
                for (int j = 0; j < CantidadDeColumnasDeA; j++)
                {
                    MatrixSuma[i][j] = A[i][j] + B[i][j];
                }
            }
        }
        else
        {
            System.Console.WriteLine("No es posible sumarlas pues no tienen la misma longitud");
        }
        return MatrixSuma;
    }

    public static double[][] MatrixPorUnEscalar(double[][] A, double a)
    {
        double[][] MatrixProducto = new double[A.GetLength(0)][];
        for (int i = 0; i < A.GetLength(0); i++)
        {
            for (int j = 0; j < A.GetLength(1); j++)
            {
                MatrixProducto[i][j] = a * A[i][j];
            }
        }
        return MatrixProducto;
    }

    public static double[][] MatrixPorMatrix(double[][] A, double[][] B)
    {
        int CantidadDeColumnasDeA = A.GetLength(1);
        int CantidadDeFilasDeB = B.GetLength(0);
        double[][] MatrixProducto = new double[A.GetLength(0)][];
        if (CantidadDeColumnasDeA == CantidadDeFilasDeB)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    for (int k = 0; k < CantidadDeColumnasDeA; k++)
                    {
                        MatrixProducto[i][j] += A[i][k] * B[k][j];
                    }
                }
            }
        }
        else
        {
            System.Console.WriteLine("No se pueden multiplicar dichas matrices");
        }
        return MatrixProducto;
    }

}