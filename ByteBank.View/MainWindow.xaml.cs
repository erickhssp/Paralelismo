using ByteBank.Core.Model;
using ByteBank.Core.Repository;
using ByteBank.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ByteBank.View
{
    public partial class MainWindow : Window
    {
        private readonly ContaClienteRepository r_Repositorio;
        private readonly ContaClienteService r_Servico;

        public MainWindow()
        {
            InitializeComponent();

            r_Repositorio = new ContaClienteRepository();
            r_Servico = new ContaClienteService();
        }

        private void BtnProcessar_Click(object sender, RoutedEventArgs e)
        {
            var contas = r_Repositorio.GetContaClientes();

            var quantidadeDeContasPorThread = contas.Count() / 8;

            var contas_parte1 = contas.Take(quantidadeDeContasPorThread);
            var contas_parte2 = contas.Take(quantidadeDeContasPorThread).Take(quantidadeDeContasPorThread);
            var contas_parte3 = contas.Take(quantidadeDeContasPorThread * 2).Take(quantidadeDeContasPorThread);
            var contas_parte4 = contas.Take(quantidadeDeContasPorThread * 3).Take(quantidadeDeContasPorThread);
            var contas_parte5 = contas.Take(quantidadeDeContasPorThread * 4).Take(quantidadeDeContasPorThread);
            var contas_parte6 = contas.Take(quantidadeDeContasPorThread * 5).Take(quantidadeDeContasPorThread);
            var contas_parte7 = contas.Take(quantidadeDeContasPorThread * 6).Take(quantidadeDeContasPorThread);
            var contas_parte8 = contas.Take(quantidadeDeContasPorThread * 7);



            var resultado = new List<string>();

            AtualizarView(new List<string>(), TimeSpan.Zero);

            var inicio = DateTime.Now;

            Thread thread_parte1 = new Thread(() =>
            {
                foreach (var conta in contas_parte1)
                {
                    var resultadoProcessamento = r_Servico.ConsolidarMovimentacao(conta);
                    resultado.Add(resultadoProcessamento);
                }
            });
            Thread thread_parte2 = new Thread(() =>
            {
                foreach (var conta in contas_parte2)
                {
                    var resultadoProcessamento = r_Servico.ConsolidarMovimentacao(conta);
                    resultado.Add(resultadoProcessamento);
                }
            });

            Thread thread_parte3 = new Thread(() =>
            {
                foreach (var conta in contas_parte3)
                {
                    var resultadoProcessamento = r_Servico.ConsolidarMovimentacao(conta);
                    resultado.Add(resultadoProcessamento);
                }
            });
            Thread thread_parte4 = new Thread(() =>
            {
                foreach (var conta in contas_parte4)
                {
                    var resultadoProcessamento = r_Servico.ConsolidarMovimentacao(conta);
                    resultado.Add(resultadoProcessamento);
                }
            });
            Thread thread_parte5 = new Thread(() =>
            {
                foreach (var conta in contas_parte5)
                {
                    var resultadoProcessamento = r_Servico.ConsolidarMovimentacao(conta);
                    resultado.Add(resultadoProcessamento);
                }
            });
            Thread thread_parte6 = new Thread(() =>
            {
                foreach (var conta in contas_parte6)
                {
                    var resultadoProcessamento = r_Servico.ConsolidarMovimentacao(conta);
                    resultado.Add(resultadoProcessamento);
                }
            });
            Thread thread_parte7 = new Thread(() =>
            {
                foreach (var conta in contas_parte7)
                {
                    var resultadoProcessamento = r_Servico.ConsolidarMovimentacao(conta);
                    resultado.Add(resultadoProcessamento);
                }
            });
            Thread thread_parte8 = new Thread(() =>
            {
                foreach (var conta in contas_parte8)
                {
                    var resultadoProcessamento = r_Servico.ConsolidarMovimentacao(conta);
                    resultado.Add(resultadoProcessamento);
                }
            });


            thread_parte1.Start();
            thread_parte2.Start();
            thread_parte3.Start();
            thread_parte4.Start();
            thread_parte5.Start();
            thread_parte6.Start();
            thread_parte7.Start();
            thread_parte8.Start();

            while (thread_parte1.IsAlive || thread_parte2.IsAlive || thread_parte3.IsAlive || thread_parte4.IsAlive
                || thread_parte5.IsAlive || thread_parte6.IsAlive || thread_parte7.IsAlive || thread_parte8.IsAlive)
            {
                Thread.Sleep(250);
                // Não vou fazer nada.
            }

            var fim = DateTime.Now;

            AtualizarView(resultado, fim - inicio);
        }

        private void AtualizarView(List<String> result, TimeSpan elapsedTime)
        {
            var tempoDecorrido = $"{ elapsedTime.Seconds }.{ elapsedTime.Milliseconds} segundos!";
            var mensagem = $"Processamento de {result.Count} clientes em {tempoDecorrido}";

            LstResultados.ItemsSource = result;
            TxtTempo.Text = mensagem;
        }
    }
}
