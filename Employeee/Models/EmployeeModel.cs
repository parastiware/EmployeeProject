using Employee.Shared.EmployeeCommon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Employee.Application.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            QualificationList = new List<QualificationModel>();
        }
        [Display(Name = "Employee Id")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Employee name is required!!")]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of birth is required!!")]
        [Display(Name="Date Of Birth")]
        public string DOB { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender is required!!")]
        public string Gender { get; set; }
        public string Salary { get; set; }
        public List<QualificationModel> QualificationList;

    }
    public class QualificationModel
    {
        public string QId { get; set; }

        [Display(Name = "Qualification Name")]
        public string QName { get; set; }

        [RegularExpression(@"[\d]",ErrorMessage ="Numbers only")]
        public string Marks { get; set; }
    }

    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            EmployeeList = new List<EmployeeCommonModel>();
            QualificationList = new List<QalificationsCommon>();
            Employee = new EmployeeModel();
            Qualification= new QualificationModel();
        }
        public List<EmployeeCommonModel> EmployeeList;
        public List<QalificationsCommon> QualificationList;
        public EmployeeModel Employee;
        public QualificationModel Qualification;
       

    }
}
