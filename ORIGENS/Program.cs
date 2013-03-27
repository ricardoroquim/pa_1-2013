using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORIGENS
{
    class Program
    {
        const String TITULO_JANELA = "ORIGENS";
        const Int32 ALTERA_JANELA = 40;
        const Int32 LARGURA_JANELA = 100;
        const Int32 LINHA_INICIAL_CORPO = 5;
        const Int32 COLUNA_INICIAL_CORPO = 3;

        static List<Doador> doadores;
        static void EscreverTituloJanela(String texto)
        {
            Int32 tamanhoTexto = texto.Length;
            Int32 larguraMenosTexto = LARGURA_JANELA - tamanhoTexto;
            Int32 metadeDoResto = larguraMenosTexto / 2;
            Console.SetCursorPosition(metadeDoResto, 2);
            Console.WriteLine(texto);

        }
        
        static void EscreverRodapeJanela(String texto)
        {
            Int32 tamanhoTexto = texto.Length;
            Int32 larguraMenosTexto = LARGURA_JANELA - tamanhoTexto;
            Int32 metadeDoResto = larguraMenosTexto / 2;
            Console.SetCursorPosition(metadeDoResto, 38);
            Console.WriteLine(texto);

        }
        private static Int32 linhaCorpo;
        static void EscreverCorpoJanela(String texto, bool linhaInicial=false)
        {
            linhaCorpo = (linhaInicial) ? LINHA_INICIAL_CORPO : linhaCorpo;

            Console.SetCursorPosition(COLUNA_INICIAL_CORPO, linhaCorpo);
            linhaCorpo = linhaCorpo + 1;
            Console.WriteLine(texto);

        }
        static void ConfigurarJanela()
        {

            Console.Title = TITULO_JANELA;
            Console.BufferHeight = ALTERA_JANELA;
            Console.BufferWidth = LARGURA_JANELA;
            Console.SetWindowSize(LARGURA_JANELA, ALTERA_JANELA);

        }
        static void ConfigurarCorDaFonte(ConsoleColor corDaFonte)
        {
            Console.ForegroundColor = corDaFonte;
        }
        static void OpcaoSelecionada()
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            cki = Console.ReadKey(true);

            if (cki.Key == ConsoleKey.V)
            {
                VisualizarClientes();
            }
            else if (cki.Key == ConsoleKey.C)
            {
                CadastrarCliente();
            }
            else if (cki.Key == ConsoleKey.S)
            {
                Console.Beep();
                return;
            }

            Menu();
        }
        static void CadastrarCliente()
        {
            EscreverTituloJanela("VOCE ESTA NA TELA DE CADASTRO DE DOADOR");
            EscreverCorpoJanela("Nome do Doador:");
            string nome = Console.ReadLine();

            Doador doador = new Doador()
            {
                Nome = nome
            };

            try
            {
                doadores.Add(doador);
            }
            catch (Exception ex)
            {
                TextWriter tw = new StreamWriter(@"C:\Temp\erro.log");
                Console.SetError(tw);
                Console.Error.WriteLine(ex);
                Console.Error.Close();
            }

            EscreverRodapeJanela(String.Format("\nO Doador {0} foi cadastrado com sucesso",
                nome));
        }
        static void VisualizarClientes()
        {
            ConfigurarCorDaFonte(ConsoleColor.Yellow);

            if (doadores.Count > 0)
            {
                Console.WriteLine("\nDoador(s) cadastrado(s)\n");

                foreach (Doador doador in doadores)
                {
                    Console.WriteLine(String.Format("{0} ", doador.Nome));
                }
            }
            else
            {
                Console.WriteLine("\nNenhum doador cadastrado.");
            }
        }
        static void Menu()
        {
            ConfigurarCorDaFonte(ConsoleColor.Green);

            EscreverTituloJanela("VOCE ESTA NA TELA PRINCIPAL");

            ConfigurarCorDaFonte(ConsoleColor.Green);

            EscreverCorpoJanela("Para visualizar os doadores cadastrados pressione a tecla V",true);

            EscreverCorpoJanela("Para cadastrar um novo doador pressione a tecla C");

            EscreverCorpoJanela("Para sair pressione a tecla S");

            EscreverRodapeJanela("Aguardando comando...");

            OpcaoSelecionada();
        }
        static void Main(string[] args)
        {
            ConfigurarJanela();

            doadores = new List<Doador>();

            Menu();
        }
    }
}
