using DCI_NETCORE_API_TEMPLATE.Models;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DCI_NETCORE_API_TEMPLATE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : Controller
    {
        //private OraConnectDB ORA = new OraConnectDB("ORA_DATABASE_NAME");
        //private SqlConnectDB SQL = new SqlConnectDB("SQL_DATABASE_NAME");
        private OraConnectDB ORA_ALPHAPD = new OraConnectDB("ALPHAPD");
        private OraConnectDB ORA_ALPHA01 = new OraConnectDB("ALPHA01");
       

        [HttpGet]
        [Route("/get")]
        public IActionResult GetData(string param)
        {

            return Ok();
        }

        [HttpPost]
        [Route("/post")]
        public IActionResult PostData([FromBody] M_GET_PLAN_PKG param)
        {
            string sDate = param.sDate;
            string fDate = param.fDate;
            string wcno = param.wcno;
            List<GSD_ACT_PLN_PKG> ListPlanPKG = new List<GSD_ACT_PLN_PKG>();
            OracleCommand strGetPlanPK = new OracleCommand();
            strGetPlanPK.CommandText = @"SELECT G.WCNO, G.MODEL, G.PLTYPE, G.PRDYMD, SUM(G.QTY) QTY, SUM(G.PLQTY) PLQTY
FROM PLAN.GSD_ACTPLN_PKG G
WHERE PRDYMD BETWEEN :sDate and :fDate and wcno = :wcno
GROUP BY G.WCNO, G.MODEL, G.PLTYPE, G.PRDYMD";
            strGetPlanPK.Parameters.Add(new OracleParameter(":sDate", sDate));
            strGetPlanPK.Parameters.Add(new OracleParameter(":fDate", fDate));
            strGetPlanPK.Parameters.Add(new OracleParameter(":wcno", wcno));
            DataTable dtPlanPk = ORA_ALPHA01.Query(strGetPlanPK);
            foreach (DataRow dr in dtPlanPk.Rows)
            {
                GSD_ACT_PLN_PKG item = new GSD_ACT_PLN_PKG();
                item.MODEL = dr["MODEL"].ToString();
                item.WCNO = dr["WCNO"].ToString();
                item.PLTYPE = dr["PLTYPE"].ToString();
                item.PLQTY = dr["PLQTY"].ToString() != "" ? int.Parse(dr["PLQTY"].ToString()) : 0;
                item.QTY = dr["QTY"].ToString() != "" ? int.Parse(dr["QTY"].ToString()) : 0;
                ListPlanPKG.Add(item);
            }
            return Ok(ListPlanPKG);
        }
    }
}
