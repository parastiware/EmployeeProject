using Employee.Shared;
using Employee.Shared.EmployeeCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace Employee.Repository.EmployeeRepository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        
        readonly  Repo repository;
        public EmployeeRepository()
        {
            repository=new Repo();
        }
        public List<EmployeeCommonModel> GetEmployees()
        {
            List<EmployeeCommonModel> EmployeeList = new List<EmployeeCommonModel>();
            string procName = "EmployeeManagement";
            List<SqlParameters> parameters = new List<SqlParameters>();
            parameters.Add(new SqlParameters { ParameterName = "@action", ParameterValue = "list" });

            var data = repository.ExecuteDataTable(procName, parameters);

            if (data != null)
            {
                foreach (DataRow dr in data.Rows)
                {
                    EmployeeCommonModel common = new EmployeeCommonModel();
                    common.Id = dr["Employee_id"].ToString();
                    common.Name = dr["Emp_name"].ToString();
                    common.Gender = dr["gender"].ToString();
                    common.Salary = dr["salary"].ToString();
                    common.JoinDate = !string.IsNullOrEmpty(dr["Entry_date"].ToString())?Convert.ToDateTime(dr["Entry_date"]).ToString("yyyy-MM-dd"):"";
                    common.DOB = !string.IsNullOrEmpty(dr["dob"].ToString())?Convert.ToDateTime(dr["dob"]).ToString("yyyy-MM-dd") :"";

                    EmployeeList.Add(common);
                }


            }

            return EmployeeList;
        }


        public CommonDbResponse AddEmployee(EmployeeCommonModel emp)
        {

            string procName = "EmployeeManagement";
            List<SqlParameters> parameters = new List<SqlParameters>();
            parameters.Add(new SqlParameters { ParameterName = "@action", ParameterValue = "create" });
            parameters.Add(new SqlParameters { ParameterName = "@employeeName", ParameterValue = emp.Name });
            parameters.Add(new SqlParameters { ParameterName = "@gender", ParameterValue = emp.Gender });
            parameters.Add(new SqlParameters { ParameterName = "@salary", ParameterValue = emp.Salary });
            parameters.Add(new SqlParameters { ParameterName = "@dob", ParameterValue = emp.DOB });
            return repository.ParseCommonDbResponse(repository.ExecuteDataTable(procName, parameters));
        }

        public CommonDbResponse UpdateEmployee(EmployeeCommonModel emp)
        {

            string procName = "EmployeeManagement";
            List<SqlParameters> parameters = new List<SqlParameters>();
            parameters.Add(new SqlParameters { ParameterName = "@action", ParameterValue = "update" });
            parameters.Add(new SqlParameters { ParameterName = "@employeeName", ParameterValue = emp.Name });
            parameters.Add(new SqlParameters { ParameterName = "@emp_id", ParameterValue = emp.Id });
            parameters.Add(new SqlParameters { ParameterName = "@gender", ParameterValue = emp.Gender });
            parameters.Add(new SqlParameters { ParameterName = "@salary", ParameterValue = emp.Salary });
            parameters.Add(new SqlParameters { ParameterName = "@dob", ParameterValue = emp.DOB });
            return repository.ParseCommonDbResponse(repository.ExecuteDataTable(procName, parameters));
        }
        public CommonDbResponse DeleteEmployee(string EmpID)
        {

            string procName = "EmployeeManagement";
            List<SqlParameters> parameters = new List<SqlParameters>();
            parameters.Add(new SqlParameters { ParameterName = "@action", ParameterValue = "delete" });
            parameters.Add(new SqlParameters { ParameterName = "@emp_id", ParameterValue = EmpID });
            return repository.ParseCommonDbResponse(repository.ExecuteDataTable(procName, parameters));
        }

        public List<QalificationsCommon> GetQualificationDropdown()
        {
            List<QalificationsCommon> QualificationList = new List<QalificationsCommon>();
            string procName = "EmployeeManagement";
            List<SqlParameters> parameters = new List<SqlParameters>();
            parameters.Add(new SqlParameters { ParameterName = "@action", ParameterValue = "qualification-dropdown" });
           
            var data = repository.ExecuteDataTable(procName, parameters);

            if (data != null)
            {
                foreach (DataRow dr in data.Rows)
                {
                    QalificationsCommon common = new QalificationsCommon();
                    common.QId = dr["Q_id"].ToString();
                    common.Name = dr["Q_name"].ToString();
                    QualificationList.Add(common);
                }

            }
            return QualificationList;

        }

        public List<QalificationsCommon> GetQualifications(string EmpID)
        {
            List<QalificationsCommon> QualificationList = new List<QalificationsCommon>();
            string procName = "EmployeeManagement";
            List<SqlParameters> parameters = new List<SqlParameters>();
            parameters.Add(new SqlParameters { ParameterName = "@action", ParameterValue="qualification-get" });
            parameters.Add(new SqlParameters { ParameterName = "@emp_id", ParameterValue= EmpID });
            var data = repository.ExecuteDataTable(procName, parameters);
            if (data != null)
            {
                foreach (DataRow dr in data.Rows)
                {
                    QalificationsCommon common = new QalificationsCommon();
                    common.QId = dr["Q_id"].ToString();
                    common.employeeId = dr["Employee_id"].ToString();
                    common.Marks = dr["Marks"].ToString();
                    common.Name = dr["Q_name"].ToString();
                    
                    QualificationList.Add(common);
                }

            }
            return QualificationList;

           
        }
    
        
        public CommonDbResponse ManageQualifications(List<QalificationsCommon> qalifications)
        {
            CommonDbResponse common = new CommonDbResponse();
            string procName = "EmployeeManagement";

            foreach (var qalification in qalifications)
            {
                List<SqlParameters> parameters = new List<SqlParameters>();
                parameters.Add(new SqlParameters { ParameterName = "@action", ParameterValue = "qualification-manage" });
                parameters.Add(new SqlParameters { ParameterName = "@emp_id", ParameterValue = qalification.employeeId });
                parameters.Add(new SqlParameters { ParameterName = "@Q_id", ParameterValue = qalification.QId });
                parameters.Add(new SqlParameters { ParameterName = "@marks", ParameterValue = qalification.Marks });
                common = repository.ParseCommonDbResponse(repository.ExecuteDataTable(procName, parameters));
            }
            return common;
        }

        public CommonDbResponse DeleteQualification(QalificationsCommon qalification)
        {
            CommonDbResponse common = new CommonDbResponse();
            string procName = "EmployeeManagement";
                List<SqlParameters> parameters = new List<SqlParameters>();
                parameters.Add(new SqlParameters { ParameterName = "@action", ParameterValue = "qualification-delete" });
                parameters.Add(new SqlParameters { ParameterName = "@emp_id", ParameterValue = qalification.employeeId });
                parameters.Add(new SqlParameters { ParameterName = "@Q_id", ParameterValue = qalification.QId });
                
                common = repository.ParseCommonDbResponse(repository.ExecuteDataTable(procName, parameters));
            return common;
        }



    }
}
