using System;
using HLY.Repository;
using HLY.Repository.Domain;
using HLY.Repository.Interface;

namespace HLY.App
{
    public class FormFactory
    {
        public static IForm CreateForm(Form form, IUnitWork<HLYDBContext> unitWork)
        {
            if (form.FrmType == 0)
            {
                return new LeipiForm(unitWork);
            }else if (form.FrmType == 1)
            {
                throw new Exception("自定义表单不需要创建数据库表");
            }
            else
            {
                return new DragForm(unitWork);
            }
        }
    }
}