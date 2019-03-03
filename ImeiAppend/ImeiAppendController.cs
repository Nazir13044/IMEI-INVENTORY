using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WcfService2.Services;
using WCMS_COMMON;
using WCMS_ENTITY;
using WCMS_MAIN.HelperClass;

namespace WCMS_MAIN.Controllers
{
    public class ImeiAppendController : Controller
    {

       
        private readonly ImeiAppendService _imeiAppendService = new ImeiAppendService();
     
     
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InventoryOut()
        {
            return View();
        }
       
        public JsonResult GetModelName()
        {
            List<tblModel> list = new List<tblModel>();
            try
            {
                list = _imeiAppendService.GetModelName().ToList();
            }
            catch (Exception ex)
            {

            }
            var objstate = list.Select(x => new { value = x.Model, Id = x.Id }).ToList();
            return Json(objstate.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetColorName()
        {
            List<tblColors> list = new List<tblColors>();
            try
            {
                list = _imeiAppendService.GetColorName().ToList();
            }
            catch (Exception ex)
            {

            }
            var objstate = list.Select(x => new { value = x.Name, Id = x.Id }).ToList();
            return Json(objstate.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertImeiInfo(List<tblIMEI> imeiNumber)
        {
            var result = new Result();
            try
            {

                
                foreach (var im in imeiNumber)
                {
                    //var newlskkkit = new tblNewBarCodeInventory();

                    var existImei =
                       _imeiAppendService.ImeiExists(new tblIMEIRecord(){IMEI1 =im.IMEI1 }).FirstOrDefault();

                    if (existImei == null)
                    {
                       
                    result = _imeiAppendService.InsertImeiInfo(im);
                    }
                    else
                    {
                        im.IMEI1 = existImei.IMEI1;
                        im.IMEI2 = existImei.IMEI2;
                        im.Model = existImei.Model;
                        im.Color = existImei.Color;
                        im.Sn = existImei.Sn;
                        im.WO = existImei.WO;
                        result = _imeiAppendService.InsertImeiInfo(im);

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(result);


        }


        public JsonResult InsertImeiInventory(List<tblIMEI> imeiNumber)
        {
            var result = new Result();
            try
            {
                foreach (var im in imeiNumber)
                {
                    var existImei =
                       _imeiAppendService.ImeiCheck(new tblIMEI() { IMEI1 = im.IMEI1 });

                    if (existImei == null)
                    {

                        result = _imeiAppendService.InsertImeiInfo(im);
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Current IMEI Already Exists";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(result);


        }
	}
}