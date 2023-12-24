﻿using Ferramenta_Scrumt.FILTERS;
using Ferramenta_Scrumt.MODEL;
using Ferramenta_Scrumt.REPOSITORIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ferramenta_Scrumt.Controllers
{
    [Authorize]
    [Filterlisttest]
    public class EquipeController : Controller
    {
        
        List<Users> UsersList;
        List<Equipe> EquiList;
        EquipeRepositorio _EquiRep = new EquipeRepositorio();
        UsersRepositorio _UsersRep = new UsersRepositorio();
        List<Projeto> ProjList;
        ProjetoRepositorio _ProjRep = new ProjetoRepositorio();


        private void CarregaLista()
        {
            EquiList = _EquiRep.Lista(new EquipeMapper());
            Session["Lista"] = EquiList;
        }
        public ActionResult Index()
        {
            CarregaLista();

            return View(EquiList);
        }
        [HttpPost]
        public ActionResult Create(Equipe E)
        {
            _EquiRep.ADD(E);
            Session["Lista"] = EquiList;
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            CarregaLista();
            ProjList = _ProjRep.ListaProj(new ProjetoMapper(), "Select top 1 ID_Projeto,Descricao, Data_Inicio, Data_Fim from Projeto order by ID_Projeto desc");
            ViewBag.Projeto = new SelectList(ProjList, "ID", "Descricao");

            UsersList = _UsersRep.Lista(new UsersMapper());
            ViewBag.Nome_Membro = new SelectList(UsersList, "ID", "Nome");
            return View();
        }

    }
}