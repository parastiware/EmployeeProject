using Employee.Repository.EmployeeRepository;
using Employee.Shared;
using Employee.Shared.EmployeeCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Employee
{
    public class EmployeeBusiness:IEmployeeBusiness
    {
        readonly IEmployeeRepository _repo;
        public EmployeeBusiness()
        {
            _repo = new EmployeeRepository();
        }

        #region Employee
        public List<EmployeeCommonModel> GetEmployees()
        {
            return _repo.GetEmployees();
        }
        public CommonDbResponse AddEmployee(EmployeeCommonModel emp)
        {
            return _repo.AddEmployee(emp);
        }
        public CommonDbResponse UpdateEmployee(EmployeeCommonModel emp)
        {
            return _repo.UpdateEmployee(emp);
        }
        #endregion


        #region Employee Qualification

        public List<QalificationsCommon> GetQualificationDropdown()
        {
            return _repo.GetQualificationDropdown();
        }


        public List<QalificationsCommon> GetQualifications(string EmpID)
        {
            return _repo.GetQualifications(EmpID);
        }
        public CommonDbResponse DeleteEmployee(string EmpID)
        {
            return _repo.DeleteEmployee(EmpID);
        }
        public CommonDbResponse ManageQualifications(List<QalificationsCommon> qualifications)
        {
            return _repo.ManageQualifications(qualifications);
        }

        public CommonDbResponse DeleteQualification(QalificationsCommon qalification)
        {
            return _repo.DeleteQualification(qalification);
        }
        #endregion
    }
}
