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
    class deleteTools
    {
        /// <summary>
        ///删除职工信息表中符合的数据 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static bool deleteEmployeeInfo(Expression<Func<employeeInfo, bool>> whereLambda)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<employeeInfo> dbDelete = db.employeeInfo.Where(whereLambda) as DbQuery<employeeInfo>;
                    List<employeeInfo> obDelete = dbDelete.ToList();
                    db.employeeInfo.RemoveRange(obDelete);
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
        ///删除公司部门表中符合的数据 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static bool deleteDepartment(Expression<Func<department, bool>> whereLambda)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<department> dbDelete = db.department.Where(whereLambda) as DbQuery<department>;
                    List<department> obDelete = dbDelete.ToList();
                    db.department.RemoveRange(obDelete);
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
        ///删除公司职务表中符合的数据 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static bool deleteCompanyPosition(Expression<Func<companyPosition, bool>> whereLambda)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<companyPosition> dbDelete = db.companyPosition.Where(whereLambda) as DbQuery<companyPosition>;
                    List<companyPosition> obDelete = dbDelete.ToList();
                    db.companyPosition.RemoveRange(obDelete);
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
        ///删除公司职务表中符合的数据 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public static bool deleteEmployeePerformance(Expression<Func<employeePerformance, bool>> whereLambda)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<employeePerformance> dbDelete = db.employeePerformance.Where(whereLambda) as DbQuery<employeePerformance>;
                    List<employeePerformance> obDelete = dbDelete.ToList();
                    db.employeePerformance.RemoveRange(obDelete);
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
        /// 删除一个雇员在公司的所有信息
        /// </summary>
        /// <param name="employeeId">雇员ID</param>
        /// <returns></returns>
        public static bool deleteEmployee(int employeeId)
        {
            try
            {
                //删除员工信息要先删除此员工在公司就职信息
                bool flag1 = false;
                flag1 = deleteEmployeePerformance(u => u.employeeId == employeeId);
                if (flag1 == true)
                {
                    bool flag2 = false;
                    flag2 = deleteEmployeeInfo(u => u.employeeId == employeeId);
                    return flag2;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除公司部门表的所有信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public static bool deleteCompanyPositionInfo(int departmentId)
        {
            try
            {
                //先删除公司部门旗下的所有职务
                bool flag1 = false;
                flag1 = deleteCompanyPosition(u => u.departmentId == departmentId);
                if (flag1 == true)
                {
                    bool flag2 = false;
                    flag2 = deleteDepartment(u => u.departmentId == departmentId);
                    return flag2;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
