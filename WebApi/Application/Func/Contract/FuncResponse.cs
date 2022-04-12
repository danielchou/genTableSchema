﻿namespace ESUN.AGD.WebApi.Application.Func.Contract
{
    /// <summary>
    /// 功能回傳結果
    /// </summary>
    public class  FuncResponse : CommonResponse
    {
        /// <summary>
        /// 功能代碼
        /// </summary>
        public string funcID { get; set; }
        /// <summary>
        /// 功能名稱
        /// </summary>
        public string funcName { get; set; }
        /// <summary>
        /// 上層功能代碼
        /// </summary>
        public string parentFuncID { get; set; }
        /// <summary>
        /// 階層
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 系統類別
        /// </summary>
        public string systemType { get; set; }
        /// <summary>
        /// Icon名稱
        /// </summary>
        public string? iconName { get; set; }
        /// <summary>
        /// 路由名稱
        /// </summary>
        public string? routeName { get; set; }
        /// <summary>
        /// 顯示順序
        /// </summary>
        public int displayOrder { get; set; }
    }
}
