using System;
using System.Collections.Generic;
using System.Text;

namespace Zhoubin.Infrastructure.Net.Core.Common
{
    /// <summary>
    /// 基础架构异常
    /// </summary>
    public class InfrastructureException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">信息</param>
        public InfrastructureException(string message) : base(message) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        public InfrastructureException() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="innerException">内联异常</param>
        public InfrastructureException(string message, Exception innerException) : base(message, innerException) { }

    }
    /// <summary>
    /// 基础架构配置异常
    /// </summary>
    public class InfrastructureConfigException : InfrastructureException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">信息</param>
        public InfrastructureConfigException(string message) : base(message) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        public InfrastructureConfigException() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="innerException">内联异常</param>
        public InfrastructureConfigException(string message, Exception innerException) : base(message, innerException) { }

    }
}
