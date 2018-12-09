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
    class selectTools
    {
        /// <summary>
        /// 查找职工信息
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static employeeInfo[] selectEmployeeInfo<TKey>(Expression<Func<employeeInfo, bool>> whereLambda, Expression<Func<employeeInfo, TKey>> orderBy)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<employeeInfo> dataObject = db.employeeInfo.Where(whereLambda).OrderBy(orderBy) as DbQuery<employeeInfo>;
                    employeeInfo[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                employeeInfo[] nullInfo = new employeeInfo[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找公司部门信息
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static department[] selectDepartment<TKey>(Expression<Func<department, bool>> whereLambda, Expression<Func<department, TKey>> orderBy)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<department> dataObject = db.department.Where(whereLambda).OrderBy(orderBy) as DbQuery<department>;
                    department[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                department[] nullInfo = new department[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找公司职务信息
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static companyPosition[] selectCompanyPosition<TKey>(Expression<Func<companyPosition, bool>> whereLambda, Expression<Func<companyPosition, TKey>> orderBy)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<companyPosition> dataObject = db.companyPosition.Where(whereLambda).OrderBy(orderBy) as DbQuery<companyPosition>;
                    companyPosition[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                companyPosition[] nullInfo = new companyPosition[0];
                return nullInfo;
            }
        }

        /// <summary>
        /// 查找公司职务信息
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static employeePerformance[] selectEmployeePerformance<TKey>(Expression<Func<employeePerformance, bool>> whereLambda, Expression<Func<employeePerformance, TKey>> orderBy)
        {
            try
            {
                using (employeeInfoSystemEntities db = new employeeInfoSystemEntities())
                {
                    DbQuery<employeePerformance> dataObject = db.employeePerformance.Where(whereLambda).OrderBy(orderBy) as DbQuery<employeePerformance>;
                    employeePerformance[] infoList = dataObject.ToArray();
                    return infoList;
                }
            }
            catch
            {
                employeePerformance[] nullInfo = new employeePerformance[0];
                return nullInfo;
            }
        }
    }
}
