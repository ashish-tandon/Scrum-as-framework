﻿using Ferramenta_Scrumt.INFRA;
using Ferramenta_Scrumt.MODEL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Ferramenta_Scrumt.REPOSITORIO
{
    public class ProductBacklogRepositorio : ISQLRepository<ProductBacklog>
    {
        DBUtil DB = new DBUtil();

        public ProductBacklog FindByID(int ID, ISQLMapper<ProductBacklog> mapper)
        {
            return mapper.MapFromSource(DB.GetByID("SP_PBACKLOG_LIST_BYID", ID));
        }

        public List<ProductBacklog> Lista(ISQLMapper<ProductBacklog> mapper)
        {
            SqlParameter[] Param = new SqlParameter[]
            { };
            string SQL = "SELECT [ID_PBacklog],[Historia],Projeto.[Descricao],Product_Backlog.[ID_Projeto],[Aceito],[Importancia] FROM Product_Backlog Inner Join Projeto on Product_Backlog.ID_Projeto = Projeto.ID_Projeto";
            return mapper.MapAllFromSource(DB.ListaSQL(Param, SQL).Tables[0]);

        }
        public List<ProductBacklog> Lista(ISQLMapper<ProductBacklog> mapper, List<Equipe> EquipeLista)
        {
            List<int> Projetos = new List<int>();
            foreach (Equipe E in EquipeLista)
                Projetos.Add(E.IDProjeto);

            return Lista(new ProductBacklogMapper()).Where(X => Projetos.Contains(X.Projeto)).ToList();
        }
        public List<ProductBacklog> Listahist(ISQLMapper<ProductBacklog> mapper)
        {
            SqlParameter[] Param = new SqlParameter[]
            { };
            string SQL = "SELECT top 4 [ID_PBacklog],[Historia],Projeto.[Descricao],Product_Backlog.[ID_Projeto],[Aceito],[Importancia] FROM Product_Backlog Inner Join Projeto on Product_Backlog.ID_Projeto = Projeto.ID_Projeto";
            return mapper.MapAllFromSource(DB.ListaSQL(Param, SQL).Tables[0]);

        }
        public List<ProductBacklog> Listahist(ISQLMapper<ProductBacklog> mapper, List<Equipe> EquipeLista)
        {
            List<int> Projetos = new List<int>();
            foreach (Equipe E in EquipeLista)
                Projetos.Add(E.IDProjeto);

            return Listahist(new ProductBacklogMapper()).Where(X => Projetos.Contains(X.Projeto)).ToList();
        }
        public void ADD(ProductBacklog Item)
        {
            SqlParameter ID = new SqlParameter("@ID_PBacklog", SqlDbType.Int);
            ID.Direction = ParameterDirection.Output;

            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@IDProjeto",Item.Projeto),
                new SqlParameter("@Historia",Item.Historia),
                new SqlParameter("@Importancia",Item.Importancia),
                new SqlParameter("@Aceito",Item.Aceito),

                ID
            };

            DB.ExecSP("SP_PBACKLOG_INCLUIR", Param);
        }

        public void Update(ProductBacklog Item)
        {

            SqlParameter[] Param = new SqlParameter[]
            {
                 new SqlParameter("@IDProjeto",Item.Projeto),
                new SqlParameter("@Historia",Item.Historia),
                new SqlParameter("@Importancia",Item.Importancia),
                new SqlParameter("@Aceito",Item.Aceito),
                new SqlParameter("@ID",Item.ID)
            };

            DB.ExecSP("SP_PBACKLOG_UPDATE", Param);
        }

        public void Delete(ProductBacklog Item)
        {
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@ID_PBacklog",Item.ID)
            };

            DB.ExecSP("SP_PBACKLOG_DELETE", Param);

        }


    }
}