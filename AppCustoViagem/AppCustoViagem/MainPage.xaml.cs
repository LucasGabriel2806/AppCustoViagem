using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCustoViagem
{
    public partial class MainPage : ContentPage
    {
        /**
         * Declarei uma propriedade do Tipo app, mesmo tipo da classe
         * do app.xaml.cs
         */
        private App PropriedadesApp;

        public MainPage()
        {
            InitializeComponent();
            /**
             * Propriedades App é igual ao application(programa em c#)
             * o programa atual(Current). Faço a conversão do tipo para
             * o tipo App. Agora tudo está disponivel pra essa propredade.
             * Tudo que estiver declarado e acessível lá na app.xaml.cs,
             * eu consigo pegar a partir daqui
             */
            PropriedadesApp = (App)Application.Current;
            //PropriedadesApp.
        }

        private async void btnPedagio_Clicked(object sender, EventArgs e)
        {
            try
            {
                int qnt_pedagios = PropriedadesApp.ListaPedagios.Count;

                PropriedadesApp.ListaPedagios.Add(new Model.Pedagio
                {
                    NroPedagio = qnt_pedagios + 1,
                    Valor = Convert.ToDecimal(etyValorP.Text)
                });

                await DisplayAlert("Adicionado!", "Veja na Lista de Pedágios", "OK");

                etyValorP.Text = "";

            }catch(Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }

        }

        private async void BtnCalcular_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Usando LINQ para somar os pedágios
                decimal valor_total_pedagios =
                    PropriedadesApp.ListaPedagios.Sum(item => item.Valor);

                if (string.IsNullOrEmpty(etyDistancia.Text))
                    throw new Exception("Por favor, preencha a disntância.");

                if (string.IsNullOrEmpty(etyConsumo.Text))
                    throw new Exception("Por favor, preencha o consumo do veículo.");

                if (string.IsNullOrEmpty(etyValorC.Text))
                    throw new Exception("Por favor, preencha o valor do combustível.");

                decimal consumo = Convert.ToDecimal(etyConsumo.Text);
                decimal preco_combustivel = Convert.ToDecimal(etyValorC.Text);
                decimal distancia = Convert.ToDecimal(etyDistancia.Text);

                //Consumo do carro
                decimal consumo_carro = (distancia / consumo) * preco_combustivel;

                //Custo total, com os pedágios
                decimal custo_total = consumo_carro + valor_total_pedagios;

                string origem = etyOrigem.Text;
                string destino = etyDestino.Text;

                /**
                 * O que o string.Format faz? Eu tenho uma string com marcadores, 0 1 e 2, 
                 * substituindo pelas variaveis seguintes na mesma ordem.
                 * ToString("C")Paramentro C de Currency(Dinheiro), transforma em R$.
                 * Só vai conseguir fazer essa conversão R$ poque definimos o cultureInfo
                 * como pt-BR.
                 */
                string mensagem = string.Format("Viagem de {0} a {1} custará {2}",
                    origem, destino, custo_total.ToString("C"));

                await DisplayAlert("Resultado Final", mensagem, "OK");

            }
            catch(Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }



        }

        private async void BtnLimpar_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool confirmar = await DisplayAlert("Tem certeza?", "Limpar todos os dados?", "SIM", "Não");

                if(confirmar)
                {
                    etyConsumo.Text = "";
                    etyDestino.Text = string.Empty;
                    etyDistancia.Text = "";
                    etyOrigem.Text = "";
                    etyValorC.Text = "";
                    etyValorP.Text = "";

                    PropriedadesApp.ListaPedagios.Clear();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void btnListaPedagio_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ListaPedagios());
            }catch(Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro:" + ex.Message, "OK");
            }

        }

        
    }
}
