using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroCompleto.Models.Responses
{
    public class ServiceResponse<T>
    {
        public T Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Sucesso { get; set; } = true;

        public static ServiceResponse<T> ComSucesso(T dados)
        {
            var response = new ServiceResponse<T>()
            {
                Dados = dados
            };

            return response;
        }
        public static ServiceResponse<T> ComSucesso(T dados, string mensagem)
        {
            var response = new ServiceResponse<T>()
            {
                Dados = dados,
                Mensagem = mensagem
            };

            return response;
        }

        public static ServiceResponse<T> ComFalha(string mensagem)
        {
            var response = new ServiceResponse<T>()
            {
                Sucesso = false,
                Mensagem = mensagem
            };

            return response;
        }
    }
}
