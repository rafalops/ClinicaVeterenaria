using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaVets.Models;

namespace ClinicaVets.Controllers
{
    [Authorize]

    public class DonosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Donos
        public ActionResult Index()
        {
            //mostrar os dados de todos os Donos apenas aos utilizadores de perfil 'Funcionario' ou perfil 'veterinario'
            if(User.IsInRole("Funcionario") || User.IsInRole("Veterinario"))
            {
                return View(db.Donos.ToList().OrderBy(d => d.DonoID));
            }    
            //senão mostra os dados do utilizador autenticado
            return View(db.Donos.Where(d => d.NomeDoUtilizador == User.Identity.Name).ToList());

        }

        // GET: Donos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // GET: Donos/Create
        [Authorize(Roles="Funcionario")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Funcionario")]
        public ActionResult Create([Bind(Include = "Nome,NIF")] Donos dono)
        {
            //Determinar o ID a atribuir ao novo DONO
            //Criar a var que recebe esse valor
            int novoID = 0;
            
            try
            {
                //Determinar o novo ID
                /*novoID =(from d in db.Donos
                    orderby d.DonoID descending
                    select d.DonoID).FirstOrDefault() +1;*/

                //Outra forma
                novoID = db.Donos.Max(d => d.DonoID) + 1;
            }
            catch (System.Exception){
                //A tabela donos está vazia, não sendo possivel devolver o Max de uma tabela
                //Atribuir manualmente o ID ao primeiro Dono
                novoID = 1;
            }
    
            //Atribuir o 'novoID' ao objeto 'dono'
            dono.DonoID = novoID;

            try
            {
                if (ModelState.IsValid)
                {
                    db.Donos.Add(dono);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {
                //não consigo guardar as alterações
                //no mínimo, preciso de notoficar o utilizador que o processo falhou
                ModelState.AddModelError("", "Ocorreu um erro na adição do novo Dono");
                //notificar o admin  que ocorreu este erro, fazer: 1ºenviar email ao programador,
                //2º ter uma tabela na BD, onde são reportados os erros:-data-metodo-controller-detalhes do erro
            }
            return View(dono);
        }//Fecha o public ActionResult Create

        // GET: Donos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // POST: Donos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonoID,Nome,NIF")] Donos donos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donos);
        }

        // GET: Donos/Delete/5
        public ActionResult Delete(int? id)
        {
            //Avalia se o parâmetro é nulo
            if (id == null)
            {
                //Redireciona a página para o inicio
                return RedirectToAction("Index"); 
            }
            Donos dono = db.Donos.Find(id);
            //Se o dono nao é encontrado...
            if (dono == null)
            {
                //Redirecionar para a pagina inicial
                return RedirectToAction("Index");
            }
            return View(dono);
        }

        // POST: Donos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Procurar o 'dono' na BD, cula PK é igual ao parametro fornecido -id-
            Donos dono = db.Donos.Find(id);
            try{
                //Remove do objeto 'db' , o dono encontrado na linha anterior 
                db.Donos.Remove(dono);
                //Torna definitivas as instruçoes anteriores 
                db.SaveChanges();
            }
            catch (Exception){
                //Gerar uma mensagem de erro a ser entregue ao utilizador
                ModelState.AddModelError("",
                    string.Format("Ocorreu um erro na operação de eliminar o 'dono' com ID {0} - {1}", id, dono.Nome)
                );
                //Regressar á view 'delete'
                return View(dono);
            }
            //devolve o controlo do programa, apresentando a view 'Index'
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
    }
}
