﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Manager.GraphicRepository {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Graphic", Namespace="http://schemas.datacontract.org/2004/07/ImageService.Models")]
    [System.SerializableAttribute()]
    public partial class Graphic : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AuthorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.IO.Stream ImageStreamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Author {
            get {
                return this.AuthorField;
            }
            set {
                if ((this.AuthorField.Equals(value) != true)) {
                    this.AuthorField = value;
                    this.RaisePropertyChanged("Author");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.IO.Stream ImageStream {
            get {
                return this.ImageStreamField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageStreamField, value) != true)) {
                    this.ImageStreamField = value;
                    this.RaisePropertyChanged("ImageStream");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Path {
            get {
                return this.PathField;
            }
            set {
                if ((object.ReferenceEquals(this.PathField, value) != true)) {
                    this.PathField = value;
                    this.RaisePropertyChanged("Path");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GraphicRepository.IGraphicRepository")]
    public interface IGraphicRepository {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGraphicRepository/AddImage", ReplyAction="http://tempuri.org/IGraphicRepository/AddImageResponse")]
        string AddImage(Manager.GraphicRepository.Graphic graphic);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGraphicRepository/AddImage", ReplyAction="http://tempuri.org/IGraphicRepository/AddImageResponse")]
        System.Threading.Tasks.Task<string> AddImageAsync(Manager.GraphicRepository.Graphic graphic);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGraphicRepository/GetImage", ReplyAction="http://tempuri.org/IGraphicRepository/GetImageResponse")]
        Manager.GraphicRepository.Graphic GetImage(int user, string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGraphicRepository/GetImage", ReplyAction="http://tempuri.org/IGraphicRepository/GetImageResponse")]
        System.Threading.Tasks.Task<Manager.GraphicRepository.Graphic> GetImageAsync(int user, string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGraphicRepository/GetNewestImages", ReplyAction="http://tempuri.org/IGraphicRepository/GetNewestImagesResponse")]
        Manager.GraphicRepository.Graphic[] GetNewestImages(int limit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGraphicRepository/GetNewestImages", ReplyAction="http://tempuri.org/IGraphicRepository/GetNewestImagesResponse")]
        System.Threading.Tasks.Task<Manager.GraphicRepository.Graphic[]> GetNewestImagesAsync(int limit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGraphicRepository/GetUserImages", ReplyAction="http://tempuri.org/IGraphicRepository/GetUserImagesResponse")]
        Manager.GraphicRepository.Graphic[] GetUserImages(int user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGraphicRepository/GetUserImages", ReplyAction="http://tempuri.org/IGraphicRepository/GetUserImagesResponse")]
        System.Threading.Tasks.Task<Manager.GraphicRepository.Graphic[]> GetUserImagesAsync(int user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGraphicRepositoryChannel : Manager.GraphicRepository.IGraphicRepository, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GraphicRepositoryClient : System.ServiceModel.ClientBase<Manager.GraphicRepository.IGraphicRepository>, Manager.GraphicRepository.IGraphicRepository {
        
        public GraphicRepositoryClient() {
        }
        
        public GraphicRepositoryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GraphicRepositoryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GraphicRepositoryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GraphicRepositoryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string AddImage(Manager.GraphicRepository.Graphic graphic) {
            return base.Channel.AddImage(graphic);
        }
        
        public System.Threading.Tasks.Task<string> AddImageAsync(Manager.GraphicRepository.Graphic graphic) {
            return base.Channel.AddImageAsync(graphic);
        }
        
        public Manager.GraphicRepository.Graphic GetImage(int user, string id) {
            return base.Channel.GetImage(user, id);
        }
        
        public System.Threading.Tasks.Task<Manager.GraphicRepository.Graphic> GetImageAsync(int user, string id) {
            return base.Channel.GetImageAsync(user, id);
        }
        
        public Manager.GraphicRepository.Graphic[] GetNewestImages(int limit) {
            return base.Channel.GetNewestImages(limit);
        }
        
        public System.Threading.Tasks.Task<Manager.GraphicRepository.Graphic[]> GetNewestImagesAsync(int limit) {
            return base.Channel.GetNewestImagesAsync(limit);
        }
        
        public Manager.GraphicRepository.Graphic[] GetUserImages(int user) {
            return base.Channel.GetUserImages(user);
        }
        
        public System.Threading.Tasks.Task<Manager.GraphicRepository.Graphic[]> GetUserImagesAsync(int user) {
            return base.Channel.GetUserImagesAsync(user);
        }
    }
}
