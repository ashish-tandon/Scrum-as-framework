﻿using System;

namespace Ferramenta_Scrumt.MODEL
{
    public class TesteUnidade
    {
        public TesteUnidade()
        {

        }
        public int ID { get; set; }
        public string Membro { get; set; }
        public int ID_Membro { get; set; }
        public string Historia { get; set; }
        public int ID_Backlog { get; set; }
        public string Classe { get; set; }
        public string Status { get; set; }
    }
}