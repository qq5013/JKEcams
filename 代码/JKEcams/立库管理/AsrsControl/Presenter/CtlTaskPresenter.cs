﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using AsrsModel;
using AsrsInterface;
using CtlDBAccess.BLL;
using CtlDBAccess.Model;
namespace AsrsControl
{
    public class CtlTaskPresenter
    {
        private IAsrsManageToCtl asrsResourceManage = null; //立库管理层接口对象
        private TaskQueryFilterModel taskFilter = null;
        private ICtlTaskView view = null;
        private ControlTaskBll taskBll = new ControlTaskBll();
        private Dictionary<string, string> nodeNameMapID = new Dictionary<string, string>();
        private Dictionary<string, string> nodeIDMapName = new Dictionary<string, string>();
        public TaskQueryFilterModel TaskFilter { get { return taskFilter; } set { taskFilter = value; } }



        public CtlTaskPresenter(ICtlTaskView view)
        {
            taskFilter = new TaskQueryFilterModel();
            this.view = view;
            nodeNameMapID["A1库"] = "1001";
            nodeNameMapID["B1库"] = "1002";

            nodeNameMapID["托盘绑定"] = "3001";
            nodeNameMapID["卸载读卡A"] = "4001";
            nodeNameMapID["卸载读卡B"] = "4002";
            nodeNameMapID["扫码工位1"] = "6001";
            nodeNameMapID["扫码工位2"] = "6002";
            nodeNameMapID["扫码工位3"] = "6003";

            nodeNameMapID["人工组包1"] = "6004";
            nodeNameMapID["人工组包2"] = "6005";
            nodeNameMapID["人工组包3"] = "6006";
            nodeNameMapID["人工组包4"] = "6007";


            nodeIDMapName["1001"] = "A1库";
            nodeIDMapName["1002"] = "B1库";

            nodeIDMapName["3001"] = "托盘绑定";
            nodeIDMapName["4001"] = "卸载读卡A";
            nodeIDMapName["4002"] = "卸载读卡B";

            nodeIDMapName["6001"] = "扫码工位1";
            nodeIDMapName["6002"] = "扫码工位2";
            nodeIDMapName["6003"] = "扫码工位3";
            nodeIDMapName["6004"] = "人工组包1";
            nodeIDMapName["6005"] = "人工组包2";
            nodeIDMapName["6006"] = "人工组包3";
            nodeIDMapName["6007"] = "人工组包4";

        }
        public void SetAsrsResManage(IAsrsManageToCtl asrsResManage)
        {
            this.asrsResourceManage = asrsResManage;
        }
        public void QueryTask()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("CreateTime between '{0}' and '{1}' ",
               taskFilter.StartDate.ToString("yyyy-MM-dd 0:00:00"),
               taskFilter.EndDate.ToString("yyyy-MM-dd 0:00:00"));
            if(taskFilter.NodeName != "所有")
            {
                strWhere.AppendFormat(" and DeviceID='{0}'", nodeNameMapID[taskFilter.NodeName]);
            }
            if(taskFilter.TaskType != "所有")
            {
               
                strWhere.AppendFormat(" and TaskType={0}", (int)(Enum.Parse(typeof(SysCfg.EnumAsrsTaskType),taskFilter.TaskType)));
            }
            if(taskFilter.TaskStatus != "所有")
            {
                strWhere.AppendFormat(" and TaskStatus='{0}'", taskFilter.TaskStatus);
            }
            strWhere.AppendFormat(" order by CreateTime asc");
            DataSet ds = taskBll.GetList(strWhere.ToString());
            DataTable dt = ds.Tables[0];
            dt.Columns["TaskID"].ColumnName= "任务ID";
            dt.Columns["TaskType"].ColumnName = "任务类型";
            dt.Columns["TaskParam"].ColumnName = "任务参数";
            dt.Columns["TaskStatus"].ColumnName = "任务状态";
            dt.Columns["TaskPhase"].ColumnName = "任务当前步号";
            dt.Columns["CreateTime"].ColumnName = "创建时间";
            dt.Columns["FinishTime"].ColumnName = "完成时间";
            dt.Columns["CreateMode"].ColumnName = "创建模式";
            dt.Columns["DeviceID"].ColumnName = "设备ID";
            dt.Columns["tag1"].ColumnName = "库房";
            dt.Columns["tag2"].ColumnName = "货位";
            dt.Columns.Remove("tag3");
            dt.Columns.Remove("tag4");
            dt.Columns.Remove("tag5");
            dt.Columns["Remark"].ColumnName = "备注";
            dt.Columns.Add("设备");
            foreach(DataRow dr in dt.Rows)
            {
                dr["设备"] = nodeIDMapName[dr["设备ID"].ToString()];
            }
            view.RefreshTaskDisp(dt);
        }
        public void DelTask(List<string> taskIDs)
        {
            foreach(string taskID in taskIDs)
            {
                CtlDBAccess.Model.ControlTaskModel taskModel = taskBll.GetModel(taskID);
                if(taskModel == null )
                {
                    continue;
                }
                if (taskModel.TaskStatus == SysCfg.EnumTaskStatus.执行中.ToString() || taskModel.TaskStatus == SysCfg.EnumTaskStatus.超时.ToString())
                {
                    continue;
                }
                if(taskModel.TaskType >5)
                {
                    continue;
                }
                AsrsTaskParamModel paramModel = new AsrsTaskParamModel();
                string reStr="";
                if (!paramModel.ParseParam((SysCfg.EnumAsrsTaskType)taskModel.TaskType, taskModel.TaskParam,string.Empty, ref reStr))
                {
                    Console.WriteLine(string.Format("任务ID：{0}，参数解析失败，无法删除"), taskModel.TaskID);
                    continue;
                }
                if (taskModel.TaskStatus == SysCfg.EnumTaskStatus.待执行.ToString())
                {
                    if(!asrsResourceManage.UpdateGsTaskStatus(taskModel.tag1, paramModel.CellPos1, EnumGSTaskStatus.完成, ref reStr))
                    {
                        Console.WriteLine(string.Format("任务ID:{0},删除失败，因为更新{1}:{2}-{3}-{4}状态失败", taskModel.TaskID, taskModel.tag1, paramModel.CellPos1.Row, paramModel.CellPos1.Col, paramModel.CellPos1.Layer));
                        continue;
                    }
                    if (taskModel.TaskType == (int)SysCfg.EnumAsrsTaskType.移库)
                    {
                        if(!asrsResourceManage.UpdateGsTaskStatus(taskModel.tag1, paramModel.CellPos2, EnumGSTaskStatus.完成, ref reStr))
                        {
                            Console.WriteLine(string.Format("任务ID:{0},删除失败，因为更新{1}:{2}-{3}-{4}状态失败", taskModel.TaskID, taskModel.tag1, paramModel.CellPos2.Row, paramModel.CellPos2.Col, paramModel.CellPos2.Layer));
                            continue;
                        }
                    }
                }
               
                taskBll.Delete(taskID);
            }
            QueryTask();
        }


        public void GetAllNodeName(ref List<string> allNodeName)
        {
            allNodeName.Clear();
            foreach (string nodeName in nodeNameMapID.Keys)
            {
                allNodeName.Add(nodeName);
            }

        }
    }
}
