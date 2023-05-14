using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCustoViagem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPedagios : ContentPage
    {
        private App PropriedadesApp;
        public ListaPedagios()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;

            /**
             * Todos os pedagios que estão na lista das PropriedadesApp
             * vão aparecer na ListView
             */
            lst_lista_pedagios.ItemsSource = PropriedadesApp.ListaPedagios;

            if(PropriedadesApp.ListaPedagios.Count == 0)
            {
                lst_lista_pedagios.IsVisible = false;
                lbl_msg_lista_vazia.IsVisible = true;
            }
            /**Observações
             * Se tiver pedagios na pagina de lista dos pedagios e eu apagar,
             * ele não vai mostrar a msg_lista_vazia, já que estou fazendo 
             * isso no contrutor. Eu preciso sair da tela e entrar de novo,
             * Pra atualizar o construtor.
             */

        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            /**
             * Quando eu clico em remover, ele chama esse metodo MenuItem_Clicked.
             * Pra eu saber qual que foi o dos vários menus items que podem estar
             * escondidos aqui, eu tenho que identificar ele com o atributo sender.
             * Aqui o sender vai armazenar qual foi o MenuItem que disparou o 
             * Evento MenuItem_Clicked.
             * 
             */
            try
            {
                /**
                 * Variavel menuItem, do tipo MenuItem, que é igual ao sender 
                 * sendo um MenuItem. O C#, como ele está separado a lógica
                 * do tratamento do evento da interface do xaml, ele não 
                 * consegue enxergar muito bem o que que ta acontecendo lá.
                 * Chegou um sender, que é do tipo object, eu preciso falar pro c#,
                 * que esse sender que chegou, é um MenuItem.
                 * Ai ele entendeu que a var menuItem chegou como um elemento da
                 * Interface MenuItem.
                 */
                MenuItem menuItem = sender as MenuItem;

                var pedagio_selecionado = (Model.Pedagio)menuItem.BindingContext;
                /**
                 * o menuItem contém algumas informações, como um BindingContext.
                 * Quando eu clico em cima de um menuItem, eu to mandando todas as
                 * informações que tem dentro da minha celula de visualização.
                 * Quando eu to clicando em remover, eu não to mandando só a mensagem 
                 * que eu quero remover, eu to mandando todos os dados desse pedagio,
                 * que são o numero do pedágio e o valor, Ta tudo isso dentro do BindingContext.
                 */

                if (await DisplayAlert("Tem Certeza?", "Deseja remover este pedágio?", "Sim", "Não"))
                {
                    PropriedadesApp.ListaPedagios.RemoveAll(i => i.NroPedagio ==
                    pedagio_selecionado.NroPedagio);

                    /**hack para resolver problema de atualização dos itens da lista.
                     * Meu listView foi abastecido no construtor da classe, essa ação de
                     * excluir, está sendo executada depois que os elementos visuais foram
                     * construidos. Então se eu remover esse item e não usar esse hack, 
                     * ele não vai sair daqui
                     */
                    lst_lista_pedagios.ItemsSource = new List<Model.Pedagio>();

                    lst_lista_pedagios.ItemsSource = PropriedadesApp.ListaPedagios;
                }

            }catch(Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var total = PropriedadesApp.ListaPedagios.Sum(i => i.Valor).ToString("C");

                await DisplayAlert("Resultado Final", String.Format("O total dos pedágios é {0}",
                    total), "OK");
            }catch(Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro: " + ex.Message, "OK");
                /**
                 * "Programação é uma atividade intelectual, programar não é datilografar
                 * Você tem que pensar, utilizar os conceitos certos, raciocinar em cima
                 * do código e sempre entender o que o códgio ta fazendo" - Tiago A. Silva
                 */
            }
        }
    }
}