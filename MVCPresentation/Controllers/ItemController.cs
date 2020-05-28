using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObject;
using LogicLayer;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    public class ItemController : Controller
    {
        private ItemManager _itemManager = new ItemManager();
        private IEnumerable<String> _itemTypes;

        public ItemController()
        {
            try
            {
                _itemTypes = _itemManager.RetrieveAllItemTypes();
            }
            catch (Exception)
            {
                // redirect to error page if need be
                throw;
            }
        }


        public ActionResult Index()
        {
            IEnumerable<Item> _items = _itemManager.RetrieveItems().Where(b => b.OnStock);

            return View(_items);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            int parseId = int.Parse(id);
            Item item = _itemManager.RetrieveItemById(parseId);
            return View(item);
        }


        [Authorize(Roles = "Manager")]

        public ActionResult Create()
        {
            ViewBag.ItemTypes = _itemTypes;

            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Create(Item item)
        {

            HttpPostedFileBase file = Request.Files["myFile"];

            ViewBag.ItemTypes = _itemTypes;

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("/Content/Photos/"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        item.Picture = file.FileName;
                    }

                    if (_itemManager.AddItem(item))
                    {

                        return RedirectToAction("Index");
                    }


                }
                catch
                {
                    return View();
                }
            }
            return View(item);
        }
        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            int parseId = int.Parse(id);
            Item item = _itemManager.RetrieveItemById(parseId);

            ViewBag.ItemTypes = _itemTypes;
            return View(item);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Edit(string id, Item item)
        {
            ViewBag.ItemTypes = _itemTypes;

            HttpPostedFileBase file = Request.Files["myFile"];

            if (ModelState.IsValid)
            {
                try
                {
                   ViewBag.file = "unexisted file";
                    if (file.FileName != "")
                    {
                        string path = Path.Combine(Server.MapPath("/Content/Photos/"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        item.Picture = file.FileName;
                        ViewBag.file = "file exist";
                    }

                    int parseId = int.Parse(id);
                    Item oldItem = _itemManager.RetrieveItemById(parseId);

                    if (oldItem != null)
                    {
                        ViewBag.result = "fail to update";

                        if (_itemManager.EditItem(oldItem, item))
                        {
                            ViewBag.result = "updated";
                           // return RedirectToAction("Index");
                        }

                    }
                  

                }
                catch(Exception ex)
                {
                    ViewBag.error = ex.Message + " " + ex.Source;
                 return View();
                }
            }

            return View(item);
            
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult DeletePermanently(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            int parseId = int.Parse(id);
            Item item = _itemManager.RetrieveItemById(parseId);

            return View(item);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult DeletePermanently(string id, FormCollection collection)
        {
            int parseId = int.Parse(id);
            try
            {
                if (_itemManager.Delete(parseId))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            int parseId = int.Parse(id);
            Item item = _itemManager.RetrieveItemById(parseId);

            return View(item);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            int parseId = int.Parse(id);
            try
            {
                if (_itemManager.DeactivateItemByID(parseId))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Manage()
        {
            IEnumerable<Item> _items = _itemManager.RetrieveItems().Where(b => b.OnStock == false);

            return View(_items);
        }


        public ActionResult Reactivate(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            int parseId = int.Parse(id);
            Item item = _itemManager.RetrieveItemById(parseId);
            return View(item);
        }
    

        [HttpPost]
        public ActionResult Reactivate(string id, FormCollection collection)
        {
            try
            {
                int parseId = int.Parse(id);

                if (_itemManager.ReactivateItemByID(parseId))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}