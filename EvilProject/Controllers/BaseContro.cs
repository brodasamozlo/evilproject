using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using EvilProject.Models;

namespace EvilProject.Controllers
{
    public class BaseContro : Controller
    {
        private EP_DB db = new EP_DB();

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

           

            var todoBD = db.TODO.ToList();

            ArrayList lista_projektow = new ArrayList();

            Hashtable lista_todo_do_zrobienia = new Hashtable();
            Hashtable lista_todo_zrobione = new Hashtable();

            if (todoBD != null)
            {
                foreach (TODO todo in todoBD)
                {
                    if (!lista_projektow.Contains(todo.project_name))
                        lista_projektow.Add(todo.project_name);
                }

                foreach (string nazwa_projektu in lista_projektow)
                {
                    ArrayList lista_zadan_do_wykonania = new ArrayList();
                    ArrayList lista_zadan_wykonanych = new ArrayList();
                    foreach (TODO todo in todoBD)
                    {
                        if (todo.project_name == nazwa_projektu)
                        {
                            if (todo.done_date.HasValue)
                                lista_zadan_wykonanych.Add(todo);
                            else
                                lista_zadan_do_wykonania.Add(todo);
                        }
                    }

                    lista_todo_do_zrobienia.Add(nazwa_projektu, lista_zadan_do_wykonania);
                    lista_todo_zrobione.Add(nazwa_projektu, lista_zadan_wykonanych);
                }
            }
            
            ViewData.Add("lista_todo_do_zrobienia", lista_todo_do_zrobienia);
            ViewData.Add("lista_todo_zrobione", lista_todo_zrobione);
        }
    }
}