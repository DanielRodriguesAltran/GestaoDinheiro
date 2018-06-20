﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GestaoDinheiro.Models;
using GestaoDinheiro.Repository;

namespace GestaoDinheiro.Controllers
{
    [Authorize]
    public class MovimentoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string userId;

        private string GetUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value;
                }
            }
            return userId;
        }
        // GET: Movimentoes

        public ActionResult Index(int selMes = 0)
        {
            ViewBag.selMes = selMes;
            userId = GetUserId();
            ViewBag.UserId = userId;
            if (selMes > 0)
            {
                var mov = db.Movimentos.Where(x => x.Dono == userId).Where(s => s.DataTime.Month == selMes).Take(10).ToList();
                return View(mov);
            }
            return View(db.Movimentos.Where(x => x.Dono == userId).OrderByDescending(x => x.DataTime).ToList());
        }

        public ActionResult CharterColumn(int selMes = 0)
        {
            if (selMes == 0)
            {
                selMes = DateTime.Now.Month;
            }
            userId = GetUserId();
            var _context = new ApplicationDbContext();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var res = db.Movimentos.Where(y => y.Dono == userId).Where(x => x.DataTime.Month == selMes).GroupBy(d => d.Tipo)
            .Select(
                m => new
                {
                    Valor = m.Sum(s => s.Valor),
                    Tipo = m.FirstOrDefault().Tipo.ToString()
                }).ToList();
            res.ToList().ForEach(rs => xValue.Add(rs.Tipo));
            res.ToList().ForEach(rs => yValue.Add(rs.Valor));

            new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
                .AddTitle("Movimentos de " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(selMes))
                .AddSeries("Default", chartType: "Doughnut", xValue: xValue, yValues: yValue)
                .AddLegend("Legenda")
                .Write("bmp");
            return null;
        }

        public ActionResult CharterColumn2(double Gasto, double Rec, double Luc)
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            if (Luc < 0)
            {
                Luc = 0;
            }
            xValue.Add("Recebeu " + Rec + "€");
            xValue.Add("Gastou " + Gasto + "€");
            xValue.Add("Lucrou " + Luc + "€");
            yValue.Add(Rec);
            yValue.Add(Gasto);
            yValue.Add(Luc);
            new Chart(width: 600, height: 400, theme: CostumColor.MyCustom)

                .AddTitle("Resumo")
                .AddSeries("Default", chartType: "Doughnut", xValue: xValue, yValues: yValue)
                .AddLegend("Legenda")
                .Write("bmp");
            return null;
        }

        // GET: Movimentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimento movimento = db.Movimentos.Find(id);
            if (movimento == null)
            {
                return HttpNotFound();
            }
            return View(movimento);
        }

        // GET: Movimentoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movimentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DataTime,Local,Descricao,Valor,Saldo_Atual,Tipo")] Movimento movimento)
        {
            userId = GetUserId();

            if (userId != null)
            {
                movimento.Dono = userId;
                ModelState["Dono"].Errors.Clear();
                if (ModelState.IsValid)
                {
                    db.Movimentos.Add(movimento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(movimento);
        }

        // GET: Movimentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimento movimento = db.Movimentos.Find(id);
            if (movimento == null)
            {
                return HttpNotFound();
            }
            return View(movimento);
        }

        // POST: Movimentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DataTime,Local,Descricao,Valor,Saldo_Atual,Tipo")] Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movimento);
        }

        // GET: Movimentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimento movimento = db.Movimentos.Find(id);
            if (movimento == null)
            {
                return HttpNotFound();
            }
            return View(movimento);
        }

        // POST: Movimentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movimento movimento = db.Movimentos.Find(id);
            db.Movimentos.Remove(movimento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            MovimentosRepository repo = new MovimentosRepository(db);
            List<Movimento> movimentos = new List<Movimento>();
            userId = GetUserId();
            var path = "";
            if (upload != null && upload.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(upload.FileName);
                // store the file inside ~/App_Data/uploads folder
                var fh = "~/App_Data/uploads/" + User.Identity.Name + "/";
                path = Path.Combine(Server.MapPath(fh) + fileName);
                upload.SaveAs(path);
            }
            ConnexionExcel ConxObject = new ConnexionExcel(path);
            //Query a worksheet with a header row  

            var query1 = from a in ConxObject.UrlConnexion.Worksheet<Movimento>(0)
                         select a;
            foreach (var result in query1)
            {
                if (!String.IsNullOrEmpty(result.Descricao))
                {
                    Tipo t;
                    if (result.Descricao.Contains("-LEV-") && result.Valor < 0)
                    {
                        t = Tipo.Levantamento;
                    }
                    else if ((result.Descricao.Contains("TRF") || result.Descricao.Contains("TRANS")) && result.Valor < 0)
                    {
                        t = Tipo.Transferencia;
                    }
                    else if ((result.Descricao.Contains("TRF") || result.Descricao.Contains("TRANS") || result.Descricao.Contains("DEP")) && result.Valor > 0)
                    {
                        t = Tipo.Deposito;
                    }
                    else if (result.Valor > 0)
                    {
                        t = Tipo.Recebido;
                    }
                    else
                    {
                        t = Tipo.Pagamento;
                    }
                    Movimento m = new Movimento
                    {
                        DataTime = result.DataTime,
                        Descricao = result.Descricao,
                        Valor = result.Valor,
                        Saldo_Atual = result.Saldo_Atual,
                        Tipo = t,
                        Dono = userId
                    };
                    movimentos.Add(m);
                    repo.Insert(m);
                }


            }
            repo.Commit();
            return View();
        }

        public ActionResult RemoveAll()
        {
            MovimentosRepository repo = new MovimentosRepository(db);
            repo.RemoveAll();
            return RedirectToAction("Upload");
        }

        public ActionResult DeleteFiles(string name)
        {

            string fullPath = Request.MapPath("~/App_Data/uploads/" + User.Identity.Name + "/" + name);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            return RedirectToAction("Upload");
        }




    }
}