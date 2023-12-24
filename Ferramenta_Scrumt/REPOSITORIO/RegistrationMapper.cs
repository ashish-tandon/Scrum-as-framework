﻿using Ferramenta_Scrumt.MODEL;
using System.Data;

namespace Ferramenta_Scrumt.REPOSITORIO
{
    public class RegistrationMapper : SqlMapperBase<Users>
    {
        public override Users MapFromSource(DataRow Record)
        {
            Users equi = new Users();

            equi.ID = (int)Record["ID_Equipe"];
            equi.Nome = (string)Record["Nome"];
            equi.Email = (string)Record["Email"];
            equi.Funcao = (int)Record["ID_Funcao"];
            equi.Senha = (string)Record["Senha"];
            return equi;
        }
    }
}