using RegistroNF.Core.Entities;

namespace RegistroNF.Core.Contracts.Service
{
    public interface IEmpresaService
    {
        void CadastroEmpresa(Empresa empresa);

        void GerenciaEmpresasCadastroParcial();

        bool EhExistente(int cnpj);

        Empresa BuscarPorCNPJ(int cnpj);
    }
}
