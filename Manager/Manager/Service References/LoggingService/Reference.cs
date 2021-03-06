﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Manager.LoggingService {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LogLevel", Namespace="http://schemas.datacontract.org/2004/07/LogService")]
    public enum LogLevel : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FATAL = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERROR = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        WARN = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INFO = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DEBUG = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LoggingService.ILoggerService")]
    public interface ILoggerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoggerService/addLog", ReplyAction="http://tempuri.org/ILoggerService/addLogResponse")]
        void addLog(string message, Manager.LoggingService.LogLevel logLevel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoggerService/addLog", ReplyAction="http://tempuri.org/ILoggerService/addLogResponse")]
        System.Threading.Tasks.Task addLogAsync(string message, Manager.LoggingService.LogLevel logLevel);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoggerServiceChannel : Manager.LoggingService.ILoggerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoggerServiceClient : System.ServiceModel.ClientBase<Manager.LoggingService.ILoggerService>, Manager.LoggingService.ILoggerService {
        
        public LoggerServiceClient() {
        }
        
        public LoggerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LoggerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoggerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoggerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void addLog(string message, Manager.LoggingService.LogLevel logLevel) {
            base.Channel.addLog(message, logLevel);
        }
        
        public System.Threading.Tasks.Task addLogAsync(string message, Manager.LoggingService.LogLevel logLevel) {
            return base.Channel.addLogAsync(message, logLevel);
        }
    }
}
