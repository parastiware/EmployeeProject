using Employee.Shared;
using Employee.Shared.EmployeeCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Repository.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        #region Employee 
        public List<EmployeeCommonModel> GetEmployees();
        public CommonDbResponse AddEmployee(EmployeeCommonModel emp);
        public CommonDbResponse UpdateEmployee(EmployeeCommonModel emp);
        public CommonDbResponse DeleteEmployee(string EmpID);
        #endregion

        #region Employee Qualification
        public List<QalificationsCommon> GetQualificationDropdown();
        public List<QalificationsCommon> GetQualifications(string EmpID);
        public CommonDbResponse ManageQualifications(List<QalificationsCommon> qalifications);
        public CommonDbResponse DeleteQualification(QalificationsCommon qalification);
        #endregion
    }
}
