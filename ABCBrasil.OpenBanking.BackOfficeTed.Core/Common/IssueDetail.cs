using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    /// <summary>
    /// Estrutura de dados para gravado informações no LOG
    /// </summary>
    public class IssueDetail
    {
        public IssueDetail()
        {
            Level = IssueLevelEnum.Info;
            Date = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public IssueLevelEnum Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FriendlyMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Exception Exception { get; set; }

        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object InformationData { get; set; }
        public string TitleMessage { get; set; }
        public string PropertyValidated { get; set; }
    }
    public enum IssueLevelEnum
    {
        Info = 2,
        Warning = 3,
        Error = 4,
    }
}
