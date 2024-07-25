using System.Collections.Generic;
namespace ScenesScripts.FocusClock
{
    public class FreeTrialPrivilege
    {
        /// <summary>
        /// 
        /// </summary>
        public string resConsumable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userConsumable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string listenType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cannotListenReason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string playReason { get; set; }
    }

    public class FreeTimeTrialPrivilege
    {
        /// <summary>
        /// 
        /// </summary>
        public string resConsumable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userConsumable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long remainTime { get; set; }
    }

    public class DataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long br { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string md5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long expi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double gain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long peak { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long payed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string canExtend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string freeTrialInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encodeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string channelLayout { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FreeTrialPrivilege freeTrialPrivilege { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FreeTimeTrialPrivilege freeTimeTrialPrivilege { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long urlSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long rightSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string podcastCtrp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string effectTypes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
    }

    public class MusicInfoModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DataItem> data { get; set; }
    }
}