using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db;
    public ClientsController(HairSalonContext db)
    {
      _db = db;
    }

    [HttpGet, ActionName("Index")]
    public ActionResult Index()
    {
      List<Client> model = _db.Clients.Include(client => client.Stylist).ToList();
      return View(model);
    }

    [HttpGet, ActionName("Create")]
    public ActionResult Create(int id)
    {
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
      return View();
    }

    [HttpPost, ActionName("Create")]
    public ActionResult Create(Client client)
    {
      _db.Clients.Add(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet, ActionName("Details")]
    public ActionResult Details(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
      ViewBag.Stylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == thisClient.StylistId);
      return View(thisClient);
    }

    [HttpGet, ActionName("Edit")]
    public ActionResult Edit(int id)
    {
      var thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
      return View(thisClient);
    }

    [HttpPost, ActionName("Edit")]
    public ActionResult Edit(Client client)
    {
      _db.Entry(client).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet, ActionName("Delete")]
    public ActionResult Delete(int id)
    {
      var thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
      return View(thisClient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
      _db.Clients.Remove(thisClient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
