using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;

namespace Dataplace.Imersao.Core.Domain.Orcamentos
{
    public class Orcamento
    {
        private Orcamento(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoCliente cliente, 
            string usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
        {

            CdEmpresa = cdEmpresa;
            CdFilial = cdFilial;
            Cliente = cliente;
            NumOrcamento = numOrcamento;
            Usuario = usuario;
            Vendedor = vendedor;
            TabelaPreco = tabelaPreco;

            // default
            Situacao = OrcamentoStatusEnum.Aberto;
            DtOrcamento = DateTime.Now;
            ValotTotal = 0;
            Itens = new List<OrcamentoItem>();

        }

        public string CdEmpresa { get; private set; }
        public string CdFilial { get; private set; }
        public int NumOrcamento { get; private set; }
        public OrcamentoCliente Cliente { get; private set; }
        public DateTime DtOrcamento { get; private set; }
        public decimal ValotTotal { get; private set; }
        public OrcamentoValidade Validade { get; private set; }
        public OrcamentoTabelaPreco TabelaPreco { get; private set; }
        public DateTime? DtFechamento { get; private set; }
        public OrcamentoVendedor Vendedor { get; private set; }
        public usuárodoOrçamento Usuario { get; private set; }
        public OrcamentoStatusEnum Situacao { get; private set; }
        public ICollection<OrcamentoItem> Itens { get; private set; }

        public void InsereItens(OrcamentoItem item)
        {
            Itens.Add(item);
        }

        public void FecharOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Fechado)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Fechado;
            DtFechamento = DateTime.Now.Date;
        }

        public void ReabrirOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Aberto)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Aberto;
            DtFechamento = null;
        }

        public void DefinirValidade(int diasValidade)
        {
            this.Validade = new OrcamentoValidade(this, diasValidade);
        }

        public void CancelarOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Fechado)
                throw new DomainException("Orçamento já está fechado não cancelado!");

            Situacao = OrcamentoStatusEnum.Cancelado;
           
        }


        #region validations

        public List<string> Validations;
        public bool IsValid()
        {
            Validations = new List<string>();

            if (string.IsNullOrEmpty(CdEmpresa))
                Validations.Add("Código da empresa é requirido!");

            if (string.IsNullOrEmpty(CdFilial))
                Validations.Add("Código da filial é requirido!");

            if (string.IsNullOrEmpty(Usuario))
                Validations.Add("Código codigo do usuario é requirido!");

            if (string.IsNullOrEmpty(TabelaPreco.CdTabela))
                Validations.Add("Tabela de Preço requirido!");

            if (string.IsNullOrEmpty(Vendedor.Codigo))
                Validations.Add("Código do Vendedor é requirido!");

            if (Validations.Count > 0)
                return false;
            else
                return true;
        }

        #endregion

        #region factory methods
        public static class Factory
        {

            public static Orcamento Orcamento(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoCliente cliente , string usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, cliente, usuario, vendedor, tabelaPreco);
            }
            public static Orcamento OrcamentoRapido(string cdEmpresa, string cdFilial, int numOrcamento, string usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, null, usuario, vendedor, tabelaPreco);
            }
        }

        #endregion
    }
}
