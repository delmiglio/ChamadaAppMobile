using ChamadaAppMobile.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class ContentPageBase : ContentPage
    {
        public UsuarioVO usuario;
        
        public ContentPageBase()
        {
            InicializarUsuario();
        }

        /// <summary>
        /// Pega os dados do usuario persistido no telefone
        /// </summary>
        private void InicializarUsuario()
        {
            usuario = App.DataBase.GetUniqueUser();
        }

        private bool _canClose = true;

        /// <summary>
        /// Chama o método ShowExitDialog() e fecha ou não o app com base na resposta.
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            if (_canClose)
                ShowExitDialog();
            else
                DependencyService.Get<IConfigPlatform>().Close_App();

            return _canClose;
        }

        /// <summary>
        /// Exibe dialogo quando o botão de voltar é pressionado.
        /// </summary>
        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Sair", "Deseja fechar o aplicativo?", "Sim", "Não");

            if (answer || !_canClose)
            {
                _canClose = false;

                OnBackButtonPressed();
            }
        }
    }
}
