﻿using System;

namespace Serpinski
{
    class Program
    {

        static void Main(string[] args)
        {

            double[][] pozition = new double[5][]; // Создаем массив массивов

            pozition[0] = new double[] { -1.0, -1.0, 1.0 }; // A
            pozition[1] = new double[] { 1.0, -1.0, 1.0 }; // B
            pozition[2] = new double[] { 1.0, -1.0, -1.0 }; // C
            pozition[3] = new double[] { -1.0, -1.0, -1.0 }; // D
            pozition[4] = new double[] { 0.0, 1.0, 0.0 };  // S
            // Координаты вершин

            int size = 6; // количество итераций 
            int vertice = 0; // Количество вершин 
            // Количество вершин в этой программе
            vertice = (int)Math.Pow(5, size + 1);

            //Фактическое количество вершин 
            //if (size != 0)
            //    vertice = 5 + 9 + 9 * (int)Math.Pow(5, size - 1);
            //else vertice = 5;

            int faces = (int)Math.Pow(5, size + 1); // Количество закрашенных полей
            int edges = 8 * (int)Math.Pow(5, size + 1);


            Faces face = new();

            string path = @"F:\vano\Учеба\Универ\Prog\Serpinski\Serpinski\Serp" + size + ".off";
            //string path = @"C:\Users\RoGad\OneDrive\Рабочий стол\pyramid_of_Serpinsky-main\Serpinski\Serp" + size + ".off";

            if (!File.Exists(path))
            {
                File.AppendAllText(path, "OFF");
                File.AppendAllText(path, "\n");
                // Записываем в файл все параметры по порядку
                // кол-во вершин, кол-во граней, кол-во ребер (можно выставить 0)
                File.AppendAllText(path, vertice.ToString() + " ");
                File.AppendAllText(path, faces.ToString() + " ");
                File.AppendAllText(path, edges.ToString() + "\n");

                // координаты всех точек
                pirPoz(pozition[0], pozition[1], pozition[2], pozition[3], pozition[4], size, path, face);
                // грани
                for (int i = 0; i < faces; i++)
                {
                    File.AppendAllText(path, face.faces[i]);
                    Console.WriteLine(face.faces[i]);
                }
                Console.WriteLine(face.verticeCol);
                Console.WriteLine(face.facesCol);

            }

        }

        class Faces
        {
            public int facesCol = 0; // Счетчик для полей
            public int verticeCol = 0; // Счетчик для вершин
            public string[] faces = new string[1000000];
            public Faces()
            {

            }
            public int upFacesCol(int n)
            {
                this.facesCol += n;
                return this.facesCol - n;
            }
            public void upVerticeCol(int n)
            {
                this.verticeCol += n;
            }


        }

        static void pirPoz(double[] A, double[] B, double[] C, double[] D, double[] S, int size, string path, Faces face)
        {
            double[][] middles = new double[9][]; // Вершины вложенных пирамид
            middles[0] = new double[3]; // ab
            middles[1] = new double[3]; // bc
            middles[2] = new double[3]; // ac
            middles[3] = new double[3]; // ad
            middles[4] = new double[3]; // cd
            middles[5] = new double[3]; // bs
            middles[6] = new double[3]; // as
            middles[7] = new double[3]; // cs
            middles[8] = new double[3]; // ds

            //string[][] middlesForFaces = new string[5][]; // Вершины для полей


            if (size != 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    middles[0][i] = (A[i] + B[i]) / 2.0; // середина A и B
                    middles[1][i] = (B[i] + C[i]) / 2.0; // середина B и C
                    middles[2][i] = (A[i] + C[i]) / 2.0; // середина A и C 
                    middles[3][i] = (A[i] + D[i]) / 2.0; // середина A и D
                    middles[4][i] = (C[i] + D[i]) / 2.0; // середина C и D
                    middles[5][i] = (B[i] + S[i]) / 2.0; // середина B и S
                    middles[6][i] = (A[i] + S[i]) / 2.0; // середина A и S
                    middles[7][i] = (C[i] + S[i]) / 2.0; // середина C и S
                    middles[8][i] = (D[i] + S[i]) / 2.0; // середина D и S
                }

                //for (int i = 0; i < 9; i++) {
                //    string str = middles[i][0].ToString() + " " + middles[i][1].ToString() + " " + middles[i][2].ToString();
                //    File.AppendAllText(path, str);
                //}

                //face.faces[face.facesCol++] = "4 " + 


                // Ищем координаты вершин таких же 5 пирамидок внутри одной
                pirPoz(A, middles[0], middles[2], middles[3], middles[6], size - 1, path, face);
                pirPoz(middles[0], B, middles[1], middles[2], middles[5], size - 1, path, face);
                pirPoz(middles[2], middles[1], C, middles[4], middles[7], size - 1, path, face);
                pirPoz(middles[3], middles[2], middles[4], D, middles[8], size - 1, path, face);
                pirPoz(middles[6], middles[5], middles[7], middles[8], S, size - 1, path, face);

            }
            else
            {
                File.AppendAllText(path, A[0].ToString() + " " + A[1].ToString() + " " + A[2].ToString() + "\n");
                File.AppendAllText(path, B[0].ToString() + " " + B[1].ToString() + " " + B[2].ToString() + "\n");
                File.AppendAllText(path, C[0].ToString() + " " + C[1].ToString() + " " + C[2].ToString() + "\n");
                File.AppendAllText(path, D[0].ToString() + " " + D[1].ToString() + " " + D[2].ToString() + "\n");
                File.AppendAllText(path, S[0].ToString() + " " + S[1].ToString() + " " + S[2].ToString() + "\n");
                // при одной итерации получается 25, но больше чем 14, тк точки повторяются. исправить

                // записываем грани и индексы вершин в массив
                int a = 0 + face.verticeCol, b = 1 + face.verticeCol, c = 2 + face.verticeCol, d = 3 + face.verticeCol, s = 4 + face.verticeCol;
                face.faces[face.upFacesCol(1)] = "4 " + a.ToString() + " " + b.ToString() + " " + c.ToString() + " " + d.ToString() + "\n";
                face.faces[face.upFacesCol(1)] = "3 " + a.ToString() + " " + s.ToString() + " " + b.ToString() + "\n";
                face.faces[face.upFacesCol(1)] = "3 " + a.ToString() + " " + s.ToString() + " " + d.ToString() + "\n";
                face.faces[face.upFacesCol(1)] = "3 " + b.ToString() + " " + s.ToString() + " " + c.ToString() + "\n";
                face.faces[face.upFacesCol(1)] = "3 " + c.ToString() + " " + s.ToString() + " " + d.ToString() + "\n";

                face.upVerticeCol(5); // Увеличиваем счетчик для последующих записей
            }


        }
    }


}