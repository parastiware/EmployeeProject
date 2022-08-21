using Employee.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.Employee;
using Employee.Shared.EmployeeCommon;
using Newtonsoft.Json;
using Employee.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeBusiness _EmpBusiness;
        [TempData]
        public string SuccessMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        public EmployeeController()
        {
            _EmpBusiness = new EmployeeBusiness();
        }

        public IActionResult Index()
        {
            
            EmployeeViewModel employeeView =new EmployeeViewModel();
            employeeView.EmployeeList = _EmpBusiness.GetEmployees();
            employeeView.QualificationList = _EmpBusiness.GetQualificationDropdown();
            return View(employeeView);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Manage(EmployeeModel employee,string qlist)
        {
            CommonDbResponse response = new CommonDbResponse();

            if (ModelState.IsValid)
            {
                EmployeeCommonModel employeeCommon = new EmployeeCommonModel()
                { Id = employee.Id,
                    Name = employee.Name,
                    DOB = employee.DOB,
                    Salary = employee.Salary,
                    Gender = employee.Gender,
                };

                if (string.IsNullOrEmpty(employee.Id))
                {
                    try
                    {
                        CreateEmployee(employeeCommon, qlist);
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage= ex.Message;
                    }
                }
                else
                {
                    try
                    {
                        UpdateEmployee(employeeCommon, qlist);
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                   
                }

            }
            return RedirectToAction("Index");
        }
        void CreateEmployee(EmployeeCommonModel employeeCommon ,string qlist)
{
        CommonDbResponse response = _EmpBusiness.AddEmployee(employeeCommon);
        if (response.Code == ResponseCode.Success)
        {
            SuccessMessage = response.Message;
            var Qlist = JsonConvert.DeserializeObject<IEnumerable<QualificationModel>>(qlist);
            List<QalificationsCommon> qalificationsCommons = new List<QalificationsCommon>();
            foreach (var item in Qlist)
            {
                var qualification = new QalificationsCommon()
                {
                    employeeId = response.Extra1,
                    QId = item.QId,
                    Name = item.QName,
                    Marks = item.Marks,
                };
                qalificationsCommons.Add(qualification);
            }
            _EmpBusiness.ManageQualifications(qalificationsCommons);
        }
        else
        {
            ErrorMessage = response.Message;
        }
    }

        void UpdateEmployee(EmployeeCommonModel employeeCommon, string qlist)
        {
            CommonDbResponse response = _EmpBusiness.UpdateEmployee(employeeCommon);
            if (response.Code == ResponseCode.Success)
            {
                SuccessMessage = response.Message;
            }
            else
            {
                ErrorMessage = response.Message;
            }
            var Qlist = JsonConvert.DeserializeObject<IEnumerable<QualificationModel>>(qlist);
            List<QalificationsCommon> qalificationsCommons = new List<QalificationsCommon>();
            foreach (var item in Qlist)
            {
                var qualification = new QalificationsCommon()
                {
                    employeeId = employeeCommon.Id,
                    QId = item.QId,
                    Name = item.QName,
                    Marks = item.Marks,
                };
                qalificationsCommons.Add(qualification);
            }
            _EmpBusiness.ManageQualifications(qalificationsCommons);
        }
      

        [HttpPost,ValidateAntiForgeryToken]
      public JsonResult  GetQualifications(string Id)
        {
            var data = _EmpBusiness.GetQualifications(Id);
            return Json(new { data = data });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(string Id)
        {
            var response = _EmpBusiness.DeleteEmployee(Id);
            if(response.Code==ResponseCode.Success)
            {
                SuccessMessage=response.Message;
            }
            else
            {
                ErrorMessage=response.Message;
            }
            return Json(response.Message);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult DeleteQualification(string QId ,string EId)
        {
            QalificationsCommon qalificationsCommons = new QalificationsCommon();
            qalificationsCommons.QId = QId; 
            qalificationsCommons.employeeId = EId;
            var response = _EmpBusiness.DeleteQualification(qalificationsCommons);
            if (response.Code == ResponseCode.Success)
            {
                SuccessMessage = response.Message;
            }
            else
            {
                ErrorMessage = response.Message;
            }
            return Json(response.Message);
        }


    }
}
