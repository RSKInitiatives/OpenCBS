using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Free.iso8583;
using Free.iso8583.example;
using Free.iso8583.example.model;
using System.Reflection;
using Iso8583WebClient.Models;

namespace Iso8583WebClient.Controllers
{
    public class MessageEditorController : Controller
    {
        public ActionResult Index(String editedItem = "")
        {
            Session["requestParams"] = new Dictionary<String, Object>() {  
                { "ServerHost", Request.QueryString["host"] },
                { "ServerPort", Request.QueryString["port"] },
                { "IsSSL", Request.QueryString["isSSl"] != null},
                { "EditedItem", editedItem}
            };

            if (!EditedItems.Item.ContainsKey(editedItem)) return Redirect("~/");
            ViewBag.EditedItem = editedItem;
            ViewBag.Panel = EditedItems.Item[editedItem].Panel;
            
            byte[] accountCodes = (byte[])Enum.GetValues(typeof(AccountCodeBytes));
            List<SelectListItem> accountCodeList = new List<SelectListItem>();
            foreach (byte accountCode in accountCodes)
            {
                String accountCodeString = MessageUtility.HexToString(accountCode);
                accountCodeList.Add(
                    new SelectListItem()
                    {
                        Value = accountCodeString,
                        Text = accountCodeString + " - " + ProcessingCode.ACCOUNT[(AccountCodeBytes)accountCode]
                    }
                );
            }
            ViewBag.AccountCodeList = accountCodeList;

            byte[] mediaModeCodes = (byte[])Enum.GetValues(typeof(PosEntryMediaModeBytes));
            List<SelectListItem> posEntryMediaList = new List<SelectListItem>();
            foreach (byte mediaModeCode in mediaModeCodes)
            {
                String mediaModeCodeString = MessageUtility.HexToString(mediaModeCode);
                posEntryMediaList.Add(
                    new SelectListItem()
                    {
                        Value = mediaModeCodeString,
                        Text = mediaModeCodeString + " - " + POSEntryMode.MEDIA[(PosEntryMediaModeBytes)mediaModeCode]
                    }
                );
            }
            ViewBag.PosEntryMediaList = posEntryMediaList;

            byte[] pinModeCodes = (byte[])Enum.GetValues(typeof(PosEntryPinModeBytes));
            List<SelectListItem> posEntryPinList = new List<SelectListItem>();
            foreach (byte pinModeCode in pinModeCodes)
            {
                String pinModeCodeString = MessageUtility.HexToString(pinModeCode).Substring(1);
                posEntryPinList.Add(
                    new SelectListItem()
                    {
                        Value = pinModeCodeString,
                        Text = pinModeCodeString + " - " + POSEntryMode.PIN_ENTRY[(PosEntryPinModeBytes)pinModeCode]
                    }
                );
            }
            ViewBag.PosEntryPinList = posEntryPinList;

            byte[] posConditionCodes = (byte[])Enum.GetValues(typeof(PosConditionCodeBytes));
            List<SelectListItem> posConditionList = new List<SelectListItem>();
            foreach (byte posConditionCode in posConditionCodes)
            {
                String posConditionCodeString = MessageUtility.HexToString(posConditionCode);
                posConditionList.Add(
                    new SelectListItem()
                    {
                        Value = posConditionCodeString,
                        Text = posConditionCodeString + " - " + POSCondition.DESCRIPTION[(PosConditionCodeBytes)posConditionCode]
                    }
                );
            }
            ViewBag.PosConditionList = posConditionList;

            Object model = EditedItems.Item[editedItem].DefaultModel;
            ViewBag.TransactionDescription = "";
            ViewBag.AdditionalField1 = "";
            ViewBag.AdditionalField2 = "";
            if (model is Request0200SB) ViewBag.TransactionDescription = (model as Request0200SB).TransactionDescription;
            if (model is Request0200TB)
            {
                ViewBag.AdditionalField1 = (model as Request0200TB).AdditionalField1;
                ViewBag.AdditionalField2 = (model as Request0200TB).AdditionalField2;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index()
        {
            Type modelType = EditedItems.Item[Request.Form["_EditedItem"]].DefaultModel.GetType();
            Object model = Activator.CreateInstance(modelType);
            String[] inputNames = Request.Form.AllKeys;
            foreach (String inputName in inputNames)
            {
                if (inputName.IndexOf("chk") == 0 || inputName == "_EditedItem" || String.IsNullOrEmpty(inputName)) continue;
                Object obj = model;
                String value = Request.Form[inputName];
                String[] propNames = inputName.Split('.');
                PropertyInfo propInfo = modelType.GetProperty(propNames[0]);
                if (Request.Form["chk" + propNames[0]] != null) //The property value is null
                {
                    propInfo.SetValue(model, null, null);
                }
                else
                {
                    Type propType = propInfo.PropertyType;
                    if (propNames.Length > 1)
                    {
                        if (propNames[0] == "AdditionalData") propType = typeof(RequestDataEntry48);
                        if (propInfo.GetValue(model, null) == null)
                        {
                            obj = Activator.CreateInstance(propType);
                            propInfo.SetValue(model, obj, null);
                        }
                        else
                        {
                            obj = propInfo.GetValue(model, null);
                        }
                        propInfo = propType.GetProperty(propNames[1]);
                        propType = propInfo.PropertyType;
                    }

                    //propType.IsValueType

                    if (propType == typeof(String)) propInfo.SetValue(obj, value, null);
                    else if (propType == typeof(int) || propType == typeof(int?))
                        propInfo.SetValue(obj, int.Parse("0" + value), null);
                    else if (propType == typeof(decimal) || propType == typeof(decimal?))
                        propInfo.SetValue(obj, decimal.Parse("0" + value), null);
                    else if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                        propInfo.SetValue(obj, DateTimeConverter.GetDateTime(value), null);
                    else if (propType == typeof(byte[]))
                        propInfo.SetValue(obj, MessageUtility.StringToHex(value), null);
                }
            }

            Session["model"] = model;
            return Redirect("~/");
        }
    }
}
