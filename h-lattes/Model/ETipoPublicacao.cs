using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// Tipos de publicação.
    /// </summary>
    public enum ETipoPublicacao
    {
        ArtigosCompletosPeriodicos = 1,
        LivrosPublicados = 2,
        TrabalhosPublicadosAnais = 3,
        CapitulosLivros = 4,
        ResumosExpandidos = 5,
        ResumosPublicados = 6,
        ArtigosAceitosPublicacao,
        TextosJornaisRevistas,
        DemaisTipos,
    }
}
