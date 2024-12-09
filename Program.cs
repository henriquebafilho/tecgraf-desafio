using System;
using System.IO;
using System.Text;

namespace tecgraf_desafio
{
    class Program
    {
        static void Main(string[] args)
        {
            Exercicio1();
            Exercicio2();
            Exercicio3();
        }

        static void Exercicio1()
        {
            string lerArquivo = @"./arquivosEntradaSaida/serieSemDV.txt";
            string criarArquivo = @"./arquivosEntradaSaida/serieComDV.txt";
            using StreamReader streamReader = new(lerArquivo);
            File.WriteAllText(criarArquivo, "");

            string serieComDV;
            var serie = string.Empty;
            while ((serie = streamReader.ReadLine()) != null)
            {
                using StreamWriter sWrite = File.AppendText(criarArquivo);
                serieComDV = RetornaHexadecimal(serie);
                sWrite.WriteLine(serieComDV);
            }
        }

        static string RetornaHexadecimal(string serieSemDV)
        {
            string serieComDV, hexadecimal;
            int somaCaracteres = 0;

            foreach (char caractere in serieSemDV)
            {
                int codigoAsc = caractere;
                somaCaracteres += codigoAsc;
            }

            hexadecimal = (somaCaracteres % 16).ToString("X");
            serieComDV = serieSemDV + "-" + hexadecimal;
            return serieComDV;
        }

        static void Exercicio2()
        {
            string lerArquivo = @"./arquivosEntradaSaida/serieParaVerificar.txt";
            string criarArquivo = @"./arquivosEntradaSaida/serieVerificada.txt";
            using StreamReader streamReader = new(lerArquivo);
            File.WriteAllText(criarArquivo, "");

            string serieVerificada;
            var serie = string.Empty;
            while ((serie = streamReader.ReadLine()) != null)
            {
                using StreamWriter sWrite = File.AppendText(criarArquivo);
                serieVerificada = VerificaSerie(serie);
                sWrite.WriteLine(serieVerificada);
            }
        }

        static string VerificaSerie(string serie)
        {
            string serieSemDV, hexadecimalIdeal, hexadecimalOriginal, serieVerificada;
            serieSemDV = serie.Remove(serie.Length - 2);
            hexadecimalOriginal = serie[^1].ToString();
            int somaCaracteres = 0;

            foreach (char caractere in serieSemDV)
            {
                int codigoAsc = caractere;
                somaCaracteres += codigoAsc;
            }

            hexadecimalIdeal = (somaCaracteres % 16).ToString("X");
            serieVerificada = serie + " " + hexadecimalIdeal.Equals(hexadecimalOriginal);
            return serieVerificada;
        }

        static void Exercicio3()
        {
            string lerSeries = @"./arquivosEntradaSaida/serieParaRelatorio.txt";
            string criarArquivo = @"./arquivosEntradaSaida/listaTotais.txt";
            using StreamReader streamReader = new(lerSeries);
            File.WriteAllText(criarArquivo, "");

            string paisAtual;
            List<string> paises = [];
            List<string> paisEQuantidade = [];
            var serie = string.Empty;
            while ((serie = streamReader.ReadLine()) != null)
            {
                paisAtual = serie.Substring(4, 3);
                paises.Add(paisAtual);
            }

            paisEQuantidade = RetornaPaisEQuantidade(paises);
            paisEQuantidade.Sort();

            foreach (string pais in paisEQuantidade)
            {
                using StreamWriter sWrite = File.AppendText(criarArquivo);
                sWrite.WriteLine(pais);
            }
        }

        static List<string> RetornaPaisEQuantidade(List<string> paises)
        {
            string lerPaises = @"./arquivosEntradaSaida/paises.txt";

            HashSet<string> paisesSemRepeticao = [.. paises];
            List<string> paisesEQuantidade = [];
            foreach (string pais in paisesSemRepeticao)
            {
                using StreamReader streamReader = new(lerPaises);
                var paisProcurado = string.Empty;
                while ((paisProcurado = streamReader.ReadLine()) != null)
                {
                    if (pais.Equals(paisProcurado[..3]))
                    {
                        string nomePais = paisProcurado.Split(";")[1];
                        int qtdRepeticoes = paises.Count(i => i.Equals(pais));
                        paisesEQuantidade.Add(nomePais + "-" + qtdRepeticoes);
                        break;
                    }
                }
            }
            return paisesEQuantidade;
        }
    }
}
