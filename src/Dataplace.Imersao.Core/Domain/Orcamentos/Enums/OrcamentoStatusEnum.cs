using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.Enums
{
    public enum OrcamentoStatusEnum
    {
        Aberto,
        Fechado,
        Cancelado
    }

    public static class OrcamentoStatusEnumExtensions
    {
        public static string ToDataValue(this OrcamentoStatusEnum value)
        {
            return value == OrcamentoStatusEnum.Fechado ? "F" : "P";
        }
        public static OrcamentoStatusEnum ToOrcamentoStatusEnum(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return OrcamentoStatusEnum.Aberto;

            if (value == "P")
                return OrcamentoStatusEnum.Fechado;
            else
                return OrcamentoStatusEnum.Aberto;
        }
    }
}
