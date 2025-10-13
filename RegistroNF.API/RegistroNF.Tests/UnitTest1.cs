using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RegistroNF.Core.Contracts.Repository;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.Entities;
using TaxAPI.Core.Entities;
using TaxAPI.Services;

namespace RegistroNF.Tests
{
    public class UnitTest1
    {
        private readonly INotaFiscalService _sut;
        private readonly Mock<IValidator<NotaFiscal>> _nfValidatorMock;
        private readonly Mock<IEmpresaService> _empresaServiceMock;
        private readonly Mock<INotaFiscalRepository> _nfRepositoryMock;
        private readonly Mock<IValidator<Empresa>> _empresaValidatorMock;

        public UnitTest1()
        {
            _empresaServiceMock = new Mock<IEmpresaService>();
            _nfRepositoryMock = new Mock<INotaFiscalRepository>();
            _empresaValidatorMock = new Mock<IValidator<Empresa>>();
            _nfValidatorMock = new Mock<IValidator<NotaFiscal>>();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(_empresaServiceMock.Object);
            serviceCollection.AddSingleton(_nfRepositoryMock.Object);
            serviceCollection.AddSingleton(_empresaValidatorMock.Object);
            serviceCollection.AddSingleton(_nfValidatorMock.Object);

            _sut = new NotaFiscalService
            (
                _nfValidatorMock.Object,
                _empresaValidatorMock.Object,
                _empresaServiceMock.Object,
                _nfRepositoryMock.Object
            );
        }

        [Fact]
        public void Test1()
        {
            var nfs = new List<NotaFiscal>
            {
                new NotaFiscal()
                {
                    Serie = 1,
                    Numero = 1,
                    DataEmissao = new DateTime(2025, 09, 24),
                    Empresa = new Empresa()
                    {
                        CNPJ = "12345678000195",
                        NomeResponsavel = "aaa",
                        EmailResponsavel = "aaa"
                    }
                },

                new NotaFiscal()
                {
                    Serie = 1,
                    Numero = 2,
                    DataEmissao = new DateTime(2025, 09, 25),
                    Empresa = new Empresa()
                    {
                        CNPJ = "12345678000195",
                        NomeResponsavel = "aaa",
                        EmailResponsavel = "aaa"
                    }
                },
            };

            var nf = new NotaFiscal()
            {
                Serie = 1,
                Numero = 3,
                DataEmissao = new DateTime(2025, 09, 23),
                Empresa = new Empresa()
                {
                    CNPJ = "12345678000195",
                    NomeResponsavel = "aaa",
                    EmailResponsavel = "aaa"
                }
            };

            _nfValidatorMock.Setup(x => x.Validate(It.IsAny<NotaFiscal>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            _nfRepositoryMock.Setup(x => x.GetSerieNF(It.IsAny<int>()))
                .Returns(nfs);

            _sut.EmitirNota(nf);

            Assert.Throws<ArgumentException>(() => _sut.EmitirNota(nf));
        }
    }
}