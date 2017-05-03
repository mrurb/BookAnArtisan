﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookAnArtisanMVC.MaterialServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Material", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Material : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConditionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OwnderIdField;
        
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
        public string Condition {
            get {
                return this.ConditionField;
            }
            set {
                if ((object.ReferenceEquals(this.ConditionField, value) != true)) {
                    this.ConditionField = value;
                    this.RaisePropertyChanged("Condition");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OwnderId {
            get {
                return this.OwnderIdField;
            }
            set {
                if ((object.ReferenceEquals(this.OwnderIdField, value) != true)) {
                    this.OwnderIdField = value;
                    this.RaisePropertyChanged("OwnderId");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MaterialServiceReference.IMaterialService")]
    public interface IMaterialService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/Create", ReplyAction="http://tempuri.org/IServiceOf_Material/CreateResponse")]
        BookAnArtisanMVC.MaterialServiceReference.Material Create(BookAnArtisanMVC.MaterialServiceReference.Material t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/Create", ReplyAction="http://tempuri.org/IServiceOf_Material/CreateResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material> CreateAsync(BookAnArtisanMVC.MaterialServiceReference.Material t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/Read", ReplyAction="http://tempuri.org/IServiceOf_Material/ReadResponse")]
        BookAnArtisanMVC.MaterialServiceReference.Material Read(BookAnArtisanMVC.MaterialServiceReference.Material t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/Read", ReplyAction="http://tempuri.org/IServiceOf_Material/ReadResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material> ReadAsync(BookAnArtisanMVC.MaterialServiceReference.Material t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/Update", ReplyAction="http://tempuri.org/IServiceOf_Material/UpdateResponse")]
        BookAnArtisanMVC.MaterialServiceReference.Material Update(BookAnArtisanMVC.MaterialServiceReference.Material t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/Update", ReplyAction="http://tempuri.org/IServiceOf_Material/UpdateResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material> UpdateAsync(BookAnArtisanMVC.MaterialServiceReference.Material t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/Delete", ReplyAction="http://tempuri.org/IServiceOf_Material/DeleteResponse")]
        BookAnArtisanMVC.MaterialServiceReference.Material Delete(BookAnArtisanMVC.MaterialServiceReference.Material t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/Delete", ReplyAction="http://tempuri.org/IServiceOf_Material/DeleteResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material> DeleteAsync(BookAnArtisanMVC.MaterialServiceReference.Material t);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/ReadAll", ReplyAction="http://tempuri.org/IServiceOf_Material/ReadAllResponse")]
        BookAnArtisanMVC.MaterialServiceReference.Material[] ReadAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_Material/ReadAll", ReplyAction="http://tempuri.org/IServiceOf_Material/ReadAllResponse")]
        System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material[]> ReadAllAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMaterialServiceChannel : BookAnArtisanMVC.MaterialServiceReference.IMaterialService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MaterialServiceClient : System.ServiceModel.ClientBase<BookAnArtisanMVC.MaterialServiceReference.IMaterialService>, BookAnArtisanMVC.MaterialServiceReference.IMaterialService {
        
        public MaterialServiceClient() {
        }
        
        public MaterialServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MaterialServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MaterialServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MaterialServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BookAnArtisanMVC.MaterialServiceReference.Material Create(BookAnArtisanMVC.MaterialServiceReference.Material t) {
            return base.Channel.Create(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material> CreateAsync(BookAnArtisanMVC.MaterialServiceReference.Material t) {
            return base.Channel.CreateAsync(t);
        }
        
        public BookAnArtisanMVC.MaterialServiceReference.Material Read(BookAnArtisanMVC.MaterialServiceReference.Material t) {
            return base.Channel.Read(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material> ReadAsync(BookAnArtisanMVC.MaterialServiceReference.Material t) {
            return base.Channel.ReadAsync(t);
        }
        
        public BookAnArtisanMVC.MaterialServiceReference.Material Update(BookAnArtisanMVC.MaterialServiceReference.Material t) {
            return base.Channel.Update(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material> UpdateAsync(BookAnArtisanMVC.MaterialServiceReference.Material t) {
            return base.Channel.UpdateAsync(t);
        }
        
        public BookAnArtisanMVC.MaterialServiceReference.Material Delete(BookAnArtisanMVC.MaterialServiceReference.Material t) {
            return base.Channel.Delete(t);
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material> DeleteAsync(BookAnArtisanMVC.MaterialServiceReference.Material t) {
            return base.Channel.DeleteAsync(t);
        }
        
        public BookAnArtisanMVC.MaterialServiceReference.Material[] ReadAll() {
            return base.Channel.ReadAll();
        }
        
        public System.Threading.Tasks.Task<BookAnArtisanMVC.MaterialServiceReference.Material[]> ReadAllAsync() {
            return base.Channel.ReadAllAsync();
        }
    }
}
