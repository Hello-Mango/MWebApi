using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure
{
    public class AppSettings
    {
        public JWTConfig JWTConfig { get; set; }
        public SwaggerConfig SwaggerConfig { get; set; }
        public SnowflakeConfig SnowflakeConfig { get; set; }
        public AuditLogConfig AuditLog { get; set; }
        public DataBaseConfig DataBaseConfig { get; set; }
        public AppSettings() { }
    }
    public class JWTConfig
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expires { get; set; }
        public int RefreshExpiration { get; set; }
    }

    public class SwaggerConfig
    {
        public bool IsShow { get; set; }
        public bool LongToString { get; set; }
    }

    public class SnowflakeConfig
    {
        public int WorkerId { get; set; }
        public int DataCenterId { get; set; }
    }
    public class AuditLogConfig
    {
        /// <summary>
        /// 是否启用数据级审计日志
        /// </summary>
        public bool DbEnable { get; set; }
        /// <summary>
        /// 是否启用登录日志
        /// </summary>
        public bool LoginEnable { get; set; }
        /// <summary>
        /// 是否启用API耗时监控
        /// </summary>
        public bool ApiMonitorEnable { get; set; }
        /// <summary>
        /// 是否启用Api调用日志记录
        /// </summary>
        public bool ApiLogEnable { get; set; }
        /// <summary>
        /// API调用日志记录过滤器
        /// </summary>
        public string ApiLogFilterPattern { get; set; }

        /// <summary>
        /// 是否启用异常日志记录
        /// </summary>
        public bool ExceptionLogEnable { get; set; }
    }

    public class DataBaseConfig
    {
        public string DbType { get; set; }
        public string ConnectionString { get; set; }
        public string ReadConnectionString { get; set; }
    }
}
