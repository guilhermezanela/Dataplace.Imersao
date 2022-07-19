namespace Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects
{
    public class usuárodoOrçamento
    {
        public usuárodoOrçamento(string usuario )
        {
            usuario = usuario;
        }
        public string usuario { get; private set; }
    }
}
