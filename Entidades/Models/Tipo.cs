using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Models
{
    public enum Tipo
    {
        Levantamento,
        Transferencia,
        Deposito,
        Recebido,
        Pagamento
    }

    public enum Departamento
    {
        Escolha,
        Supermecado,
        Restaurante,
        Educaçao,
        Transportes,
        Saude,
        ViaVerde,
        Ginasio,
        Telemoveis,
        Levantamento,
        Vestuário,
        GestaoConta,
        Outros
    }
}