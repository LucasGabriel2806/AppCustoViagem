using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCustoViagem
{
    public partial class App : Application
    {
        /**
         * A classe List organiza em algo que é parecido com arrayList do JAVA.
         * Eu tenho varias funcionalidades e, eu tenho um recurso do C# Chamado
         * LINQ. Ele é um recurso que usamos pra facilitar a iteração com listas
         * sem de fato ter que iterar uma lista.
         * Consigo fazer algumas operações como Inserção / Deleção / Soma / Contagem
         * Declarei essa lista aqui porque fica disponivel no app inteiro
         */
        public List<Model.Pedagio> ListaPedagios = new List<Model.Pedagio>();


        public App()
        {
            InitializeComponent();
            /**
             * Usei um artifico pra definir que todo nosso aplicativo vai estar
             * em portugues brasileiro. Ele não traduz, ele faz ajustes pro nosso
             * idioma e cultura. Ex: o dinheiro tem R$, a separação das casas dedcimais
             * não é com ponto e sim com virgula, etc.
             * Um programa é composto por muitas threads.
             * Acesso a classe thread de maneira geral, pego a thread corrente(tela atual), 
             * e defino a cultura daquela tela como a nossa, então sempre que eu 
             * trocar a tela do app, muda isso.
             */
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            /**
             * Usando NavigationPage pra poder fazer a troca de páginas
             * quando eu for ver a lista de pedágios. Sempre que eu chego
             * na lista de pedágios, eu tenho a opção de clicar e ir numa tela
             * ou voltar 
             */
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
