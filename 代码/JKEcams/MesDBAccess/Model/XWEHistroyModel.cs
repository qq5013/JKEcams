﻿using System;
namespace MesDBAccess.Model
{
    /// <summary>
    /// XWEHistroyModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class XWEHistroyModel
    {
        public XWEHistroyModel()
        { }
        #region Model
        private long _batterycodeid;
        private string _code;
        private string _channel;
        private string _pressure;
        private string _innerrc;
        private string _power;
        private string _capcity;
        private string _testresult;
        private DateTime? _testtime;
        private string _tag1;
        private string _tag2;
        private string _tag3;
        private string _tag4;
        private string _tag5;
        private string _housename;
        private string _goodssitename;
        private string _teststatus;
        private string _testtype;
        private string _palletid;
        /// <summary>
        /// 条码的主键ID
        /// </summary>
        public long BatteryCodeID
        {
            set { _batterycodeid = value; }
            get { return _batterycodeid; }
        }
        /// <summary>
        /// 条码
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 通道
        /// </summary>
        public string Channel
        {
            set { _channel = value; }
            get { return _channel; }
        }
        /// <summary>
        /// 电压
        /// </summary>
        public string Pressure
        {
            set { _pressure = value; }
            get { return _pressure; }
        }
        /// <summary>
        /// 内阻
        /// </summary>
        public string InnerRC
        {
            set { _innerrc = value; }
            get { return _innerrc; }
        }
        /// <summary>
        /// 功率
        /// </summary>
        public string Power
        {
            set { _power = value; }
            get { return _power; }
        }
        /// <summary>
        /// 容量
        /// </summary>
        public string Capcity
        {
            set { _capcity = value; }
            get { return _capcity; }
        }
        /// <summary>
        /// 测试结果
        /// </summary>
        public string TestResult
        {
            set { _testresult = value; }
            get { return _testresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TestTime
        {
            set { _testtime = value; }
            get { return _testtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tag1
        {
            set { _tag1 = value; }
            get { return _tag1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tag2
        {
            set { _tag2 = value; }
            get { return _tag2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tag3
        {
            set { _tag3 = value; }
            get { return _tag3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tag4
        {
            set { _tag4 = value; }
            get { return _tag4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tag5
        {
            set { _tag5 = value; }
            get { return _tag5; }
        }
        /// <summary>
        /// 库房名称
        /// </summary>
        public string HouseName
        {
            set { _housename = value; }
            get { return _housename; }
        }
        /// <summary>
        /// 货位名称，1-2-4,货位的排列层
        /// </summary>
        public string GoodsSiteName
        {
            set { _goodssitename = value; }
            get { return _goodssitename; }
        }
        /// <summary>
        /// 测试状态
        /// </summary>
        public string TestStatus
        {
            set { _teststatus = value; }
            get { return _teststatus; }
        }
        /// <summary>
        /// 测试类型
        /// </summary>
        public string TestType
        {
            set { _testtype = value; }
            get { return _testtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PalletID
        {
            set { _palletid = value; }
            get { return _palletid; }
        }
        #endregion Model

    }
}

