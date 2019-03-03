using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS_COMMON;
using WCMS_DAL.Interfaces;
using WCMS_ENTITY;

namespace WCMS_DAL.Inserting
{
    public class WCMS_DAL_ImeiAppend : IImeiAppend
    {
        private readonly WCMSEntities _entity = new WCMSEntities();
        public List<tblModel> GetModelName()
        {
            List<tblModel> list;
            try
            {
                list = _entity.tblModel.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }
        public List<tblColors> GetColorName()
        {
            List<tblColors> list;
            try
            {
                list = _entity.tblColors.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public bool InsertImeiInfo(tblIMEI im)
        {
            try
            {
                _entity.tblIMEI.Add(im);
                _entity.SaveChanges();

            }
            catch (Exception exception)
            {

                throw exception;

            }

            return true;
        }

        public bool InsertNewImeiInfo(tblIMEI imei)
        {
            try
            {
                tblNewBarCodeInven va=new tblNewBarCodeInven();
                va.IMEI1 = imei.IMEI1;
                va.IMEI2 = imei.IMEI2;
                va.Model = imei.Model;
                va.Color = imei.Color;
                _entity.tblNewBarCodeInven.Add(va);
                _entity.SaveChanges();

            }
            catch (Exception exception)
            {

                throw exception;

            }

            return true;
        }
        public List<tblIMEIRecord> ImeiExists(tblIMEIRecord imei)
        {
            List<tblIMEIRecord> list;
            try
            {
                list = _entity.tblIMEIRecord.Where(x=>x.IMEI1==imei.IMEI1 || x.IMEI2==imei.IMEI1).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return list;
        }

        public tblIMEI ImeiCheck(tblIMEI imei)
        {
            tblIMEI im;
            try
            {
                im = _entity.tblIMEI.FirstOrDefault(x => x.IMEI1 == imei.IMEI1 || x.IMEI2 == imei.IMEI1);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return im;
        }

      

       
    }
}
