using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeeInfoSystem.Model;

namespace EmployeeInfoSystem.dataTools
{
    class updateTools
    {
        /// <summary>
        /// 修改employeeInfo表的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool updateEmployeeInfo(Expression<Func<employeeInfo, bool>> whereLambda, employeeInfo info)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<employeeInfo> dataObject = db.employeeInfo.Where(whereLambda) as DbQuery<employeeInfo>;
                    employeeInfo oldInfo = dataObject.FirstOrDefault();
                    oldInfo.employeeName = info.employeeName;
                    oldInfo.employeeSex = info.employeeSex;
                    oldInfo.employeePhone = info.employeePhone;
                    oldInfo.employeeBir = info.employeeBir;
                    oldInfo.employeeIdentity = info.employeeIdentity;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改department表的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool updateDepartment(Expression<Func<department, bool>> whereLambda, department info)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<department> dataObject = db.department.Where(whereLambda) as DbQuery<department>;
                    department oldInfo = dataObject.FirstOrDefault();
                    oldInfo.departmentName = info.departmentName;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改companyPosition表的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool updateCompanyPosition(Expression<Func<companyPosition, bool>> whereLambda, companyPosition info)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<companyPosition> dataObject = db.companyPosition.Where(whereLambda) as DbQuery<companyPosition>;
                    companyPosition oldInfo = dataObject.FirstOrDefault();
                    oldInfo.departmentId = info.departmentId;
                    oldInfo.positionName = info.positionName;
                    oldInfo.positionSalary = info.positionSalary;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改employeePerformance表的数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool updateEmployeePerformance(Expression<Func<employeePerformance, bool>> whereLambda, employeePerformance info)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<employeePerformance> dataObject = db.employeePerformance.Where(whereLambda) as DbQuery<employeePerformance>;
                    employeePerformance oldInfo = dataObject.FirstOrDefault();
                    oldInfo.positionId = info.positionId;
                    oldInfo.performance = info.performance;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
